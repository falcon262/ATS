using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class AIAnalysisDetails
    {
        public decimal SkillMatchScore { get; set; }
        public decimal ExperienceScore { get; set; }
        public decimal EducationScore { get; set; }
        public decimal LocationScore { get; set; }
        public List<string> Strengths { get; set; }
        public List<string> Weaknesses { get; set; }
        public string Recommendation { get; set; }
        public List<string> RedFlags { get; set; }
    }
}
