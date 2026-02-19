using ATS.Candidates.Dtos;
using ATS.Candidates.Dtos.Info;
using ATS.Domain.Candidates.BlobContainer;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace ATS.Candidates
{
    public class CandidateAppService : ApplicationService, ICandidateAppService
    {
        private readonly IRepository<Candidate, Guid> _candidateRepository;
        private readonly IBlobContainer<CandidateContainer> _blobContainer;

        public CandidateAppService(
            IRepository<Candidate, Guid> candidateRepository,
            IBlobContainer<CandidateContainer> blobContainer)
        {
            _candidateRepository = candidateRepository;
            _blobContainer = blobContainer;
        }

        public async Task<CandidateDto> GetAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetAsync(id);
            var dto = ObjectMapper.Map<Candidate, CandidateDto>(candidate);

            // Deserialize JSON fields with error handling and legacy format support
            dto.Skills = DeserializeJsonField<List<string>>(candidate.SkillsJson, new List<string>());
            dto.Education = DeserializeEducationJson(candidate.EducationJson);
            dto.Experience = DeserializeExperienceJson(candidate.ExperienceJson);
            dto.Certifications = DeserializeJsonField<List<string>>(candidate.CertificationsJson, new List<string>());
            dto.Tags = DeserializeJsonField<List<string>>(candidate.TagsJson, new List<string>());

            return dto;
        }

        // Fix for CS0019 and CS8629 in GetListAsync method
        public async Task<PagedResultDto<CandidateListDto>> GetListAsync(GetCandidateListInput input)
        {
            var queryable = await _candidateRepository.GetQueryableAsync();

            // Apply filters
            queryable = queryable
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), c =>
                    c.FirstName.Contains(input.Filter) ||
                    c.LastName.Contains(input.Filter) ||
                    c.Email.Contains(input.Filter) ||
                    c.CurrentJobTitle.Contains(input.Filter))
                .WhereIf(input.Status.HasValue, c => c.Status == input.Status.Value)
                .WhereIf(input.MinExperience.HasValue, c => c.YearsOfExperience >= input.MinExperience.Value)
                .WhereIf(input.MaxExperience.HasValue, c => c.YearsOfExperience <= input.MaxExperience.Value)
                .WhereIf(input.MinAIScore.HasValue, c => c.OverallAIScore >= input.MinAIScore.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Source), c => c.Source == input.Source)
                .WhereIf(input.IsOpenToRemote.HasValue, c => c.IsOpenToRemote == input.IsOpenToRemote.Value);

            // Filter by skills
            if (input.Skills != null && input.Skills.Any())
            {
                foreach (var skill in input.Skills)
                {
                    queryable = queryable.Where(c => c.SkillsJson.Contains(skill));
                }
            }

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var candidates = await AsyncExecuter.ToListAsync(
                queryable
                    .OrderBy(c => c.CreationTime)
                    .PageBy(input.SkipCount, input.MaxResultCount)
            );

            var dtos = candidates.Select(c =>
            {
                var dto = ObjectMapper.Map<Candidate, CandidateListDto>(c);
                dto.FullName = c.GetFullName();
                dto.Location = $"{c.City}, {c.State}";

                var skills = DeserializeJsonField<List<string>>(c.SkillsJson, new List<string>());
                dto.TopSkills = skills.Take(3).ToList();

                return dto;
            }).ToList();

            return new PagedResultDto<CandidateListDto>(totalCount, dtos);
        }

        public async Task<CandidateDto> CreateAsync(CreateUpdateCandidateDto input)
        {
            // Check email uniqueness
            if (!await IsEmailUniqueAsync(input.Email))
            {
                throw new BusinessException("Email address is already in use.");
            }

            var candidate = new Candidate(
                GuidGenerator.Create(),
                input.FirstName,
                input.LastName,
                input.Email,
                input.PhoneNumber
            );

            MapToEntity(input, candidate);

            candidate = await _candidateRepository.InsertAsync(candidate);

            return await GetAsync(candidate.Id);
        }

        public async Task<CandidateDto> UpdateAsync(Guid id, CreateUpdateCandidateDto input)
        {
            var candidate = await _candidateRepository.GetAsync(id);

            // Check email uniqueness if changed
            if (candidate.Email != input.Email)
            {
                if (!await IsEmailUniqueAsync(input.Email, id))
                {
                    throw new BusinessException("Email address is already in use.");
                }
            }

            candidate.FirstName = input.FirstName;
            candidate.LastName = input.LastName;
            candidate.Email = input.Email;
            candidate.PhoneNumber = input.PhoneNumber;

            MapToEntity(input, candidate);

            await _candidateRepository.UpdateAsync(candidate);

            return await GetAsync(candidate.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _candidateRepository.DeleteAsync(id);
        }

        public async Task<CandidateDto> UploadResumeAsync(UploadResumeInput input)
        {
            var candidate = await _candidateRepository.GetAsync(input.CandidateId);

            // Save file to blob storage
            var fileName = $"{input.CandidateId}/resume_{DateTime.UtcNow.Ticks}_{input.FileName}";
            await _blobContainer.SaveAsync(fileName, input.FileContent);

            candidate.ResumeUrl = fileName;
            await _candidateRepository.UpdateAsync(candidate);

            return await GetAsync(candidate.Id);
        }

        public async Task<ResumeDownloadDto> DownloadResumeAsync(Guid candidateId)
        {
            var candidate = await _candidateRepository.GetAsync(candidateId);

            if (string.IsNullOrWhiteSpace(candidate.ResumeUrl))
            {
                throw new BusinessException("This candidate has not uploaded a resume.");
            }

            // Get the blob from storage
            var blob = await _blobContainer.GetAsync(candidate.ResumeUrl);

            // Extract filename from the blob path
            var fileName = System.IO.Path.GetFileName(candidate.ResumeUrl);

            // Determine content type based on file extension
            var extension = System.IO.Path.GetExtension(fileName).ToLowerInvariant();
            var contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                _ => "application/octet-stream"
            };

            // Read blob content
            byte[] content;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                await blob.CopyToAsync(memoryStream);
                content = memoryStream.ToArray();
            }

            return new ResumeDownloadDto
            {
                Content = content,
                FileName = fileName,
                ContentType = contentType
            };
        }

        public async Task<List<CandidateListDto>> GetTopCandidatesAsync(int count = 10)
        {
            var queryable = await _candidateRepository.GetQueryableAsync();

            // Filter candidates with OverallAIScore >= 70, order by score descending, take top 'count'
            var candidates = await AsyncExecuter.ToListAsync(
                queryable
                    .Where(c => c.OverallAIScore.HasValue && c.OverallAIScore.Value >= 70)
                    .OrderByDescending(c => c.OverallAIScore)
                    .ThenBy(c => c.CreationTime)
                    .Take(count)
            );

            return ObjectMapper.Map<List<Candidate>, List<CandidateListDto>>(candidates);
        }

        private void MapToEntity(CreateUpdateCandidateDto input, Candidate candidate)
        {
            candidate.AlternatePhone = input.AlternatePhone;
            candidate.CurrentJobTitle = input.CurrentJobTitle;
            candidate.CurrentCompany = input.CurrentCompany;
            candidate.YearsOfExperience = input.YearsOfExperience;
            candidate.City = input.City;
            candidate.State = input.State;
            candidate.Country = input.Country;
            candidate.PostalCode = input.PostalCode;
            candidate.LinkedInUrl = input.LinkedInUrl;
            candidate.GitHubUrl = input.GitHubUrl;
            candidate.PersonalWebsite = input.PersonalWebsite;
            candidate.ExpectedSalary = input.ExpectedSalary;
            candidate.PreferredCurrency = input.PreferredCurrency;
            candidate.IsWillingToRelocate = input.IsWillingToRelocate;
            candidate.IsOpenToRemote = input.IsOpenToRemote;
            candidate.AvailableFrom = input.AvailableFrom;
            candidate.NoticePeriod = input.NoticePeriod;
            candidate.Source = input.Source;
            candidate.ReferredBy = input.ReferredBy;
            candidate.Notes = input.Notes;

            // Serialize lists to JSON
            candidate.SkillsJson = JsonSerializer.Serialize(input.Skills);
            candidate.EducationJson = JsonSerializer.Serialize(input.Education);
            candidate.ExperienceJson = JsonSerializer.Serialize(input.Experience);
            candidate.CertificationsJson = JsonSerializer.Serialize(input.Certifications);
            candidate.TagsJson = JsonSerializer.Serialize(input.Tags);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            var queryable = await _candidateRepository.GetQueryableAsync();
            var exists = queryable.Any(c => c.Email == email && (!excludeId.HasValue || c.Id != excludeId.Value));
            return !exists;
        }

        public Task GrantConsentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RevokeConsentAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Safely deserialize JSON field with fallback to default value
        /// </summary>
        private T DeserializeJsonField<T>(string? json, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(json))
                return defaultValue;

            try
            {
                var result = JsonSerializer.Deserialize<T>(json);
                return result ?? defaultValue;
            }
            catch (JsonException ex)
            {
                Logger.LogWarning(ex, $"Failed to deserialize JSON field. Returning default value. JSON: {json}");
                return defaultValue;
            }
        }

        /// <summary>
        /// Deserialize education JSON, handling both legacy format {"Summary":"..."} and new format [{...}]
        /// </summary>
        private List<EducationInfo> DeserializeEducationJson(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<EducationInfo>();

            try
            {
                // Try to deserialize as array first (new format)
                if (json.TrimStart().StartsWith("["))
                {
                    var result = JsonSerializer.Deserialize<List<EducationInfo>>(json);
                    return result ?? new List<EducationInfo>();
                }

                // Handle legacy format: {"Summary":"BS in Computer Engineering, University Of Ghana"}
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                
                if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("Summary", out var summaryProp))
                {
                    var summary = summaryProp.GetString();
                    if (!string.IsNullOrWhiteSpace(summary))
                    {
                        return new List<EducationInfo>
                        {
                            new EducationInfo
                            {
                                Degree = summary,
                                Institution = "",
                                FieldOfStudy = ""
                            }
                        };
                    }
                }

                return new List<EducationInfo>();
            }
            catch (JsonException ex)
            {
                Logger.LogWarning(ex, $"Failed to deserialize education JSON. JSON: {json}");
                return new List<EducationInfo>();
            }
        }

        /// <summary>
        /// Deserialize experience JSON, handling both legacy format {"Summary":"..."} and new format [{...}]
        /// </summary>
        private List<ExperienceInfo> DeserializeExperienceJson(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new List<ExperienceInfo>();

            try
            {
                // Try to deserialize as array first (new format)
                if (json.TrimStart().StartsWith("["))
                {
                    var result = JsonSerializer.Deserialize<List<ExperienceInfo>>(json);
                    return result ?? new List<ExperienceInfo>();
                }

                // Handle legacy format: {"Summary":"A hardworking..."}
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                
                if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("Summary", out var summaryProp))
                {
                    var summary = summaryProp.GetString();
                    if (!string.IsNullOrWhiteSpace(summary))
                    {
                        return new List<ExperienceInfo>
                        {
                            new ExperienceInfo
                            {
                                Description = summary,
                                Company = "",
                                JobTitle = ""
                            }
                        };
                    }
                }

                return new List<ExperienceInfo>();
            }
            catch (JsonException ex)
            {
                Logger.LogWarning(ex, $"Failed to deserialize experience JSON. JSON: {json}");
                return new List<ExperienceInfo>();
            }
        }
    }
}
