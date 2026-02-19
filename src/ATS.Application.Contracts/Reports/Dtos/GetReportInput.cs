using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Reports.Dtos
{
    public class GetReportInput
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? DepartmentId { get; set; }
        public string ReportType { get; set; }
    }
}
