using ATS.Applications;
using ATS.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ATS.Jobs
{
    /// <summary>
    /// Represents a job posting in the system
    /// </summary>
    public class Job : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string Requirements { get; set; }

        [MaxLength(1000)]
        public string Responsibilities { get; set; }

        [MaxLength(2000)]
        public string? Benefits { get; set; }

        // Department relationship
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        public bool IsRemote { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        [MaxLength(50)]
        public string Currency { get; set; } = "USD";

        // JSON field for storing required skills
        public string RequiredSkillsJson { get; set; }

        // JSON field for storing preferred skills
        public string PreferredSkillsJson { get; set; }

        public JobStatus Status { get; set; }

        public DateTime PostedDate { get; set; }
        public DateTime? ClosingDate { get; set; }

        // Hiring manager
        public Guid? HiringManagerId { get; set; }

        [MaxLength(100)]
        public string? HiringManagerName { get; set; }

        [MaxLength(100)]
        public string? HiringManagerEmail { get; set; }

        // Statistics
        public int ViewCount { get; set; }
        public int ApplicationCount { get; set; }

        // Public application link
        [MaxLength(250)]
        public string? PublicSlug { get; set; }

        public bool IsPubliclyVisible { get; set; }

        // Navigation properties
        public virtual ICollection<Application> Applications { get; set; }

        protected Job()
        {
            Applications = new HashSet<Application>();
        }

        public Job(Guid id, string title, string description, Guid departmentId,
            EmploymentType employmentType, string location = null) : base(id)
        {
            Title = title;
            Description = description;
            DepartmentId = departmentId;
            EmploymentType = employmentType;
            Location = location;
            Status = JobStatus.Draft;
            PostedDate = DateTime.UtcNow;
            Applications = new HashSet<Application>();
        }

        public void Publish()
        {
            if (Status != JobStatus.Draft)
                throw new InvalidOperationException("Only draft jobs can be published");

            Status = JobStatus.Active;
            PostedDate = DateTime.UtcNow;
            IsPubliclyVisible = true;

            // Generate slug if not already set
            if (string.IsNullOrWhiteSpace(PublicSlug))
            {
                PublicSlug = JobSlugGenerator.GenerateFromTitle(Title, Id);
            }
        }

        public void SetPublicSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug cannot be empty", nameof(slug));

            PublicSlug = slug;
        }

        public void Close()
        {
            Status = JobStatus.Closed;
            ClosingDate = DateTime.UtcNow;
        }

        public void IncrementViewCount()
        {
            ViewCount++;
        }

        public void IncrementApplicationCount()
        {
            ApplicationCount++;
        }
    }

   
}
