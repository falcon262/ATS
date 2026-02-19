using ATS.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ATS.Departments
{
    /// <summary>
    /// Represents a department in the organization
    /// </summary>
    public class Department : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        // Department head
        public Guid? HeadId { get; set; }

        [MaxLength(100)]
        public string? HeadName { get; set; }

        [MaxLength(100)]
        public string? HeadEmail { get; set; }

        // Parent department for hierarchical structure
        public Guid? ParentDepartmentId { get; set; }
        public virtual Department ParentDepartment { get; set; }

        public bool IsActive { get; set; }

        // Navigation properties
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Department> SubDepartments { get; set; }

        protected Department()
        {
            Jobs = new HashSet<Job>();
            SubDepartments = new HashSet<Department>();
        }

        public Department(Guid id, string name, string code = null) : base()
        {
            Id = id;
            Name = name;
            Code = code;
            IsActive = true;
            Jobs = new HashSet<Job>();
            SubDepartments = new HashSet<Department>();
        }
    }
}
