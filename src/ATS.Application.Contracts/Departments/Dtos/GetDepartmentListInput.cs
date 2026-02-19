using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ATS.Departments.Dtos
{
    public class GetDepartmentListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public bool? IsActive { get; set; }
        public Guid? ParentDepartmentId { get; set; }
    }
}
