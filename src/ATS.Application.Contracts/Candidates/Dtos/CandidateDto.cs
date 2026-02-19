using ATS.Candidates.Dtos.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Candidates.Dtos
{
    // ========== CANDIDATE DTOs ==========

    public class CandidateDto : FullAuditedEntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhone { get; set; }
        public string CurrentJobTitle { get; set; }
        public string CurrentCompany { get; set; }
        public int YearsOfExperience { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string ResumeUrl { get; set; }
        public string CoverLetterUrl { get; set; }
        public string PortfolioUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string PersonalWebsite { get; set; }
        public List<string> Skills { get; set; }
        public List<EducationInfo> Education { get; set; }
        public List<ExperienceInfo> Experience { get; set; }
        public List<string> Certifications { get; set; }
        public decimal? ExpectedSalary { get; set; }
        public string PreferredCurrency { get; set; }
        public bool IsWillingToRelocate { get; set; }
        public bool IsOpenToRemote { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public string NoticePeriod { get; set; }
        public string Source { get; set; }
        public string ReferredBy { get; set; }
        public CandidateStatus Status { get; set; }
        public decimal? OverallAIScore { get; set; }
        public List<string> Tags { get; set; }
        public string Notes { get; set; }
        public bool ConsentToProcess { get; set; }
        public DateTime? ConsentDate { get; set; }
    }
}
