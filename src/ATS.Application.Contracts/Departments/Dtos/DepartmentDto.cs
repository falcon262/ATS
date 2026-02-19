using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Departments.Dtos
{
    public class DepartmentDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid? HeadId { get; set; }
        public string HeadName { get; set; }
        public string HeadEmail { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public string ParentDepartmentName { get; set; }
        public bool IsActive { get; set; }
        public int JobCount { get; set; }
        public List<DepartmentDto> SubDepartments { get; set; }
    }
}
