using ATS.Candidates.Dtos;
using ATS.Reports.Dtos;
using ATS.Reports.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ATS.Reports
{
    // ========== REPORTS SERVICE INTERFACE ==========
    public interface IReportsAppService : IApplicationService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        Task<RecruitmentMetricsDto> GetRecruitmentMetricsAsync(GetReportInput input);
        Task<FileDto> ExportApplicationsAsync(Guid jobId);
        Task<FileDto> ExportCandidatesAsync(GetCandidateListInput input);
    }
}
