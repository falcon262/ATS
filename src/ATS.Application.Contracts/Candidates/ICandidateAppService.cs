using ATS.Candidates.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ATS.Candidates
{
    // ========== CANDIDATE SERVICE INTERFACE ==========
    public interface ICandidateAppService : IApplicationService
    {
        Task<CandidateDto> GetAsync(Guid id);
        Task<PagedResultDto<CandidateListDto>> GetListAsync(GetCandidateListInput input);
        Task<CandidateDto> CreateAsync(CreateUpdateCandidateDto input);
        Task<CandidateDto> UpdateAsync(Guid id, CreateUpdateCandidateDto input);
        Task DeleteAsync(Guid id);
        Task<CandidateDto> UploadResumeAsync(UploadResumeInput input);
        Task<ResumeDownloadDto> DownloadResumeAsync(Guid candidateId);
        Task<List<CandidateListDto>> GetTopCandidatesAsync(int count = 10);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
        Task GrantConsentAsync(Guid id);
        Task RevokeConsentAsync(Guid id);
    }
}
