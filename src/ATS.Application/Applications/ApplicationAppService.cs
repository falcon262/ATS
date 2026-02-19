using ATS.Applications.Dtos;
using ATS.Candidates;
using ATS.Departments;
using ATS.Jobs;
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
using Volo.Abp.Timing;

namespace ATS.Applications
{
    public class ApplicationAppService : ApplicationService, IApplicationAppService
    {
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly IRepository<Job, Guid> _jobRepository;
        private readonly IRepository<Candidate, Guid> _candidateRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;

        public ApplicationAppService(
            IRepository<Application, Guid> applicationRepository,
            IRepository<Job, Guid> jobRepository,
            IRepository<Candidate, Guid> candidateRepository,
            IRepository<Department, Guid> departmentRepository)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<ApplicationDto> GetAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id, includeDetails: true);
            return await MapToApplicationDtoAsync(application);
        }

        public async Task<PagedResultDto<ApplicationListDto>> GetListAsync(GetApplicationListInput input)
        {
            var queryable = await _applicationRepository.GetQueryableAsync();

            queryable = queryable
                .WhereIf(input.JobId.HasValue, a => a.JobId == input.JobId.Value)
                .WhereIf(input.CandidateId.HasValue, a => a.CandidateId == input.CandidateId.Value)
                .WhereIf(input.Status.HasValue, a => a.Status == input.Status.Value)
                .WhereIf(input.Stage.HasValue, a => a.Stage == input.Stage.Value)
                .WhereIf(input.AssignedToId.HasValue, a => a.AssignedToId == input.AssignedToId.Value)
                .WhereIf(input.MinAIScore.HasValue, a => a.AIScore >= input.MinAIScore.Value)
                .WhereIf(input.AppliedAfter.HasValue, a => a.AppliedDate >= input.AppliedAfter.Value)
                .WhereIf(input.AppliedBefore.HasValue, a => a.AppliedDate <= input.AppliedBefore.Value);

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var applications = await AsyncExecuter.ToListAsync(
                queryable
                    .OrderBy(a => a.AppliedDate)
                    .PageBy(input.SkipCount, input.MaxResultCount)
            );

            var dtos = new List<ApplicationListDto>();

            foreach (var app in applications)
            {
                var job = await _jobRepository.GetAsync(app.JobId);
                var candidate = await _candidateRepository.GetAsync(app.CandidateId);

                var dto = ObjectMapper.Map<Application, ApplicationListDto>(app);
                dto.JobId = app.JobId;
                dto.JobTitle = job.Title;
                dto.CandidateId = app.CandidateId;
                dto.CandidateName = candidate.GetFullName();
                dto.CandidateEmail = candidate.Email;

                dtos.Add(dto);
            }

            return new PagedResultDto<ApplicationListDto>(totalCount, dtos);
        }

        public async Task<ApplicationDto> CreateAsync(CreateApplicationDto input)
        {
            var application = new Application(
                Guid.NewGuid(),
                input.JobId,
                input.CandidateId
                //input.CoverLetter
            );

            if (input.ScreeningAnswers != null && input.ScreeningAnswers.Any())
            {
                application.ScreeningAnswersJson = JsonSerializer.Serialize(input.ScreeningAnswers);
                await _applicationRepository.UpdateAsync(application);
            }

            return await GetAsync(application.Id);
        }

        public async Task<ApplicationDto> UpdateAsync(Guid id, UpdateApplicationDto input)
        {
            var application = await _applicationRepository.GetAsync(id);

            if (input.Status.HasValue)
                application.Status = input.Status.Value;

            if (input.Stage.HasValue)
                application.MoveToStage(input.Stage.Value);

            if (!string.IsNullOrWhiteSpace(input.ReviewNotes))
                application.ReviewNotes = input.ReviewNotes;

            if (input.Rating.HasValue)
                application.Rating = input.Rating.Value;

            if (input.AssignedToId.HasValue)
                application.AssignTo(input.AssignedToId.Value, input.AssignedToName);

            await _applicationRepository.UpdateAsync(application);

            return await GetAsync(application.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _applicationRepository.DeleteAsync(id);
        }

        public async Task<ApplicationDto> MoveToStageAsync(MoveApplicationStageInput input)
        {
            var application = await _applicationRepository.GetAsync(input.ApplicationId);

            application.MoveToStage(input.NewStage);

            if (!string.IsNullOrWhiteSpace(input.Notes))
            {
                application.ReviewNotes = $"{application.ReviewNotes}\n[{Clock.Now}] Stage changed: {input.Notes}";
            }

            await _applicationRepository.UpdateAsync(application);

            // Update rankings if needed
            if (input.NewStage == PipelineStage.Offer)
            {
                await UpdateRankingsAsync(application.JobId);
            }

            return await GetAsync(application.Id);
        }

        public async Task<ApplicationDto> RejectAsync(RejectApplicationInput input)
        {
            var application = await _applicationRepository.GetAsync(input.ApplicationId);

            application.Reject(input.RejectionReason);

            await _applicationRepository.UpdateAsync(application);

            // TODO: Send notification if requested
            if (input.SendNotification)
            {
                // Implement notification logic
            }

            return await GetAsync(application.Id);
        }

        public async Task<ApplicationDto> MakeOfferAsync(MakeOfferInput input)
        {
            var application = await _applicationRepository.GetAsync(input.ApplicationId);

            application.MakeOffer(input.OfferedSalary, input.OfferExpiryDate);

            if (!string.IsNullOrWhiteSpace(input.OfferDetails))
            {
                application.ReviewNotes = $"{application.ReviewNotes}\n[{Clock.Now}] Offer details: {input.OfferDetails}";
            }

            await _applicationRepository.UpdateAsync(application);

            return await GetAsync(application.Id);
        }

        public async Task<ApplicationDto> AcceptOfferAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);
            application.AcceptOffer();
            await _applicationRepository.UpdateAsync(application);

            return await GetAsync(application.Id);
        }

        public async Task<ApplicationDto> DeclineOfferAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);
            application.DeclineOffer();
            await _applicationRepository.UpdateAsync(application);

            return await GetAsync(application.Id);
        }

        public async Task<ApplicationDto> AssignReviewerAsync(Guid id, Guid reviewerId, string reviewerName)
        {
            var application = await _applicationRepository.GetAsync(id);
            application.AssignTo(reviewerId, reviewerName);
            await _applicationRepository.UpdateAsync(application);

            return await GetAsync(application.Id);
        }

        public async Task<Dictionary<string, int>> GetPipelineStatisticsAsync(Guid jobId)
        {
            var queryable = await _applicationRepository.GetQueryableAsync();
            var stats = queryable
                .Where(a => a.JobId == jobId)
                .GroupBy(a => a.Stage)
                .Select(g => new { Stage = g.Key, Count = g.Count() })
                .ToList();

            return stats.ToDictionary(
                kvp => kvp.Stage.ToString(),
                kvp => kvp.Count
            );
        }

        private async Task<ApplicationDto> MapToApplicationDtoAsync(Application application)
        {
            var dto = ObjectMapper.Map<Application, ApplicationDto>(application);

            // Get related data
            var job = await _jobRepository.GetAsync(application.JobId);
            var candidate = await _candidateRepository.GetAsync(application.CandidateId);
            var department = await AsyncExecuter.FirstOrDefaultAsync(
                (await _departmentRepository.GetQueryableAsync())
                .Where(d => d.Id == job.DepartmentId)
            );

            dto.JobTitle = job.Title;
            dto.DepartmentName = department?.Name;
            dto.CandidateName = candidate.GetFullName();
            dto.CandidateEmail = candidate.Email;

            // Deserialize JSON fields
            if (!string.IsNullOrEmpty(application.AIAnalysisDetailsJson))
            {
                dto.AIAnalysisDetails = JsonSerializer.Deserialize<AIAnalysisDetails>(application.AIAnalysisDetailsJson);
            }

            if (!string.IsNullOrEmpty(application.SkillMatchScoresJson))
            {
                dto.SkillMatchScores = JsonSerializer.Deserialize<List<SkillMatchDto>>(application.SkillMatchScoresJson);
            }

            if (!string.IsNullOrEmpty(application.ScreeningAnswersJson))
            {
                dto.ScreeningAnswers = JsonSerializer.Deserialize<Dictionary<string, string>>(application.ScreeningAnswersJson);
            }

            if (!string.IsNullOrEmpty(application.ActivityLogJson))
            {
                dto.ActivityLog = JsonSerializer.Deserialize<List<ActivityLogEntry>>(application.ActivityLogJson);
            }

            return dto;
        }

        private async Task UpdateRankingsAsync(Guid jobId)
        {
            var queryable = await _applicationRepository.GetQueryableAsync();
            var applications = queryable
                .Where(a => a.JobId == jobId)
                .OrderByDescending(a => a.AIScore ?? 0)
                .ToList();

            int rank = 1;
            foreach (var app in applications)
            {
                app.Rank = rank++;
                await _applicationRepository.UpdateAsync(app);
            }
        }
    }
}
