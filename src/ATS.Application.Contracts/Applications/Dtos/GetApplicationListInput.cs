using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Applications.Dtos
{
    public class GetApplicationListInput : PagedAndSortedResultRequestDto
    {
        public Guid? JobId { get; set; }
        public Guid? CandidateId { get; set; }
        public ApplicationStatus? Status { get; set; }
        public PipelineStage? Stage { get; set; }
        public Guid? AssignedToId { get; set; }
        public decimal? MinAIScore { get; set; }
        public DateTime? AppliedAfter { get; set; }
        public DateTime? AppliedBefore { get; set; }
    }
}
