using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class RejectApplicationInput
    {
        public Guid ApplicationId { get; set; }

        [Required]
        [StringLength(500)]
        public string RejectionReason { get; set; }

        public bool SendNotification { get; set; }
    }
}
