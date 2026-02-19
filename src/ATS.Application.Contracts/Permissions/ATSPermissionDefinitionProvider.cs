using ATS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ATS.Permissions;

public class ATSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ATSPermissions.GroupName);

        // Dashboard Permission (Admin/Recruiter only)
        myGroup.AddPermission(ATSPermissions.Dashboard.Default, L("Permission:Dashboard"));

        // Jobs Management Permissions
        var jobsPermission = myGroup.AddPermission(ATSPermissions.Jobs.Default, L("Permission:Jobs"));
        jobsPermission.AddChild(ATSPermissions.Jobs.Create, L("Permission:Jobs.Create"));
        jobsPermission.AddChild(ATSPermissions.Jobs.Edit, L("Permission:Jobs.Edit"));
        jobsPermission.AddChild(ATSPermissions.Jobs.Delete, L("Permission:Jobs.Delete"));

        // Candidates Management Permissions
        var candidatesPermission = myGroup.AddPermission(ATSPermissions.Candidates.Default, L("Permission:Candidates"));
        candidatesPermission.AddChild(ATSPermissions.Candidates.Create, L("Permission:Candidates.Create"));
        candidatesPermission.AddChild(ATSPermissions.Candidates.Edit, L("Permission:Candidates.Edit"));
        candidatesPermission.AddChild(ATSPermissions.Candidates.Delete, L("Permission:Candidates.Delete"));

        // Applications Management Permissions
        var applicationsPermission = myGroup.AddPermission(ATSPermissions.Applications.Default, L("Permission:Applications"));
        applicationsPermission.AddChild(ATSPermissions.Applications.Edit, L("Permission:Applications.Edit"));
        applicationsPermission.AddChild(ATSPermissions.Applications.Delete, L("Permission:Applications.Delete"));

        // Departments Management Permissions
        var departmentsPermission = myGroup.AddPermission(ATSPermissions.Departments.Default, L("Permission:Departments"));
        departmentsPermission.AddChild(ATSPermissions.Departments.Create, L("Permission:Departments.Create"));
        departmentsPermission.AddChild(ATSPermissions.Departments.Edit, L("Permission:Departments.Edit"));
        departmentsPermission.AddChild(ATSPermissions.Departments.Delete, L("Permission:Departments.Delete"));

        // Pipeline Permission
        myGroup.AddPermission(ATSPermissions.Pipeline.Default, L("Permission:Pipeline"));

        // Candidate Portal Permissions (Candidates only)
        var candidatePortalPermission = myGroup.AddPermission(
            ATSPermissions.CandidatePortal.Default, 
            L("Permission:CandidatePortal"));
        
        candidatePortalPermission.AddChild(
            ATSPermissions.CandidatePortal.ViewApplications, 
            L("Permission:CandidatePortal.ViewApplications"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ATSResource>(name);
    }
}
