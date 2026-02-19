using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Reports.Dtos
{
    public class TrendDataPoint
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public string Label { get; set; }
    }
}
