namespace ATS.Permissions;

public static class ATSPermissions
{
    public const string GroupName = "ATS";

    // Admin Dashboard
    public static class Dashboard
    {
        public const string Default = GroupName + ".Dashboard";
    }

    // Jobs Management
    public static class Jobs
    {
        public const string Default = GroupName + ".Jobs";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    // Candidates Management
    public static class Candidates
    {
        public const string Default = GroupName + ".Candidates";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    // Applications Management
    public static class Applications
    {
        public const string Default = GroupName + ".Applications";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    // Departments Management
    public static class Departments
    {
        public const string Default = GroupName + ".Departments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    // Pipeline Management
    public static class Pipeline
    {
        public const string Default = GroupName + ".Pipeline";
    }

    // Candidate Portal (for candidates only)
    public static class CandidatePortal
    {
        public const string Default = GroupName + ".CandidatePortal";
        public const string ViewApplications = Default + ".ViewApplications";
    }
}
