using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Jobs.Dtos
{
    public class CreateUpdateJobDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string? Requirements { get; set; }

        [StringLength(1000)]
        public string? Responsibilities { get; set; }

        [StringLength(2000)]
        public string? Benefits { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public bool IsRemote { get; set; }

        [Required]
        public EmploymentType EmploymentType { get; set; }

        [Required]
        public ExperienceLevel ExperienceLevel { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MinSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MaxSalary { get; set; }

        [StringLength(50)]
        public string Currency { get; set; } = "USD";

        public List<string> RequiredSkills { get; set; } = new List<string>();
        public List<string> PreferredSkills { get; set; } = new List<string>();

        public DateTime? ClosingDate { get; set; }

        public Guid? HiringManagerId { get; set; }

        [StringLength(100)]
        public string? HiringManagerName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? HiringManagerEmail { get; set; }
    }
}
