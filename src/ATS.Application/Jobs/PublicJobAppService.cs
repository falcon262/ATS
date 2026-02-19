using ATS.AI.Jobs;
using ATS.Applications;
using ATS.Candidates;
using ATS.Departments;
using ATS.Domain.Candidates.BlobContainer;
using ATS.Jobs.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace ATS.Jobs
{
    /// <summary>
    /// Public service for job applications - no authentication required
    /// </summary>
    [AllowAnonymous]
    public class PublicJobAppService : ApplicationService, IPublicJobAppService
    {
        private readonly IRepository<Job, Guid> _jobRepository;
        private readonly IRepository<Candidate, Guid> _candidateRepository;
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;
        private readonly IBlobContainer<CandidateContainer> _blobContainer;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly ILogger<PublicJobAppService> _logger;

        public PublicJobAppService(
            IRepository<Job, Guid> jobRepository,
            IRepository<Candidate, Guid> candidateRepository,
            IRepository<Application, Guid> applicationRepository,
            IRepository<Department, Guid> departmentRepository,
            IBlobContainer<CandidateContainer> blobContainer,
            IGuidGenerator guidGenerator,
            IBackgroundJobManager backgroundJobManager,
            ILogger<PublicJobAppService> logger)
        {
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _applicationRepository = applicationRepository;
            _departmentRepository = departmentRepository;
            _blobContainer = blobContainer;
            _guidGenerator = guidGenerator;
            _backgroundJobManager = backgroundJobManager;
            _logger = logger;
        }

        public async Task<PublicJobDto> GetBySlugAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new UserFriendlyException("Invalid job link");

            var queryable = await _jobRepository.GetQueryableAsync();
            var job = await AsyncExecuter.FirstOrDefaultAsync(
                queryable.Where(j => 
                    j.PublicSlug == slug && 
                    j.Status == JobStatus.Active && 
                    j.IsPubliclyVisible));

            if (job == null)
                throw new UserFriendlyException("Job not found or no longer available");

            // Increment view count
            job.IncrementViewCount();
            await _jobRepository.UpdateAsync(job);

            // Map to DTO
            var dto = ObjectMapper.Map<Job, PublicJobDto>(job);

            // Deserialize skills
            if (!string.IsNullOrEmpty(job.RequiredSkillsJson))
                dto.RequiredSkills = JsonSerializer.Deserialize<List<string>>(job.RequiredSkillsJson) ?? new List<string>();
            
            if (!string.IsNullOrEmpty(job.PreferredSkillsJson))
                dto.PreferredSkills = JsonSerializer.Deserialize<List<string>>(job.PreferredSkillsJson) ?? new List<string>();

            // Get department name
            var department = await _departmentRepository.GetAsync(job.DepartmentId);
            dto.DepartmentName = department.Name;

            return dto;
        }

        public async Task<List<PublicJobDto>> GetActivePublicJobsAsync()
        {
            var queryable = await _jobRepository.GetQueryableAsync();
            var jobs = await AsyncExecuter.ToListAsync(
                queryable
                    .Where(j => j.Status == JobStatus.Active && j.IsPubliclyVisible)
                    .OrderByDescending(j => j.PostedDate)
                    .Take(100));

            var dtos = ObjectMapper.Map<List<Job>, List<PublicJobDto>>(jobs);

            // Get department names
            var departmentIds = jobs.Select(j => j.DepartmentId).Distinct().ToList();
            var departments = await _departmentRepository.GetListAsync(d => departmentIds.Contains(d.Id));
            var departmentDict = departments.ToDictionary(d => d.Id, d => d.Name);

            foreach (var dto in dtos)
            {
                var job = jobs.First(j => j.Id == dto.Id);
                dto.DepartmentName = departmentDict.GetValueOrDefault(job.DepartmentId) ?? "";

                // Deserialize skills
                if (!string.IsNullOrEmpty(job.RequiredSkillsJson))
                    dto.RequiredSkills = JsonSerializer.Deserialize<List<string>>(job.RequiredSkillsJson) ?? new List<string>();
                
                if (!string.IsNullOrEmpty(job.PreferredSkillsJson))
                    dto.PreferredSkills = JsonSerializer.Deserialize<List<string>>(job.PreferredSkillsJson) ?? new List<string>();
            }

            return dtos;
        }

        public async Task<Guid> SubmitApplicationAsync(PublicJobApplicationDto input)
        {
            // Validate job exists and is active
            var job = await _jobRepository.GetAsync(input.JobId);
            if (job.Status != JobStatus.Active || !job.IsPubliclyVisible)
                throw new UserFriendlyException("This job is no longer accepting applications");

            if (job.ClosingDate.HasValue && job.ClosingDate.Value < DateTime.UtcNow)
                throw new UserFriendlyException("The application deadline for this job has passed");

            // Check if candidate already applied to this job
            var existingApplication = await _applicationRepository.FirstOrDefaultAsync(a => 
                a.JobId == input.JobId && 
                a.Candidate.Email == input.Email);

            if (existingApplication != null)
                throw new UserFriendlyException("You have already applied to this job");

            // Find or create candidate
            var candidate = await _candidateRepository.FirstOrDefaultAsync(c => c.Email == input.Email);
            
            if (candidate == null)
            {
                // Create new candidate
                candidate = new Candidate(
                    _guidGenerator.Create(),
                    input.FirstName,
                    input.LastName,
                    input.Email,
                    input.Phone ?? string.Empty);

                if (!string.IsNullOrWhiteSpace(input.CurrentJobTitle))
                    candidate.CurrentJobTitle = input.CurrentJobTitle;
                if (!string.IsNullOrWhiteSpace(input.CurrentCompany))
                    candidate.CurrentCompany = input.CurrentCompany;
                candidate.YearsOfExperience = input.YearsOfExperience;
                if (!string.IsNullOrWhiteSpace(input.City))
                    candidate.City = input.City;
                if (!string.IsNullOrWhiteSpace(input.State))
                    candidate.State = input.State;
                if (!string.IsNullOrWhiteSpace(input.Country))
                    candidate.Country = input.Country;
                if (!string.IsNullOrWhiteSpace(input.LinkedInUrl))
                    candidate.LinkedInUrl = input.LinkedInUrl;
                if (!string.IsNullOrWhiteSpace(input.GitHubUrl))
                    candidate.GitHubUrl = input.GitHubUrl;
                if (!string.IsNullOrWhiteSpace(input.PortfolioUrl))
                    candidate.PortfolioUrl = input.PortfolioUrl;

                // Store skills, education, and experience as JSON
                if (input.Skills.Any())
                    candidate.SkillsJson = JsonSerializer.Serialize(input.Skills);

                if (!string.IsNullOrWhiteSpace(input.EducationSummary))
                    candidate.EducationJson = JsonSerializer.Serialize(new { Summary = input.EducationSummary });

                if (!string.IsNullOrWhiteSpace(input.ExperienceSummary))
                    candidate.ExperienceJson = JsonSerializer.Serialize(new { Summary = input.ExperienceSummary });

                // Grant GDPR consent
                if (input.ConsentToProcess)
                    candidate.GrantConsent();

                candidate = await _candidateRepository.InsertAsync(candidate, autoSave: true);
            }
            else
            {
                // Update existing candidate with new information if provided
                if (!string.IsNullOrWhiteSpace(input.Phone) && string.IsNullOrWhiteSpace(candidate.PhoneNumber))
                    candidate.PhoneNumber = input.Phone;
                
                if (!string.IsNullOrWhiteSpace(input.LinkedInUrl) && string.IsNullOrWhiteSpace(candidate.LinkedInUrl))
                    candidate.LinkedInUrl = input.LinkedInUrl;

                await _candidateRepository.UpdateAsync(candidate, autoSave: true);
            }

            // Save resume to blob storage if provided
            if (!string.IsNullOrWhiteSpace(input.ResumeContentBase64) && !string.IsNullOrWhiteSpace(input.ResumeFileName))
            {
                try
                {
                    var resumeBytes = Convert.FromBase64String(input.ResumeContentBase64);
                    
                    // Validate file size (5MB max)
                    if (resumeBytes.Length > 5 * 1024 * 1024)
                        throw new UserFriendlyException("Resume file size must not exceed 5MB");

                    var blobName = $"{candidate.Id}/{_guidGenerator.Create()}-{input.ResumeFileName}";
                    await _blobContainer.SaveAsync(blobName, resumeBytes, overrideExisting: true);
                    
                    candidate.ResumeUrl = blobName;
                    await _candidateRepository.UpdateAsync(candidate);
                }
                catch (FormatException)
                {
                    throw new UserFriendlyException("Invalid resume file format");
                }
            }

            // Create application
            var application = new Application(
                _guidGenerator.Create(),
                input.JobId,
                candidate.Id);

            if (!string.IsNullOrWhiteSpace(input.CoverLetter))
                application.CoverLetter = input.CoverLetter;
            application.Status = ApplicationStatus.New;
            application.Stage = PipelineStage.Applied;

            application = await _applicationRepository.InsertAsync(application, autoSave: true);

            // Increment job application count
            job.IncrementApplicationCount();
            await _jobRepository.UpdateAsync(job);

            // Enqueue AI analysis job
            try
            {
                await _backgroundJobManager.EnqueueAsync(new AnalyzeApplicationJobArgs
                {
                    ApplicationId = application.Id
                });
                _logger.LogInformation("Enqueued AI analysis for Application {ApplicationId}", application.Id);
            }
            catch (Exception ex)
            {
                // Don't fail the application submission if background job fails
                _logger.LogError(ex, "Failed to enqueue AI analysis for Application {ApplicationId}", application.Id);
            }

            return application.Id;
        }
    }
}

