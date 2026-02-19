using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ATS.Jobs.Public
{
    /// <summary>
    /// Public service for job applications (no authentication required)
    /// </summary>
    public interface IPublicJobAppService : IApplicationService
    {
        /// <summary>
        /// Get a public job by its slug
        /// </summary>
        Task<PublicJobDto> GetBySlugAsync(string slug);

        /// <summary>
        /// Get all active public jobs
        /// </summary>
        Task<List<PublicJobDto>> GetActivePublicJobsAsync();

        /// <summary>
        /// Submit a job application (creates candidate and application records)
        /// </summary>
        /// <returns>Application ID for registration</returns>
        Task<Guid> SubmitApplicationAsync(PublicJobApplicationDto input);
    }
}

