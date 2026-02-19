using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class CreateApplicationDto
    {
        [Required]
        public Guid JobId { get; set; }

        [Required]
        public Guid CandidateId { get; set; }

        [StringLength(5000)]
        public string CoverLetter { get; set; }

        public Dictionary<string, string> ScreeningAnswers { get; set; }
    }
}
