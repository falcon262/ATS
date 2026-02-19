using ATS.Departments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ATS.Departments
{
    // ========== DEPARTMENT SERVICE INTERFACE ==========
    public interface IDepartmentAppService : ICrudAppService<
        DepartmentDto,
        Guid,
        GetDepartmentListInput,
        CreateUpdateDepartmentDto>
    {
        Task<List<DepartmentDto>> GetAllActiveAsync();
        Task<List<DepartmentDto>> GetHierarchyAsync();
    }
}
