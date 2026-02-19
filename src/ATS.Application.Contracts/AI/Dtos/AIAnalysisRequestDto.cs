using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.AI.Dtos
{
    public class AIAnalysisRequestDto
    {
        public Guid ApplicationId { get; set; }
        public bool ForceReanalysis { get; set; }
    }
}
