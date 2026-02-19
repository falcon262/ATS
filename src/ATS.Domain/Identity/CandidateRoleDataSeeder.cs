using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace ATS.Identity
{
    /// <summary>
    /// Seeds the "Candidate" role with appropriate permissions
    /// </summary>
    public class CandidateRoleDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IPermissionManager _permissionManager;

        // Permission constants (must match ATSPermissions.cs)
        private const string CandidatePortalPermission = "ATS.CandidatePortal";
        private const string ViewApplicationsPermission = "ATS.CandidatePortal.ViewApplications";

        public CandidateRoleDataSeeder(
            IIdentityRoleRepository roleRepository,
            IPermissionManager permissionManager)
        {
            _roleRepository = roleRepository;
            _permissionManager = permissionManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Check if Candidate role exists
            var candidateRole = await _roleRepository.FindByNormalizedNameAsync("CANDIDATE");
            
            if (candidateRole == null)
            {
                // Create Candidate role
                candidateRole = new IdentityRole(
                    Guid.NewGuid(),
                    "Candidate",
                    tenantId: context.TenantId)
                {
                    IsPublic = true,
                    IsDefault = false
                };

                await _roleRepository.InsertAsync(candidateRole, autoSave: true);
            }

            // Grant permissions to Candidate role
            await _permissionManager.SetForRoleAsync(
                candidateRole.Name,
                CandidatePortalPermission,
                true);

            await _permissionManager.SetForRoleAsync(
                candidateRole.Name,
                ViewApplicationsPermission,
                true);
        }
    }
}

