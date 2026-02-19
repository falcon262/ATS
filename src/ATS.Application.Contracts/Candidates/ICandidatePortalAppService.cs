using ATS.Applications.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ATS.Candidates
{
    /// <summary>
    /// Service for candidate portal (authenticated candidates only)
    /// </summary>
    public interface ICandidatePortalAppService : IApplicationService
    {
        /// <summary>
        /// Get all applications for the current candidate
        /// </summary>
        Task<List<CandidateApplicationListDto>> GetMyApplicationsAsync();

        /// <summary>
        /// Get detailed view of a specific application (with ownership validation)
        /// </summary>
        Task<ApplicationDto> GetMyApplicationDetailAsync(Guid id);

        /// <summary>
        /// Register a candidate account from an existing application
        /// </summary>
        Task RegisterFromApplicationAsync(CandidateRegistrationDto input);
    }
}

