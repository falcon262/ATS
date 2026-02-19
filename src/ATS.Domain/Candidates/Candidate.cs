using ATS.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ATS.Candidates
{
    /// <summary>
    /// Represents a candidate/applicant in the system
    /// </summary>
    public class Candidate : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(20)]
        public string? AlternatePhone { get; set; }

        // Current Position
        [MaxLength(200)]
        public string? CurrentJobTitle { get; set; }

        [MaxLength(200)]
        public string? CurrentCompany { get; set; }

        public int YearsOfExperience { get; set; }

        // Location
        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        // Resume and documents
        [MaxLength(500)]
        public string? ResumeUrl { get; set; }

        [MaxLength(500)]
        public string? CoverLetterUrl { get; set; }

        [MaxLength(500)]
        public string? PortfolioUrl { get; set; }

        // Social profiles
        [MaxLength(200)]
        public string? LinkedInUrl { get; set; }

        [MaxLength(200)]
        public string? GitHubUrl { get; set; }

        [MaxLength(200)]
        public string? PersonalWebsite { get; set; }

        // Skills (stored as JSON)
        public string? SkillsJson { get; set; }

        // Education (stored as JSON)
        public string? EducationJson { get; set; }

        // Work Experience (stored as JSON)
        public string? ExperienceJson { get; set; }

        // Certifications (stored as JSON)
        public string? CertificationsJson { get; set; }

        // Candidate preferences
        public decimal? ExpectedSalary { get; set; }

        [MaxLength(50)]
        public string? PreferredCurrency { get; set; } = "USD";

        public bool IsWillingToRelocate { get; set; }
        public bool IsOpenToRemote { get; set; }

        public DateTime? AvailableFrom { get; set; }

        [MaxLength(50)]
        public string? NoticePeriod { get; set; }

        // Source tracking
        [MaxLength(100)]
        public string? Source { get; set; } // LinkedIn, Indeed, Referral, etc.

        [MaxLength(200)]
        public string? ReferredBy { get; set; }

        // Status
        public CandidateStatus Status { get; set; }

        // AI Analysis Results (stored as JSON)
        public string? AIAnalysisJson { get; set; }

        // Overall AI Score
        public decimal? OverallAIScore { get; set; }

        // Tags for easy searching (stored as JSON)
        public string? TagsJson { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        // GDPR Compliance
        public bool ConsentToProcess { get; set; }
        public DateTime? ConsentDate { get; set; }

        // Navigation properties
        public virtual ICollection<Application> Applications { get; set; }

        protected Candidate()
        {
            Applications = new HashSet<Application>();
        }

        public Candidate(Guid id, string firstName, string lastName, string email,
            string phoneNumber = null) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Status = CandidateStatus.Active;
            Applications = new HashSet<Application>();
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void UpdateAIScore(decimal score)
        {
            OverallAIScore = Math.Min(100, Math.Max(0, score));
        }

        public void GrantConsent()
        {
            ConsentToProcess = true;
            ConsentDate = DateTime.UtcNow;
        }

        public void RevokeConsent()
        {
            ConsentToProcess = false;
            Status = CandidateStatus.Inactive;
        }
    }

}
