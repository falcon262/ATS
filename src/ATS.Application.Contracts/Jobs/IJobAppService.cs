using ATS.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ATS.Jobs
{
    // ========== JOB SERVICE INTERFACE ==========
    public interface IJobAppService : IApplicationService
    {
        Task<JobDto> GetAsync(Guid id);
        Task<PagedResultDto<JobListDto>> GetListAsync(GetJobListInput input);
        Task<JobDto> CreateAsync(CreateUpdateJobDto input);
        Task<JobDto> UpdateAsync(Guid id, CreateUpdateJobDto input);
        Task DeleteAsync(Guid id);
        Task<JobDto> PublishAsync(PublishJobInput input);
        Task<JobDto> CloseAsync(Guid id);
        Task<List<JobListDto>> GetActiveJobsAsync();
        Task IncrementViewCountAsync(Guid id);
    }
}
