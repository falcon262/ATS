using ATS.Applications;
using ATS.Applications.Dtos;
using ATS.Jobs;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace ATS.Candidates
{
    /// <summary>
    /// Service for candidate portal - authenticated candidates only
    /// </summary>
    [Authorize]
    public class CandidatePortalAppService : ApplicationService, ICandidatePortalAppService
    {
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly IRepository<Candidate, Guid> _candidateRepository;
        private readonly IRepository<Job, Guid> _jobRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IIdentityRoleRepository _roleRepository;

        public CandidatePortalAppService(
            IRepository<Application, Guid> applicationRepository,
            IRepository<Candidate, Guid> candidateRepository,
            IRepository<Job, Guid> jobRepository,
            IIdentityUserRepository userRepository,
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleRepository = roleRepository;
        }

        public async Task<List<CandidateApplicationListDto>> GetMyApplicationsAsync()
        {
            var currentUserEmail = CurrentUser.Email;
            if (string.IsNullOrEmpty(currentUserEmail))
                throw new UserFriendlyException("Unable to identify current user");

            // Find candidate by email
            var candidate = await _candidateRepository.FirstOrDefaultAsync(c => c.Email == currentUserEmail);
            if (candidate == null)
                throw new UserFriendlyException("Candidate profile not found");

            // Get all applications for this candidate
            var queryable = await _applicationRepository.GetQueryableAsync();
            var applications = await AsyncExecuter.ToListAsync(
                queryable
                    .Where(a => a.CandidateId == candidate.Id)
                    .OrderByDescending(a => a.AppliedDate));

            var dtos = new List<CandidateApplicationListDto>();

            // Get job information
            var jobIds = applications.Select(a => a.JobId).Distinct().ToList();
            var jobs = await _jobRepository.GetListAsync(j => jobIds.Contains(j.Id));
            var jobDict = jobs.ToDictionary(j => j.Id, j => j);

            foreach (var application in applications)
            {
                var job = jobDict.GetValueOrDefault(application.JobId);
                if (job != null)
                {
                    dtos.Add(new CandidateApplicationListDto
                    {
                        Id = application.Id,
                        JobTitle = job.Title,
                        Company = job.DepartmentId.ToString(), // Could be enhanced with actual company info
                        AppliedDate = application.AppliedDate,
                        Status = application.Status,
                        Stage = application.Stage,
                        AIScore = application.AIScore,
                        JobId = job.Id
                    });
                }
            }

            return dtos;
        }

        public async Task<ApplicationDto> GetMyApplicationDetailAsync(Guid id)
        {
            var currentUserEmail = CurrentUser.Email;
            if (string.IsNullOrEmpty(currentUserEmail))
                throw new UserFriendlyException("Unable to identify current user");

            // Find candidate by email
            var candidate = await _candidateRepository.FirstOrDefaultAsync(c => c.Email == currentUserEmail);
            if (candidate == null)
                throw new UserFriendlyException("Candidate profile not found");

            // Get application with ownership validation
            var application = await _applicationRepository.GetAsync(id);
            
            if (application.CandidateId != candidate.Id)
                throw new UserFriendlyException("You do not have permission to view this application");

            // Map to DTO
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }

        [AllowAnonymous] // Allow anonymous for initial registration
        public async Task RegisterFromApplicationAsync(CandidateRegistrationDto input)
        {
            // Validate application exists
            var application = await _applicationRepository.GetAsync(input.ApplicationId);
            
            // Get candidate from application
            var candidate = await _candidateRepository.GetAsync(application.CandidateId);

            // Verify email matches
            if (candidate.Email != input.Email)
                throw new UserFriendlyException("Email does not match the application");

            // Check if user already exists
            var existingUser = await _userRepository.FindByNormalizedEmailAsync(
                _userManager.NormalizeName(input.Email));
            
            if (existingUser != null)
                throw new UserFriendlyException("An account with this email already exists. Please login instead.");

            // Create Identity user
            var user = new IdentityUser(
                GuidGenerator.Create(),
                input.Email,
                input.Email,
                tenantId: CurrentTenant.Id);

            user.SetEmailConfirmed(true); // Auto-confirm for now; consider email verification in production

            // Create user with password
            var identityResult = await _userManager.CreateAsync(user, input.Password);
            if (!identityResult.Succeeded)
            {
                throw new UserFriendlyException(
                    "Failed to create account: " + 
                    string.Join(", ", identityResult.Errors.Select(e => e.Description)));
            }

            // Assign "Candidate" role
            var candidateRole = await _roleRepository.FindByNormalizedNameAsync("CANDIDATE");
            if (candidateRole != null)
            {
                await _userManager.AddToRoleAsync(user, "Candidate");
            }

            // Note: In a real application, you might want to send a welcome email here
        }
    }
}

