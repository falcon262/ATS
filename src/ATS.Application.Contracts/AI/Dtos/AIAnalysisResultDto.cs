using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.AI.Dtos
{
    public class AIAnalysisResultDto
    {
        public Guid ApplicationId { get; set; }
        public decimal OverallScore { get; set; }
        public decimal SkillMatchScore { get; set; }
        public decimal ExperienceScore { get; set; }
        public decimal EducationScore { get; set; }
        public decimal LocationScore { get; set; }
        public decimal SalaryExpectationScore { get; set; }
        public string AIProvider { get; set; }
        public string ModelVersion { get; set; }
        public List<string> Strengths { get; set; }
        public List<string> Weaknesses { get; set; }
        public string Recommendation { get; set; }
        public string RecommendationType { get; set; }
        public List<string> ExtractedKeywords { get; set; }
        public DateTime AnalysisDate { get; set; }
        public int ProcessingTimeMs { get; set; }
        public decimal ConfidenceLevel { get; set; }
        public List<string> RedFlags { get; set; }
    }
}
