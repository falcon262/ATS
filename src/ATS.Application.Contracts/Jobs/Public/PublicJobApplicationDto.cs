using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATS.Jobs.Public
{
    /// <summary>
    /// DTO for public job application submission
    /// </summary>
    public class PublicJobApplicationDto
    {
        [Required]
        public Guid JobId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? CurrentJobTitle { get; set; }

        [MaxLength(200)]
        public string? CurrentCompany { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [Url]
        [MaxLength(200)]
        public string? LinkedInUrl { get; set; }

        [Url]
        [MaxLength(200)]
        public string? GitHubUrl { get; set; }

        [Url]
        [MaxLength(200)]
        public string? PortfolioUrl { get; set; }

        [MaxLength(5000)]
        public string? CoverLetter { get; set; }

        [MaxLength(1000)]
        public string? EducationSummary { get; set; }

        [MaxLength(2000)]
        public string? ExperienceSummary { get; set; }

        public List<string> Skills { get; set; } = new List<string>();

        public string? ResumeFileName { get; set; }

        /// <summary>
        /// Resume file content as base64 string
        /// </summary>
        public string? ResumeContentBase64 { get; set; }

        /// <summary>
        /// GDPR Consent
        /// </summary>
        [Required]
        public bool ConsentToProcess { get; set; }
    }
}

