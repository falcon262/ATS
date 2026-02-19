using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Applications.Dtos
{
    public class UpdateApplicationDto
    {
        public ApplicationStatus? Status { get; set; }
        public PipelineStage? Stage { get; set; }

        [StringLength(2000)]
        public string ReviewNotes { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        public Guid? AssignedToId { get; set; }
        public string AssignedToName { get; set; }
    }
}
