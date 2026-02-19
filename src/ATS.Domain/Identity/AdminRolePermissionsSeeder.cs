using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace ATS.Identity
{
    /// <summary>
    /// Seeds the "admin" role with all ATS permissions
    /// </summary>
    public class AdminRolePermissionsSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IPermissionManager _permissionManager;

        // All admin permission constants
        private static readonly string[] AdminPermissions = new[]
        {
            "ATS.Dashboard",
            "ATS.Jobs",
            "ATS.Jobs.Create",
            "ATS.Jobs.Edit",
            "ATS.Jobs.Delete",
            "ATS.Candidates",
            "ATS.Candidates.Create",
            "ATS.Candidates.Edit",
            "ATS.Candidates.Delete",
            "ATS.Applications",
            "ATS.Applications.Edit",
            "ATS.Applications.Delete",
            "ATS.Departments",
            "ATS.Departments.Create",
            "ATS.Departments.Edit",
            "ATS.Departments.Delete",
            "ATS.Pipeline"
        };

        public AdminRolePermissionsSeeder(
            IIdentityRoleRepository roleRepository,
            IPermissionManager permissionManager)
        {
            _roleRepository = roleRepository;
            _permissionManager = permissionManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Find admin role (it should exist from ABP seed data)
            var adminRole = await _roleRepository.FindByNormalizedNameAsync("ADMIN");
            
            if (adminRole == null)
            {
                // If somehow admin role doesn't exist, return
                // (ABP should have created it already)
                return;
            }

            // Grant all admin permissions
            foreach (var permission in AdminPermissions)
            {
                await _permissionManager.SetForRoleAsync(
                    adminRole.Name,
                    permission,
                    true);
            }
        }
    }
}

