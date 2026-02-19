using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace ATS.Departments
{
    public class DepartmentDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Department, Guid> _departmentRepository;
        private readonly IGuidGenerator _guidGenerator;

        public DepartmentDataSeeder(
            IRepository<Department, Guid> departmentRepository,
            IGuidGenerator guidGenerator)
        {
            _departmentRepository = departmentRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _departmentRepository.GetCountAsync() > 0)
            {
                return;
            }

            var departments = new List<Department>
            {
                new Department(_guidGenerator.Create(), "Engineering", "ENG")
                {
                    Description = "Responsible for software development, DevOps, and technical innovation",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Product", "PRD")
                {
                    Description = "Product management and strategy",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Design", "DSG")
                {
                    Description = "UI/UX design, graphic design, and brand identity",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Marketing", "MKT")
                {
                    Description = "Marketing, communications, and brand management",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Sales", "SLS")
                {
                    Description = "Sales operations and business development",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Human Resources", "HR")
                {
                    Description = "Talent acquisition, employee relations, and HR operations",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Finance", "FIN")
                {
                    Description = "Financial planning, accounting, and administration",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Customer Success", "CS")
                {
                    Description = "Customer support, success, and satisfaction",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Operations", "OPS")
                {
                    Description = "Business operations and process management",
                    IsActive = true
                },
                new Department(_guidGenerator.Create(), "Legal", "LEG")
                {
                    Description = "Legal compliance and contract management",
                    IsActive = true
                }
            };

            await _departmentRepository.InsertManyAsync(departments, autoSave: true);
        }
    }
}

