using ATS.Applications.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ATS.Applications
{
    // ========== APPLICATION SERVICE INTERFACE ==========
    public interface IApplicationAppService : IApplicationService
    {
        Task<ApplicationDto> GetAsync(Guid id);
        Task<PagedResultDto<ApplicationListDto>> GetListAsync(GetApplicationListInput input);
        Task<ApplicationDto> CreateAsync(CreateApplicationDto input);
        Task<ApplicationDto> UpdateAsync(Guid id, UpdateApplicationDto input);
        Task DeleteAsync(Guid id);
        Task<ApplicationDto> MoveToStageAsync(MoveApplicationStageInput input);
        Task<ApplicationDto> RejectAsync(RejectApplicationInput input);
        Task<ApplicationDto> MakeOfferAsync(MakeOfferInput input);
        Task<ApplicationDto> AcceptOfferAsync(Guid id);
        Task<ApplicationDto> DeclineOfferAsync(Guid id);
        Task<ApplicationDto> AssignReviewerAsync(Guid id, Guid reviewerId, string reviewerName);
        Task<Dictionary<string, int>> GetPipelineStatisticsAsync(Guid jobId);
    }
}
