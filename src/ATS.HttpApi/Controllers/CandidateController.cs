using ATS.Candidates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace ATS.Controllers
{
    [ApiController]
    [Route("api/app/candidate")]
    public class CandidateController : AbpController
    {
        private readonly ICandidateAppService _candidateAppService;

        public CandidateController(ICandidateAppService candidateAppService)
        {
            _candidateAppService = candidateAppService;
        }

        [HttpGet("{candidateId}/resume")]
        public async Task<IActionResult> DownloadResume(Guid candidateId)
        {
            var result = await _candidateAppService.DownloadResumeAsync(candidateId);
            return File(result.Content, result.ContentType, result.FileName);
        }
    }
}

