using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Reports.Dtos
{
    public class DashboardStatsDto
    {
        public int OpenPositions { get; set; }
        public int TotalApplications { get; set; }
        public int ApplicationsThisWeek { get; set; }
        public int InterviewsScheduled { get; set; }
        public int OffersExtended { get; set; }
        public int HiredThisMonth { get; set; }
        public double AverageTimeToHire { get; set; }
        public decimal AverageAIScore { get; set; }
        public Dictionary<string, int> ApplicationsByStatus { get; set; }
        public Dictionary<string, int> ApplicationsByStage { get; set; }
        public List<TrendDataPoint> ApplicationTrend { get; set; }
    }
}
