using ATS.Applications.Dtos;
using ATS.Candidates;
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
    /// Candidate portal API (authenticated candidates only)
    /// </summary>
    [RemoteService(Name = "ATS")]
    [Area("ats")]
    [Route("api/candidate-portal")]
    public class CandidatePortalController : AbpControllerBase
    {
        private readonly ICandidatePortalAppService _candidatePortalService;

        public CandidatePortalController(ICandidatePortalAppService candidatePortalService)
        {
            _candidatePortalService = candidatePortalService;
        }

        /// <summary>
        /// Get all applications for the current candidate
        /// </summary>
        [HttpGet("applications")]
        [Authorize]
        public virtual Task<List<CandidateApplicationListDto>> GetMyApplicationsAsync()
        {
            return _candidatePortalService.GetMyApplicationsAsync();
        }

        /// <summary>
        /// Get detailed view of a specific application
        /// </summary>
        [HttpGet("applications/{id}")]
        [Authorize]
        public virtual Task<ApplicationDto> GetMyApplicationDetailAsync(Guid id)
        {
            return _candidatePortalService.GetMyApplicationDetailAsync(id);
        }

        /// <summary>
        /// Register a candidate account from an existing application
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public virtual Task RegisterFromApplicationAsync([FromBody] CandidateRegistrationDto input)
        {
            return _candidatePortalService.RegisterFromApplicationAsync(input);
        }
    }
}

