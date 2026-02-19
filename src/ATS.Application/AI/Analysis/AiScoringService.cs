using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ATS.AI.OpenAI;
using ATS.AI.Parsing;
using ATS.Applications.Dtos;
using ATS.Candidates;
using ATS.Candidates.Dtos;
using ATS.Jobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ATS.AI.Analysis
{
    public class AiScoringService : ITransientDependency
    {
        private readonly OpenAiClientFactory _clientFactory;
        private readonly AIOptions _options;
        private readonly ILogger<AiScoringService> _logger;
        private readonly IBlobContainer _blobContainer;
        private readonly IRepository<Job, Guid> _jobRepository;
        private readonly IRepository<Candidate, Guid> _candidateRepository;
        private readonly ResumeTextExtractor _resumeExtractor;

        public AiScoringService(
            OpenAiClientFactory clientFactory,
            IOptions<AIOptions> options,
            ILogger<AiScoringService> logger,
            IBlobContainer blobContainer,
            IRepository<Job, Guid> jobRepository,
            IRepository<Candidate, Guid> candidateRepository,
            ResumeTextExtractor resumeExtractor)
        {
            _clientFactory = clientFactory;
            _options = options.Value;
            _logger = logger;
            _blobContainer = blobContainer;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _resumeExtractor = resumeExtractor;
        }

        public async Task<AnalysisResult> AnalyzeApplicationAsync(
            Guid jobId, 
            Guid candidateId, 
            string? coverLetter = null, 
            Dictionary<string, string>? screeningAnswers = null)
        {
            if (!_options.Enabled)
            {
                throw new BusinessException("AI analysis is currently disabled");
            }

            _logger.LogInformation("Starting AI analysis for Job {JobId}, Candidate {CandidateId}", jobId, candidateId);

            // Fetch job details
            var job = await _jobRepository.GetAsync(jobId);
            
            // Fetch candidate details
            var candidate = await _candidateRepository.GetAsync(candidateId);

            // Extract resume text
            string resumeText = string.Empty;
            if (!string.IsNullOrWhiteSpace(candidate.ResumeUrl))
            {
                resumeText = await ExtractResumeTextAsync(candidate.ResumeUrl);
            }

            // Build analysis request
            var analysisRequest = BuildAnalysisRequest(job, candidate, resumeText, coverLetter, screeningAnswers);

            // Call LLM
            var result = await CallOpenAIAsync(analysisRequest);

            _logger.LogInformation("AI analysis completed for Job {JobId}, Candidate {CandidateId} with score {Score}", 
                jobId, candidateId, result.OverallScore);

            return result;
        }

        private async Task<string> ExtractResumeTextAsync(string resumeUrl)
        {
            try
            {
                var blob = await _blobContainer.GetAsync(resumeUrl);
                
                using (var memoryStream = new MemoryStream())
                {
                    await blob.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    
                    var fileName = resumeUrl.Split('/').LastOrDefault() ?? "resume.pdf";
                    return await _resumeExtractor.ExtractTextAsync(memoryStream, fileName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to extract resume text from {ResumeUrl}", resumeUrl);
                return string.Empty;
            }
        }

        private string BuildAnalysisRequest(
            Job job, 
            Candidate candidate, 
            string resumeText, 
            string? coverLetter, 
            Dictionary<string, string>? screeningAnswers)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("# Job Description");
            sb.AppendLine($"**Title:** {job.Title}");
            sb.AppendLine($"**Department:** {job.Department}");
            sb.AppendLine($"**Location:** {job.Location}");
            sb.AppendLine($"**Employment Type:** {job.EmploymentType}");
            sb.AppendLine($"**Experience Level:** {job.ExperienceLevel}");
            sb.AppendLine();
            sb.AppendLine($"**Description:**");
            sb.AppendLine(job.Description);
            sb.AppendLine();
            
            if (!string.IsNullOrWhiteSpace(job.Responsibilities))
            {
                sb.AppendLine($"**Responsibilities:**");
                sb.AppendLine(job.Responsibilities);
                sb.AppendLine();
            }

            var requiredSkills = DeserializeSkills(job.RequiredSkillsJson);
            if (requiredSkills.Any())
            {
                sb.AppendLine($"**Required Skills:** {string.Join(", ", requiredSkills)}");
            }

            var preferredSkills = DeserializeSkills(job.PreferredSkillsJson);
            if (preferredSkills.Any())
            {
                sb.AppendLine($"**Preferred Skills:** {string.Join(", ", preferredSkills)}");
            }

            if (!string.IsNullOrWhiteSpace(job.Benefits))
            {
                sb.AppendLine();
                sb.AppendLine($"**Benefits:**");
                sb.AppendLine(job.Benefits);
            }

            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();
            sb.AppendLine("# Candidate Profile");
            sb.AppendLine($"**Name:** {candidate.FirstName} {candidate.LastName}");
            sb.AppendLine($"**Current Title:** {candidate.CurrentJobTitle ?? "Not specified"}");
            sb.AppendLine($"**Current Company:** {candidate.CurrentCompany ?? "Not specified"}");
            sb.AppendLine($"**Years of Experience:** {candidate.YearsOfExperience}");
            sb.AppendLine($"**Location:** {candidate.City}, {candidate.State}, {candidate.Country}");
            sb.AppendLine($"**Remote Preference:** {(candidate.IsOpenToRemote ? "Open to remote" : "Prefers on-site")}");
            sb.AppendLine($"**Willing to Relocate:** {(candidate.IsWillingToRelocate ? "Yes" : "No")}");
            
            if (candidate.ExpectedSalary.HasValue)
            {
                sb.AppendLine($"**Expected Salary:** {candidate.PreferredCurrency} {candidate.ExpectedSalary:N0}");
            }

            sb.AppendLine();
            sb.AppendLine("**Resume Content:**");
            sb.AppendLine(resumeText);
            
            if (!string.IsNullOrWhiteSpace(coverLetter))
            {
                sb.AppendLine();
                sb.AppendLine("**Cover Letter:**");
                sb.AppendLine(coverLetter);
            }

            if (screeningAnswers != null && screeningAnswers.Any())
            {
                sb.AppendLine();
                sb.AppendLine("**Screening Answers:**");
                foreach (var qa in screeningAnswers)
                {
                    sb.AppendLine($"Q: {qa.Key}");
                    sb.AppendLine($"A: {qa.Value}");
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private List<string> DeserializeSkills(string? skillsJson)
        {
            if (string.IsNullOrWhiteSpace(skillsJson))
                return new List<string>();

            try
            {
                return JsonSerializer.Deserialize<List<string>>(skillsJson) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        private async Task<AnalysisResult> CallOpenAIAsync(string request)
        {
            var client = _clientFactory.CreateClient();
            var chatClient = client.GetChatClient(_options.Model);

            var rubricPath = Path.Combine(AppContext.BaseDirectory, "AI", "Prompts", "ScoringRubric.md");
            string systemPrompt;
            
            if (File.Exists(rubricPath))
            {
                systemPrompt = await File.ReadAllTextAsync(rubricPath);
            }
            else
            {
                // Fallback inline rubric
                systemPrompt = GetInlineRubric();
            }

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(request)
            };

            var options = new ChatCompletionOptions
            {
                Temperature = (float)_options.Temperature,
                MaxOutputTokenCount = _options.MaxTokens,
                ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
            };

            try
            {
                var completion = await chatClient.CompleteChatAsync(messages, options);
                var responseText = completion.Value.Content[0].Text;

                _logger.LogDebug("OpenAI Response: {Response}", responseText);

                var result = JsonSerializer.Deserialize<AnalysisResult>(responseText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null)
                {
                    throw new BusinessException("Failed to parse AI response");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to call OpenAI API");
                throw new BusinessException("AI analysis failed: " + ex.Message);
            }
        }

        private string GetInlineRubric()
        {
            return @"You are an expert technical recruiter analyzing candidate resumes for job applications. 
Evaluate how well a candidate matches a job description and return ONLY valid JSON with this structure:
{
  ""overallScore"": 0-100,
  ""hireBand"": ""Strong"" | ""Consider"" | ""No"",
  ""skillsMatch"": [{""skill"": ""string"", ""hasIt"": true|false, ""yearsExperience"": 0, ""weight"": ""Required""|""Preferred""}],
  ""experienceFit"": {""yearsRelevant"": 0, ""seniorityLevel"": ""string"", ""score"": 0-25},
  ""educationFit"": {""degreeLevel"": ""string"", ""relevance"": ""string"", ""certifications"": [], ""score"": 0-15},
  ""culturalFit"": {""strengths"": [], ""concerns"": [], ""score"": 0-10},
  ""logisticsFit"": {""locationMatch"": true|false, ""availabilityMatch"": true|false, ""salaryMatch"": ""string"", ""score"": 0-10},
  ""keyStrengths"": [],
  ""riskFlags"": [],
  ""recommendation"": ""string"",
  ""followUpQuestions"": []
}
Score: Skills(40) + Experience(25) + Education(15) + Cultural(10) + Logistics(10) = 100.
Hire Bands: Strong(80-100), Consider(60-79), No(<60).";
        }
    }

    public class AnalysisResult
    {
        [JsonPropertyName("overallScore")]
        public int OverallScore { get; set; }

        [JsonPropertyName("hireBand")]
        public string HireBand { get; set; } = string.Empty;

        [JsonPropertyName("skillsMatch")]
        public List<SkillMatch> SkillsMatch { get; set; } = new();

        [JsonPropertyName("experienceFit")]
        public ExperienceFit ExperienceFit { get; set; } = new();

        [JsonPropertyName("educationFit")]
        public EducationFit EducationFit { get; set; } = new();

        [JsonPropertyName("culturalFit")]
        public CulturalFit CulturalFit { get; set; } = new();

        [JsonPropertyName("logisticsFit")]
        public LogisticsFit LogisticsFit { get; set; } = new();

        [JsonPropertyName("keyStrengths")]
        public List<string> KeyStrengths { get; set; } = new();

        [JsonPropertyName("riskFlags")]
        public List<string> RiskFlags { get; set; } = new();

        [JsonPropertyName("recommendation")]
        public string Recommendation { get; set; } = string.Empty;

        [JsonPropertyName("followUpQuestions")]
        public List<string> FollowUpQuestions { get; set; } = new();
    }

    public class SkillMatch
    {
        [JsonPropertyName("skill")]
        public string Skill { get; set; } = string.Empty;

        [JsonPropertyName("hasIt")]
        public bool HasIt { get; set; }

        [JsonPropertyName("yearsExperience")]
        public int YearsExperience { get; set; }

        [JsonPropertyName("weight")]
        public string Weight { get; set; } = string.Empty;
    }

    public class ExperienceFit
    {
        [JsonPropertyName("yearsRelevant")]
        public int YearsRelevant { get; set; }

        [JsonPropertyName("seniorityLevel")]
        public string SeniorityLevel { get; set; } = string.Empty;

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }

    public class EducationFit
    {
        [JsonPropertyName("degreeLevel")]
        public string DegreeLevel { get; set; } = string.Empty;

        [JsonPropertyName("relevance")]
        public string Relevance { get; set; } = string.Empty;

        [JsonPropertyName("certifications")]
        public List<string> Certifications { get; set; } = new();

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }

    public class CulturalFit
    {
        [JsonPropertyName("strengths")]
        public List<string> Strengths { get; set; } = new();

        [JsonPropertyName("concerns")]
        public List<string> Concerns { get; set; } = new();

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }

    public class LogisticsFit
    {
        [JsonPropertyName("locationMatch")]
        public bool LocationMatch { get; set; }

        [JsonPropertyName("availabilityMatch")]
        public bool AvailabilityMatch { get; set; }

        [JsonPropertyName("salaryMatch")]
        public string SalaryMatch { get; set; } = string.Empty;

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}

