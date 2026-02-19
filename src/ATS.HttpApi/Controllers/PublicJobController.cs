using ATS.Jobs.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace ATS.Controllers
{
    /// <summary>
    /// Public API for job applications (no authentication required)
    /// </summary>
    [RemoteService(Name = "ATS")]
    [Area("ats")]
    [Route("api/public/jobs")]
    [AllowAnonymous]
    public class PublicJobController : AbpControllerBase
    {
        private readonly IPublicJobAppService _publicJobService;

        public PublicJobController(IPublicJobAppService publicJobService)
        {
            _publicJobService = publicJobService;
        }

        /// <summary>
        /// Get all active public jobs
        /// </summary>
        [HttpGet]
        public virtual Task<List<PublicJobDto>> GetActivePublicJobsAsync()
        {
            return _publicJobService.GetActivePublicJobsAsync();
        }

        /// <summary>
        /// Get a specific job by its public slug
        /// </summary>
        /// <param name="slug">The job slug (e.g., "software-engineer-abc123")</param>
        [HttpGet("{slug}")]
        public virtual Task<PublicJobDto> GetBySlugAsync(string slug)
        {
            return _publicJobService.GetBySlugAsync(slug);
        }

        /// <summary>
        /// Submit a job application
        /// </summary>
        [HttpPost("apply")]
        public virtual Task<Guid> SubmitApplicationAsync([FromBody] PublicJobApplicationDto input)
        {
            return _publicJobService.SubmitApplicationAsync(input);
        }
    }
}

