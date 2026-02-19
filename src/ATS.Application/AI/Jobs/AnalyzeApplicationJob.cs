using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ATS.AI.Analysis;
using ATS.Applications;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace ATS.AI.Jobs
{
    public class AnalyzeApplicationJob : AsyncBackgroundJob<AnalyzeApplicationJobArgs>, ITransientDependency
    {
        private readonly AiScoringService _scoringService;
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly ILogger<AnalyzeApplicationJob> _logger;

        public AnalyzeApplicationJob(
            AiScoringService scoringService,
            IRepository<Application, Guid> applicationRepository,
            ILogger<AnalyzeApplicationJob> logger)
        {
            _scoringService = scoringService;
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        [UnitOfWork]
        public override async Task ExecuteAsync(AnalyzeApplicationJobArgs args)
        {
            _logger.LogInformation("Executing AI analysis for Application {ApplicationId}", args.ApplicationId);

            try
            {
                var application = await _applicationRepository.GetAsync(args.ApplicationId);

                var result = await _scoringService.AnalyzeApplicationAsync(
                    application.JobId,
                    application.CandidateId,
                    application.CoverLetter,
                    DeserializeScreeningAnswers(application.ScreeningAnswersJson));

                // Update application with analysis results
                application.AIScore = result.OverallScore;
                application.AIMatchSummary = result.Recommendation;
                application.AIAnalysisDetailsJson = JsonSerializer.Serialize(result);
                application.SkillMatchScoresJson = JsonSerializer.Serialize(result.SkillsMatch);

                await _applicationRepository.UpdateAsync(application, autoSave: true);

                _logger.LogInformation(
                    "AI analysis completed for Application {ApplicationId} with score {Score}, band {Band}",
                    args.ApplicationId, result.OverallScore, result.HireBand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to analyze Application {ApplicationId}", args.ApplicationId);
                throw;
            }
        }

        private System.Collections.Generic.Dictionary<string, string>? DeserializeScreeningAnswers(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;

            try
            {
                return JsonSerializer.Deserialize<System.Collections.Generic.Dictionary<string, string>>(json);
            }
            catch
            {
                return null;
            }
        }
    }

    public class AnalyzeApplicationJobArgs
    {
        public Guid ApplicationId { get; set; }
    }
}

