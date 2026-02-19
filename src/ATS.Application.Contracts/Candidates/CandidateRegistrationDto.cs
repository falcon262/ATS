using System;
using System.ComponentModel.DataAnnotations;

namespace ATS.Candidates
{
    /// <summary>
    /// DTO for candidate registration from application
    /// </summary>
    public class CandidateRegistrationDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(128, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public Guid ApplicationId { get; set; }

        public bool AcceptTerms { get; set; }
    }
}

