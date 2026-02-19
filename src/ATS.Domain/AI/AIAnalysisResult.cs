using ATS.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace ATS.AI
{
    /// <summary>
    /// Stores AI analysis results for applications
    /// </summary>
    public class AIAnalysisResult : CreationAuditedEntity<Guid>
    {
        public Guid ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        // Overall matching score
        public decimal OverallScore { get; set; }

        // Individual scoring components
        public decimal SkillMatchScore { get; set; }
        public decimal ExperienceScore { get; set; }
        public decimal EducationScore { get; set; }
        public decimal LocationScore { get; set; }
        public decimal SalaryExpectationScore { get; set; }

        // AI Provider details
        [MaxLength(50)]
        public string AIProvider { get; set; } // Claude, OpenAI, etc.

        [MaxLength(50)]
        public string ModelVersion { get; set; }

        // Detailed analysis (JSON)
        public string DetailedAnalysisJson { get; set; }

        // Strengths and weaknesses
        public string StrengthsJson { get; set; }
        public string WeaknessesJson { get; set; }

        // Recommendations
        [MaxLength(1000)]
        public string Recommendation { get; set; }

        public RecommendationType RecommendationType { get; set; }

        // Keywords extracted
        public string ExtractedKeywordsJson { get; set; }

        // Processing metadata
        public DateTime AnalysisDate { get; set; }
        public int ProcessingTimeMs { get; set; }

        // Confidence level
        public decimal ConfidenceLevel { get; set; }

        // Red flags or concerns
        public string RedFlagsJson { get; set; }

        protected AIAnalysisResult()
        {
        }

        public AIAnalysisResult(Guid id, Guid applicationId) : base()
        {
            Id = id;
            ApplicationId = applicationId;
            AnalysisDate = DateTime.UtcNow;
        }

        public void SetScores(decimal overall, decimal skills, decimal experience,
            decimal education, decimal location, decimal salary)
        {
            OverallScore = Math.Min(100, Math.Max(0, overall));
            SkillMatchScore = Math.Min(100, Math.Max(0, skills));
            ExperienceScore = Math.Min(100, Math.Max(0, experience));
            EducationScore = Math.Min(100, Math.Max(0, education));
            LocationScore = Math.Min(100, Math.Max(0, location));
            SalaryExpectationScore = Math.Min(100, Math.Max(0, salary));
        }
    }

    public enum RecommendationType
    {
        StronglyRecommend = 0,
        Recommend = 1,
        Consider = 2,
        NotRecommended = 3,
        RequiresReview = 4
    }
}
