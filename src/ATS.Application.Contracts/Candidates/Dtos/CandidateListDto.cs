using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Candidates.Dtos
{
    public class CandidateListDto : EntityDto<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CurrentJobTitle { get; set; }
        public int YearsOfExperience { get; set; }
        public string Location { get; set; }
        public decimal? OverallAIScore { get; set; }
        public CandidateStatus Status { get; set; }
        public DateTime CreationTime { get; set; }
        public List<string> TopSkills { get; set; }
    }
}
