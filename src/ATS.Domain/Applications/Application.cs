using ATS.Candidates;
using ATS.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace ATS.Applications
{
    /// <summary>
    /// Represents a job application linking a candidate to a job
    /// </summary>
    public class Application : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        // Foreign Keys
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }

        public Guid CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }

        // Application details
        public DateTime AppliedDate { get; set; }

        public ApplicationStatus Status { get; set; }

        public PipelineStage Stage { get; set; }

        // AI Analysis
        public decimal? AIScore { get; set; }

        [MaxLength(1000)]
        public string? AIMatchSummary { get; set; }

        // JSON for detailed AI analysis
        public string? AIAnalysisDetailsJson { get; set; }

        // Skill matching scores (JSON)
        public string? SkillMatchScoresJson { get; set; }

        // Ranking
        public int? Rank { get; set; }

        // Cover letter specific to this application
        [MaxLength(5000)]
        public string? CoverLetter { get; set; }

        // Answers to screening questions (JSON)
        public string? ScreeningAnswersJson { get; set; }

        // Reviewer assignment
        public Guid? AssignedToId { get; set; }

        [MaxLength(100)]
        public string? AssignedToName { get; set; }

        public DateTime? AssignedDate { get; set; }

        // Review and feedback
        [MaxLength(2000)]
        public string? ReviewNotes { get; set; }

        public int? Rating { get; set; } // 1-5 star rating

        // Interview scheduling
        public DateTime? InterviewDate { get; set; }

        [MaxLength(200)]
        public string? InterviewLocation { get; set; }

        [MaxLength(500)]
        public string? InterviewNotes { get; set; }

        // Rejection reason (if rejected)
        [MaxLength(500)]
        public string? RejectionReason { get; set; }

        public DateTime? RejectedDate { get; set; }

        // Offer details (if offered)
        public decimal? OfferedSalary { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? OfferExpiryDate { get; set; }
        public bool? OfferAccepted { get; set; }

        // Timeline tracking
        public DateTime? ScreeningCompletedDate { get; set; }
        public DateTime? FirstInterviewDate { get; set; }
        public DateTime? FinalInterviewDate { get; set; }
        public DateTime? DecisionDate { get; set; }

        // Activity log (stored as JSON)
        public string? ActivityLogJson { get; set; }

        // Email communication tracking
        public int EmailsSent { get; set; }
        public DateTime? LastEmailDate { get; set; }

        protected Application()
        {
        }

        public Application(Guid id, Guid jobId, Guid candidateId) : base(id)
        {
            JobId = jobId;
            CandidateId = candidateId;
            AppliedDate = DateTime.UtcNow;
            Status = ApplicationStatus.New;
            Stage = PipelineStage.Applied;
            EmailsSent = 0;
        }

        public void MoveToStage(PipelineStage newStage)
        {
            Stage = newStage;

            // Update corresponding dates
            switch (newStage)
            {
                case PipelineStage.Screening:
                    Status = ApplicationStatus.InReview;
                    break;
                case PipelineStage.FirstInterview:
                    FirstInterviewDate = DateTime.UtcNow;
                    ScreeningCompletedDate = DateTime.UtcNow;
                    break;
                case PipelineStage.FinalInterview:
                    FinalInterviewDate = DateTime.UtcNow;
                    break;
                case PipelineStage.Offer:
                    Status = ApplicationStatus.Offered;
                    OfferDate = DateTime.UtcNow;
                    DecisionDate = DateTime.UtcNow;
                    break;
                case PipelineStage.Hired:
                    Status = ApplicationStatus.Hired;
                    break;
                case PipelineStage.Rejected:
                    Status = ApplicationStatus.Rejected;
                    RejectedDate = DateTime.UtcNow;
                    break;
            }
        }

        public void SetAIScore(decimal score, string summary)
        {
            AIScore = Math.Min(100, Math.Max(0, score));
            AIMatchSummary = summary;
        }

        public void AssignTo(Guid userId, string userName)
        {
            AssignedToId = userId;
            AssignedToName = userName;
            AssignedDate = DateTime.UtcNow;
            Status = ApplicationStatus.InReview;
        }

        public void Reject(string reason)
        {
            Status = ApplicationStatus.Rejected;
            Stage = PipelineStage.Rejected;
            RejectionReason = reason;
            RejectedDate = DateTime.UtcNow;
        }

        public void MakeOffer(decimal salary, DateTime expiryDate)
        {
            Status = ApplicationStatus.Offered;
            Stage = PipelineStage.Offer;
            OfferedSalary = salary;
            OfferDate = DateTime.UtcNow;
            OfferExpiryDate = expiryDate;
        }

        public void AcceptOffer()
        {
            OfferAccepted = true;
            Status = ApplicationStatus.Hired;
            Stage = PipelineStage.Hired;
        }

        public void DeclineOffer()
        {
            OfferAccepted = false;
            Status = ApplicationStatus.Rejected;
            Stage = PipelineStage.Rejected;
            RejectionReason = "Candidate declined offer";
        }
    }

}
