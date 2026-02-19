using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.AI.Dtos
{
    public class AIRankingRequestDto
    {
        public Guid JobId { get; set; }
        public int TopCount { get; set; } = 10;
        public decimal MinScore { get; set; } = 0;
    }
}
