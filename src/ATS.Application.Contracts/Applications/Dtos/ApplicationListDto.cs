using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Applications.Dtos
{
    public class ApplicationListDto : EntityDto<Guid>
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public Guid CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public PipelineStage Stage { get; set; }
        public decimal? AIScore { get; set; }
        public int? Rank { get; set; }
        public string AssignedToName { get; set; }
        public int? Rating { get; set; }
    }
}
