using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Jobs.Dtos
{
    public class GetJobListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? DepartmentId { get; set; }
        public JobStatus? Status { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public bool? IsRemote { get; set; }
        public DateTime? PostedAfter { get; set; }
        public DateTime? PostedBefore { get; set; }
    }
}
