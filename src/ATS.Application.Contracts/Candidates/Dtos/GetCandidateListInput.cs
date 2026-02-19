using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Candidates.Dtos
{
    public class GetCandidateListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public List<string>? Skills { get; set; }
        public int? MinExperience { get; set; }
        public int? MaxExperience { get; set; }
        public decimal? MinAIScore { get; set; }
        public CandidateStatus? Status { get; set; }
        public string? Source { get; set; }
        public bool? IsOpenToRemote { get; set; }
    }
}
