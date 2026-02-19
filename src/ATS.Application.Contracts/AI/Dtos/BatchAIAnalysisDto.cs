using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.AI.Dtos
{
    public class BatchAIAnalysisDto
    {
        public Guid JobId { get; set; }
        public List<Guid> ApplicationIds { get; set; }
        public bool RankAfterAnalysis { get; set; }
    }
}
