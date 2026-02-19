using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Jobs.Dtos
{
    // ========== JOB DTOs ==========

    public class JobDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public string Benefits { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public bool IsRemote { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string Currency { get; set; }
        public List<string> RequiredSkills { get; set; }
        public List<string> PreferredSkills { get; set; }
        public JobStatus Status { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public Guid? HiringManagerId { get; set; }
        public string HiringManagerName { get; set; }
        public string HiringManagerEmail { get; set; }
        public int ViewCount { get; set; }
        public int ApplicationCount { get; set; }
        public string? PublicSlug { get; set; }
        public bool IsPubliclyVisible { get; set; }
        
        /// <summary>
        /// Computed property for public application URL
        /// Will be set by the application layer
        /// </summary>
        public string? PublicApplicationUrl { get; set; }
    }

}
