using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Departments.Dtos
{
    public class CreateUpdateDepartmentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public Guid? HeadId { get; set; }

        [StringLength(100)]
        public string HeadName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string HeadEmail { get; set; }

        public Guid? ParentDepartmentId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
