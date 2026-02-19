using ATS.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ATS.Interviews
{
    /// <summary>
    /// Interview scheduling and tracking
    /// </summary>
    public class Interview : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public InterviewType Type { get; set; }

        public InterviewStatus Status { get; set; }

        // Schedule
        public DateTime ScheduledDate { get; set; }
        public int DurationMinutes { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        [MaxLength(500)]
        public string MeetingLink { get; set; } // For virtual interviews

        // Interviewers (JSON array of objects with Id, Name, Email)
        public string InterviewersJson { get; set; }

        // Interview guide/questions (JSON)
        public string QuestionsJson { get; set; }

        // Feedback
        [MaxLength(2000)]
        public string Feedback { get; set; }

        public int? Rating { get; set; } // 1-5

        // Evaluation scores (JSON)
        public string EvaluationScoresJson { get; set; }

        public InterviewDecision? Decision { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        // Attachments (JSON array of URLs)
        public string AttachmentsJson { get; set; }

        protected Interview()
        {
        }

        public Interview(Guid id, Guid applicationId, string title,
            DateTime scheduledDate, InterviewType type) : base()
        {
            Id = id;
            ApplicationId = applicationId;
            Title = title;
            ScheduledDate = scheduledDate;
            Type = type;
            Status = InterviewStatus.Scheduled;
            DurationMinutes = 60; // Default 1 hour
        }

        public void Complete(string feedback, int rating, InterviewDecision decision)
        {
            Status = InterviewStatus.Completed;
            Feedback = feedback;
            Rating = rating;
            Decision = decision;
        }

        public void Cancel(string reason)
        {
            Status = InterviewStatus.Cancelled;
            Notes = reason;
        }

        public void Reschedule(DateTime newDate)
        {
            ScheduledDate = newDate;
            Status = InterviewStatus.Rescheduled;
        }
    }

    public enum InterviewType
    {
        PhoneScreen = 0,
        VideoCall = 1,
        InPerson = 2,
        Technical = 3,
        Behavioral = 4,
        Panel = 5,
        Final = 6
    }

    public enum InterviewStatus
    {
        Scheduled = 0,
        Rescheduled = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        NoShow = 5
    }

    public enum InterviewDecision
    {
        StrongYes = 0,
        Yes = 1,
        Maybe = 2,
        No = 3,
        StrongNo = 4
    }
}
