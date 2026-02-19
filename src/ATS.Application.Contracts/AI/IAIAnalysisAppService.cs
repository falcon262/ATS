using ATS.AI.Dtos;
using ATS.Applications.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ATS.AI
{
    // ========== AI SERVICE INTERFACE ==========
    public interface IAIAnalysisAppService : IApplicationService
    {
        Task<AIAnalysisResultDto> AnalyzeApplicationAsync(AIAnalysisRequestDto input);
        Task<List<AIAnalysisResultDto>> BatchAnalyzeAsync(BatchAIAnalysisDto input);
        Task<List<ApplicationListDto>> GetRankedApplicationsAsync(AIRankingRequestDto input);
        Task UpdateRankingsAsync(Guid jobId);
    }
}
