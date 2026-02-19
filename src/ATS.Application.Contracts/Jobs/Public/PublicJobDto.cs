using ATS.Jobs;
using System;
using System.Collections.Generic;

namespace ATS.Jobs.Public
{
    /// <summary>
    /// Public job DTO for external job listings - no sensitive data
    /// </summary>
    public class PublicJobDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Requirements { get; set; }
        public string? Responsibilities { get; set; }
        public string? Location { get; set; }
        public bool IsRemote { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public List<string> RequiredSkills { get; set; } = new List<string>();
        public List<string> PreferredSkills { get; set; } = new List<string>();
        public DateTime PostedDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string PublicSlug { get; set; } = string.Empty;
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? Currency { get; set; }
    }
}

