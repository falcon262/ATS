using ATS.Applications;
using System;

namespace ATS.Candidates
{
    /// <summary>
    /// DTO for candidate's view of their applications
    /// </summary>
    public class CandidateApplicationListDto
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string? Company { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public PipelineStage Stage { get; set; }
        public decimal? AIScore { get; set; }
        public Guid JobId { get; set; }
    }
}

