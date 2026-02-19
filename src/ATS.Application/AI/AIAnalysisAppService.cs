using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ATS.AI.Analysis;
using ATS.AI.Dtos;
using ATS.AI.Jobs;
using ATS.Applications;
using ATS.Applications.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace ATS.AI
{
    public class AIAnalysisAppService : ApplicationService, IAIAnalysisAppService
    {
        private readonly AiScoringService _scoringService;
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly AIOptions _options;
        private readonly ILogger<AIAnalysisAppService> _logger;

        public AIAnalysisAppService(
            AiScoringService scoringService,
            IRepository<Application, Guid> applicationRepository,
            IBackgroundJobManager backgroundJobManager,
            IOptions<AIOptions> options,
            ILogger<AIAnalysisAppService> logger)
        {
            _scoringService = scoringService;
            _applicationRepository = applicationRepository;
            _backgroundJobManager = backgroundJobManager;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<AIAnalysisResultDto> AnalyzeApplicationAsync(AIAnalysisRequestDto input)
        {
            var application = await _applicationRepository.GetAsync(input.ApplicationId);

            // Check if already analyzed and not forcing reanalysis
            if (!input.ForceReanalysis && application.AIScore.HasValue)
            {
                _logger.LogInformation("Application {ApplicationId} already analyzed, returning cached result", input.ApplicationId);
                return MapToDto(application);
            }

            var sw = Stopwatch.StartNew();

            var result = await _scoringService.AnalyzeApplicationAsync(
                application.JobId,
                application.CandidateId,
                application.CoverLetter,
                DeserializeScreeningAnswers(application.ScreeningAnswersJson));

            // Update application
            application.AIScore = result.OverallScore;
            application.AIMatchSummary = result.Recommendation;
            application.AIAnalysisDetailsJson = JsonSerializer.Serialize(result);
            application.SkillMatchScoresJson = JsonSerializer.Serialize(result.SkillsMatch);

            await _applicationRepository.UpdateAsync(application, autoSave: true);

            sw.Stop();

            return new AIAnalysisResultDto
            {
                ApplicationId = application.Id,
                OverallScore = result.OverallScore,
                SkillMatchScore = result.ExperienceFit.Score,
                ExperienceScore = result.ExperienceFit.Score,
                EducationScore = result.EducationFit.Score,
                LocationScore = result.LogisticsFit.Score,
                SalaryExpectationScore = result.LogisticsFit.Score,
                AIProvider = _options.Provider,
                ModelVersion = _options.Model,
                Strengths = result.KeyStrengths,
                Weaknesses = result.RiskFlags,
                Recommendation = result.Recommendation,
                RecommendationType = result.HireBand,
                ExtractedKeywords = result.SkillsMatch.Where(s => s.HasIt).Select(s => s.Skill).ToList(),
                AnalysisDate = DateTime.UtcNow,
                ProcessingTimeMs = (int)sw.ElapsedMilliseconds,
                ConfidenceLevel = 0.85m,
                RedFlags = result.RiskFlags
            };
        }

        public async Task<List<AIAnalysisResultDto>> BatchAnalyzeAsync(BatchAIAnalysisDto input)
        {
            var results = new List<AIAnalysisResultDto>();

            foreach (var applicationId in input.ApplicationIds)
            {
                try
                {
                    // Enqueue background job for each
                    await _backgroundJobManager.EnqueueAsync(new AnalyzeApplicationJobArgs
                    {
                        ApplicationId = applicationId
                    });

                    _logger.LogInformation("Enqueued analysis for Application {ApplicationId}", applicationId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to enqueue analysis for Application {ApplicationId}", applicationId);
                }
            }

            return results;
        }

        public async Task<List<ApplicationListDto>> GetRankedApplicationsAsync(AIRankingRequestDto input)
        {
            var queryable = await _applicationRepository.GetQueryableAsync();
            
            var applications = await AsyncExecuter.ToListAsync(
                queryable
                    .Where(a => a.JobId == input.JobId && a.AIScore.HasValue && a.AIScore >= input.MinScore)
                    .OrderByDescending(a => a.AIScore)
                    .Take(input.TopCount));

            // Map to DTOs using ObjectMapper
            return ObjectMapper.Map<List<Application>, List<ApplicationListDto>>(applications);
        }

        public async Task UpdateRankingsAsync(Guid jobId)
        {
            var queryable = await _applicationRepository.GetQueryableAsync();
            
            var applications = await AsyncExecuter.ToListAsync(
                queryable.Where(a => a.JobId == jobId));

            foreach (var application in applications)
            {
                if (!application.AIScore.HasValue)
                {
                    await _backgroundJobManager.EnqueueAsync(new AnalyzeApplicationJobArgs
                    {
                        ApplicationId = application.Id
                    });
                }
            }

            _logger.LogInformation("Enqueued {Count} applications for re-ranking for Job {JobId}", applications.Count, jobId);
        }

        private AIAnalysisResultDto MapToDto(Application application)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(application.AIAnalysisDetailsJson))
                {
                    var details = JsonSerializer.Deserialize<AnalysisResult>(application.AIAnalysisDetailsJson);
                    if (details != null)
                    {
                        return new AIAnalysisResultDto
                        {
                            ApplicationId = application.Id,
                            OverallScore = details.OverallScore,
                            SkillMatchScore = details.ExperienceFit.Score,
                            ExperienceScore = details.ExperienceFit.Score,
                            EducationScore = details.EducationFit.Score,
                            LocationScore = details.LogisticsFit.Score,
                            SalaryExpectationScore = details.LogisticsFit.Score,
                            AIProvider = _options.Provider,
                            ModelVersion = _options.Model,
                            Strengths = details.KeyStrengths,
                            Weaknesses = details.RiskFlags,
                            Recommendation = details.Recommendation,
                            RecommendationType = details.HireBand,
                            ExtractedKeywords = details.SkillsMatch.Where(s => s.HasIt).Select(s => s.Skill).ToList(),
                            AnalysisDate = application.LastModificationTime ?? application.CreationTime,
                            ProcessingTimeMs = 0,
                            ConfidenceLevel = 0.85m,
                            RedFlags = details.RiskFlags
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deserialize AI analysis details for Application {ApplicationId}", application.Id);
            }

            // Fallback
            return new AIAnalysisResultDto
            {
                ApplicationId = application.Id,
                OverallScore = application.AIScore ?? 0,
                Recommendation = application.AIMatchSummary ?? "No analysis available",
                AIProvider = _options.Provider,
                ModelVersion = _options.Model,
                AnalysisDate = application.LastModificationTime ?? application.CreationTime,
                Strengths = new List<string>(),
                Weaknesses = new List<string>(),
                ExtractedKeywords = new List<string>(),
                RedFlags = new List<string>()
            };
        }

        private Dictionary<string, string>? DeserializeScreeningAnswers(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}

