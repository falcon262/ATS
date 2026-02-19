using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ATS.Jobs
{
    /// <summary>
    /// Domain service for Job entity business logic
    /// </summary>
    public class JobManager : DomainService
    {
        private readonly IRepository<Job, Guid> _jobRepository;

        public JobManager(IRepository<Job, Guid> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        /// <summary>
        /// Creates a new job with slug generation
        /// </summary>
        public async Task<Job> CreateAsync(
            Guid id,
            string title,
            string description,
            Guid departmentId,
            EmploymentType employmentType,
            string? location = null,
            string? customSlug = null)
        {
            var job = new Job(id, title, description, departmentId, employmentType, location);

            // If custom slug provided, sanitize and set it
            if (!string.IsNullOrWhiteSpace(customSlug))
            {
                var sanitizedSlug = JobSlugGenerator.SanitizeCustomSlug(customSlug);
                await EnsureUniqueSlugAsync(sanitizedSlug);
                job.SetPublicSlug(sanitizedSlug);
            }

            return job;
        }

        /// <summary>
        /// Ensures the slug is unique across all jobs
        /// </summary>
        /// <param name="slug">The slug to check</param>
        /// <exception cref="InvalidOperationException">Thrown if slug already exists</exception>
        public async Task EnsureUniqueSlugAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return;

            var queryable = await _jobRepository.GetQueryableAsync();
            var exists = queryable.Any(j => j.PublicSlug == slug);

            if (exists)
            {
                throw new InvalidOperationException($"A job with slug '{slug}' already exists.");
            }
        }

        /// <summary>
        /// Updates the slug for an existing job
        /// </summary>
        public async Task UpdateSlugAsync(Job job, string newSlug)
        {
            if (string.IsNullOrWhiteSpace(newSlug))
                throw new ArgumentException("Slug cannot be empty", nameof(newSlug));

            var sanitizedSlug = JobSlugGenerator.SanitizeCustomSlug(newSlug);

            // Only check uniqueness if slug is actually changing
            if (job.PublicSlug != sanitizedSlug)
            {
                await EnsureUniqueSlugAsync(sanitizedSlug);
                job.SetPublicSlug(sanitizedSlug);
            }
        }

        /// <summary>
        /// Generates and sets a slug for a job if it doesn't have one
        /// </summary>
        public void EnsureSlugExists(Job job)
        {
            if (string.IsNullOrWhiteSpace(job.PublicSlug))
            {
                var generatedSlug = JobSlugGenerator.GenerateFromTitle(job.Title, job.Id);
                job.SetPublicSlug(generatedSlug);
            }
        }
    }
}

