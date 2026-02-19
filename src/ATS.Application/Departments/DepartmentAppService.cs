using ATS.Departments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ATS.Departments
{
    public class DepartmentAppService : CrudAppService<
        Department,
        DepartmentDto,
        Guid,
        GetDepartmentListInput,
        CreateUpdateDepartmentDto>, IDepartmentAppService
    {
        public DepartmentAppService(IRepository<Department, Guid> repository)
            : base(repository)
        {
        }

        protected override async Task<IQueryable<Department>> CreateFilteredQueryAsync(GetDepartmentListInput input)
        {
            var query = await base.CreateFilteredQueryAsync(input);

            // Filter by search text
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(d =>
                    d.Name.Contains(input.Filter) ||
                    d.Code.Contains(input.Filter) ||
                    d.Description.Contains(input.Filter));
            }

            // Filter by active status
            if (input.IsActive.HasValue)
            {
                query = query.Where(d => d.IsActive == input.IsActive.Value);
            }

            // Filter by parent department
            if (input.ParentDepartmentId.HasValue)
            {
                query = query.Where(d => d.ParentDepartmentId == input.ParentDepartmentId.Value);
            }

            return query;
        }

        public async Task<List<DepartmentDto>> GetAllActiveAsync()
        {
            var departments = await Repository.GetListAsync(d => d.IsActive);
            return ObjectMapper.Map<List<Department>, List<DepartmentDto>>(departments);
        }

        public async Task<List<DepartmentDto>> GetHierarchyAsync()
        {
            // Get all active departments
            var allDepartments = await Repository.GetListAsync(d => d.IsActive);

            // Get root departments (no parent)
            var rootDepartments = allDepartments.Where(d => d.ParentDepartmentId == null).ToList();

            var result = new List<DepartmentDto>();

            foreach (var dept in rootDepartments)
            {
                var deptDto = ObjectMapper.Map<Department, DepartmentDto>(dept);
                LoadSubDepartments(deptDto, allDepartments);
                result.Add(deptDto);
            }

            return result;
        }

        private void LoadSubDepartments(DepartmentDto parentDto, List<Department> allDepartments)
        {
            var children = allDepartments.Where(d => d.ParentDepartmentId == parentDto.Id).ToList();

            if (children.Any())
            {
                parentDto.SubDepartments = ObjectMapper.Map<List<Department>, List<DepartmentDto>>(children);

                foreach (var childDto in parentDto.SubDepartments)
                {
                    LoadSubDepartments(childDto, allDepartments);
                }
            }
        }

        protected override IQueryable<Department> ApplySorting(IQueryable<Department> query, GetDepartmentListInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Sorting))
            {
                return query.OrderBy(d => d.Name);
            }

            return base.ApplySorting(query, input);
        }
    }
}

