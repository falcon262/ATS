using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Jobs.Dtos
{
    public class JobListDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public JobStatus Status { get; set; }
        public DateTime PostedDate { get; set; }
        public int ApplicationCount { get; set; }
        public bool IsRemote { get; set; }
    }
}
