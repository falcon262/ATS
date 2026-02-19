using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Reports.Dtos
{
    public class RecruitmentMetricsDto
    {
        public double AverageTimeToHire { get; set; }
        public double AverageTimeInStage { get; set; }
        public decimal OfferAcceptanceRate { get; set; }
        public decimal ApplicationToInterviewRate { get; set; }
        public decimal InterviewToOfferRate { get; set; }
        public int TotalHires { get; set; }
        public int TotalApplications { get; set; }
        public Dictionary<string, double> TimeByStage { get; set; }
        public Dictionary<string, int> HiresByDepartment { get; set; }
        public Dictionary<string, int> HiresBySource { get; set; }
    }
}
