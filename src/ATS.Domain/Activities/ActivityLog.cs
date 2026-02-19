using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ATS.Activities
{
    /// <summary>
    /// Activity tracking for audit and timeline purposes
    /// </summary>
    public class ActivityLog : CreationAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string EntityType { get; set; } // Job, Application, Candidate

        public Guid EntityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Action { get; set; } // Created, Updated, StatusChanged, etc.

        [MaxLength(500)]
        public string Description { get; set; }

        // Old and new values for changes (JSON)
        public string OldValuesJson { get; set; }
        public string NewValuesJson { get; set; }

        // User who performed the action
        public Guid? UserId { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string IpAddress { get; set; }

        [MaxLength(200)]
        public string UserAgent { get; set; }

        protected ActivityLog()
        {
        }

        public ActivityLog(Guid id, string entityType, Guid entityId, string action,
            string description = null) : base()
        {
            Id = id;
            EntityType = entityType;
            EntityId = entityId;
            Action = action;
            Description = description;
        }
    }
}
