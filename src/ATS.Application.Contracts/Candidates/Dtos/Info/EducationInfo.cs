using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Candidates.Dtos.Info
{
    public class EducationInfo
    {
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string Institution { get; set; }
        public int? GraduationYear { get; set; }
        public string Grade { get; set; }
    }

}
