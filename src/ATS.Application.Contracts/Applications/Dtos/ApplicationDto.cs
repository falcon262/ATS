using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Applications.Dtos
{
    public class ApplicationDto : FullAuditedEntityDto<Guid>
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public Guid CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public PipelineStage Stage { get; set; }
        public decimal? AIScore { get; set; }
        public string AIMatchSummary { get; set; }
        public AIAnalysisDetails AIAnalysisDetails { get; set; }
        public List<SkillMatchDto> SkillMatchScores { get; set; }
        public int? Rank { get; set; }
        public string CoverLetter { get; set; }
        public Dictionary<string, string> ScreeningAnswers { get; set; }
        public Guid? AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string ReviewNotes { get; set; }
        public int? Rating { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string InterviewLocation { get; set; }
        public string InterviewNotes { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? RejectedDate { get; set; }
        public decimal? OfferedSalary { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? OfferExpiryDate { get; set; }
        public bool? OfferAccepted { get; set; }
        public List<ActivityLogEntry> ActivityLog { get; set; }
    }
}
