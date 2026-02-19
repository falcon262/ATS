using ATS.Candidates.Dtos.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Candidates.Dtos
{
    public class CreateUpdateCandidateDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string AlternatePhone { get; set; }

        [StringLength(200)]
        public string CurrentJobTitle { get; set; }

        [StringLength(200)]
        public string CurrentCompany { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [Url]
        [StringLength(200)]
        public string LinkedInUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string GitHubUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string PersonalWebsite { get; set; }

        public List<string> Skills { get; set; } = new List<string>();
        public List<EducationInfo> Education { get; set; } = new List<EducationInfo>();
        public List<ExperienceInfo> Experience { get; set; } = new List<ExperienceInfo>();
        public List<string> Certifications { get; set; } = new List<string>();

        [Range(0, double.MaxValue)]
        public decimal? ExpectedSalary { get; set; }

        [StringLength(50)]
        public string PreferredCurrency { get; set; }

        public bool IsWillingToRelocate { get; set; }
        public bool IsOpenToRemote { get; set; }
        public DateTime? AvailableFrom { get; set; }

        [StringLength(50)]
        public string NoticePeriod { get; set; }

        [StringLength(100)]
        public string Source { get; set; }

        [StringLength(200)]
        public string ReferredBy { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        [StringLength(2000)]
        public string Notes { get; set; }
    }
}
