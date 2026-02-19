using ATS.Departments;
using ATS.Jobs.Dtos;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace ATS.Jobs
{
    public class JobAppService : ApplicationService, IJobAppService
    {
        private readonly IRepository<Job, Guid> _jobRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;
        private readonly JobManager _jobManager;

        public JobAppService(
            IRepository<Job, Guid> jobRepository,
            IRepository<Department, Guid> departmentRepository,
            JobManager jobManager)
        {
            _jobRepository = jobRepository;
            _departmentRepository = departmentRepository;
            _jobManager = jobManager;
        }

        public async Task<JobDto> GetAsync(Guid id)
        {
            var job = await _jobRepository.GetAsync(id, includeDetails: false);
            var jobDto = ObjectMapper.Map<Job, JobDto>(job);

            // Map JSON fields
            if (!string.IsNullOrEmpty(job.RequiredSkillsJson))
                jobDto.RequiredSkills = JsonSerializer.Deserialize<List<string>>(job.RequiredSkillsJson);
            if (!string.IsNullOrEmpty(job.PreferredSkillsJson))
                jobDto.PreferredSkills = JsonSerializer.Deserialize<List<string>>(job.PreferredSkillsJson);

            // Get department name
            var department = await _departmentRepository.FindAsync(job.DepartmentId);
            jobDto.DepartmentName = department?.Name ?? "Unknown";

            return jobDto;
        }

        public async Task<PagedResultDto<JobListDto>> GetListAsync(GetJobListInput input)
        {
            var queryable = await _jobRepository.GetQueryableAsync();

            // Apply filters
            queryable = queryable
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    j => j.Title.Contains(input.Filter) || j.Description.Contains(input.Filter))
                .WhereIf(input.DepartmentId.HasValue, j => j.DepartmentId == input.DepartmentId.Value)
                .WhereIf(input.Status.HasValue, j => j.Status == input.Status.Value)
                .WhereIf(input.EmploymentType.HasValue, j => j.EmploymentType == input.EmploymentType.Value)
                .WhereIf(input.ExperienceLevel.HasValue, j => j.ExperienceLevel == input.ExperienceLevel.Value)
                .WhereIf(input.IsRemote.HasValue, j => j.IsRemote == input.IsRemote.Value)
                .WhereIf(input.PostedAfter.HasValue, j => j.PostedDate >= input.PostedAfter.Value)
                .WhereIf(input.PostedBefore.HasValue, j => j.PostedDate <= input.PostedBefore.Value);

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var jobs = await AsyncExecuter.ToListAsync(
                queryable
                    .OrderBy(j => j.PostedDate)
                    .PageBy(input.SkipCount, input.MaxResultCount)
            );

            var jobListDtos = ObjectMapper.Map<List<Job>, List<JobListDto>>(jobs);

            // Get department names
            var departmentIds = jobs.Select(j => j.DepartmentId).Distinct().ToList();
            var departments = await _departmentRepository.GetListAsync(d => departmentIds.Contains(d.Id));
            var departmentDict = departments.ToDictionary(d => d.Id, d => d.Name);

            foreach (var dto in jobListDtos)
            {
                var job = jobs.First(j => j.Id == dto.Id);
                dto.DepartmentName = departmentDict.GetValueOrDefault(job.DepartmentId);
            }

            return new PagedResultDto<JobListDto>(totalCount, jobListDtos);
        }

        public async Task<JobDto> CreateAsync(CreateUpdateJobDto input)
        {
            // Validate department exists
            await _departmentRepository.GetAsync(input.DepartmentId);

            var job = new Job(
                GuidGenerator.Create(),
                input.Title,
                input.Description,
                input.DepartmentId,
                input.EmploymentType,
                input.Location
            );

            job.Requirements = input.Requirements;
            job.Responsibilities = input.Responsibilities;
            job.Benefits = input.Benefits;
            job.IsRemote = input.IsRemote;
            job.ExperienceLevel = input.ExperienceLevel;
            job.MinSalary = input.MinSalary;
            job.MaxSalary = input.MaxSalary;
            job.Currency = input.Currency;
            job.ClosingDate = input.ClosingDate;
            job.HiringManagerId = input.HiringManagerId;
            job.HiringManagerName = input.HiringManagerName;
            job.HiringManagerEmail = input.HiringManagerEmail;

            // Serialize skills to JSON
            job.RequiredSkillsJson = input.RequiredSkills != null && input.RequiredSkills.Count > 0 
                ? JsonSerializer.Serialize(input.RequiredSkills) 
                : "[]";
            job.PreferredSkillsJson = input.PreferredSkills != null && input.PreferredSkills.Count > 0 
                ? JsonSerializer.Serialize(input.PreferredSkills) 
                : "[]";

            job = await _jobRepository.InsertAsync(job, autoSave: true);

            // Map to DTO and include department name
            var jobDto = ObjectMapper.Map<Job, JobDto>(job);
            
            // Map JSON fields
            if (!string.IsNullOrEmpty(job.RequiredSkillsJson))
                jobDto.RequiredSkills = JsonSerializer.Deserialize<List<string>>(job.RequiredSkillsJson);
            if (!string.IsNullOrEmpty(job.PreferredSkillsJson))
                jobDto.PreferredSkills = JsonSerializer.Deserialize<List<string>>(job.PreferredSkillsJson);

            // Get department name
            var department = await _departmentRepository.FindAsync(job.DepartmentId);
            jobDto.DepartmentName = department?.Name ?? "Unknown";

            return jobDto;
        }

        public async Task<JobDto> UpdateAsync(Guid id, CreateUpdateJobDto input)
        {
            var job = await _jobRepository.GetAsync(id);

            job.Title = input.Title;
            job.Description = input.Description;
            job.Requirements = input.Requirements;
            job.Responsibilities = input.Responsibilities;
            job.Benefits = input.Benefits;
            job.DepartmentId = input.DepartmentId;
            job.Location = input.Location;
            job.IsRemote = input.IsRemote;
            job.EmploymentType = input.EmploymentType;
            job.ExperienceLevel = input.ExperienceLevel;
            job.MinSalary = input.MinSalary;
            job.MaxSalary = input.MaxSalary;
            job.Currency = input.Currency;
            job.ClosingDate = input.ClosingDate;
            job.HiringManagerId = input.HiringManagerId;
            job.HiringManagerName = input.HiringManagerName;
            job.HiringManagerEmail = input.HiringManagerEmail;

            // Serialize skills to JSON (handle nulls like CreateAsync does)
            job.RequiredSkillsJson = input.RequiredSkills != null && input.RequiredSkills.Count > 0 
                ? JsonSerializer.Serialize(input.RequiredSkills) 
                : "[]";
            job.PreferredSkillsJson = input.PreferredSkills != null && input.PreferredSkills.Count > 0 
                ? JsonSerializer.Serialize(input.PreferredSkills) 
                : "[]";

            await _jobRepository.UpdateAsync(job, autoSave: true);

            return await GetAsync(job.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _jobRepository.DeleteAsync(id);
        }

        public async Task<JobDto> PublishAsync(PublishJobInput input)
        {
            var job = await _jobRepository.GetAsync(input.JobId);

            // Ensure slug exists before publishing
            _jobManager.EnsureSlugExists(job);

            job.Publish();

            if (input.ScheduledDate.HasValue)
            {
                job.PostedDate = input.ScheduledDate.Value;
            }

            await _jobRepository.UpdateAsync(job);

            return await GetAsync(job.Id);
        }

        public async Task<JobDto> CloseAsync(Guid id)
        {
            var job = await _jobRepository.GetAsync(id);
            job.Close();
            await _jobRepository.UpdateAsync(job);

            return await GetAsync(job.Id);
        }

        public async Task<List<JobListDto>> GetActiveJobsAsync()
        {
            // Get jobs with Status == JobStatus.Active (assuming JobStatus.Active exists)
            var queryable = await _jobRepository.GetQueryableAsync();
            var activeJobs = await AsyncExecuter.ToListAsync(
                queryable
                    .Where(j => j.Status == JobStatus.Active)
                    .OrderByDescending(j => j.PostedDate)
                    .Take(100)
            );
            return ObjectMapper.Map<List<Job>, List<JobListDto>>(activeJobs);
        }

        public async Task IncrementViewCountAsync(Guid id)
        {
            var job = await _jobRepository.GetAsync(id);
            job.IncrementViewCount();
            await _jobRepository.UpdateAsync(job);
        }
    }
}
