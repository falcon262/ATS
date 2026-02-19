using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Departments.Dtos
{
    public class DepartmentListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string HeadName { get; set; }
        public bool IsActive { get; set; }
        public int JobCount { get; set; }
        public int SubDepartmentCount { get; set; }
    }
}
