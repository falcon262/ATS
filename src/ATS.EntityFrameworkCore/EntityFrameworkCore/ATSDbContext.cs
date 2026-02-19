using ATS.Activities;
using ATS.AI;
using ATS.Applications;
using ATS.Candidates;
using ATS.Departments;
using ATS.Interviews;
using ATS.Jobs;
using ATS.Skills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace ATS.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ATSDbContext :
    AbpDbContext<ATSDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Base Entities
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<AIAnalysisResult> AIAnalysisResults { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<Interview> Interviews { get; set; }

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public ATSDbContext(DbContextOptions<ATSDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        builder.ApplyConfiguration(new JobConfiguration());
        builder.ApplyConfiguration(new CandidateConfiguration());
        builder.ApplyConfiguration(new ApplicationConfiguration());
        builder.ApplyConfiguration(new DepartmentConfiguration());
        builder.ApplyConfiguration(new SkillConfiguration());
        builder.ApplyConfiguration(new AIAnalysisResultConfiguration());
        builder.ApplyConfiguration(new ActivityLogConfiguration());
        builder.ApplyConfiguration(new InterviewConfiguration());

        // Seed data (optional)
        SeedData(builder);

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ATSConsts.DbTablePrefix + "YourEntities", ATSConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }

    private void SeedData(ModelBuilder builder)
    {
        // Seed departments
        var engineeringDeptId = Guid.NewGuid();
        var productDeptId = Guid.NewGuid();
        var designDeptId = Guid.NewGuid();

        builder.Entity<Department>().HasData(
            new Department(engineeringDeptId, "Engineering", "ENG")
            {
                Description = "Software Development Department",
                IsActive = true
            },
            new Department(productDeptId, "Product", "PROD")
            {
                Description = "Product Management Department",
                IsActive = true
            },
            new Department(designDeptId, "Design", "DES")
            {
                Description = "UX/UI Design Department",
                IsActive = true
            }
        );

        // Seed common skills
        builder.Entity<Skill>().HasData(
            // Programming Languages
            new Skill(Guid.NewGuid(), "C#", "Programming") { IsActive = true },
            new Skill(Guid.NewGuid(), "JavaScript", "Programming") { IsActive = true },
            new Skill(Guid.NewGuid(), "TypeScript", "Programming") { IsActive = true },
            new Skill(Guid.NewGuid(), "Python", "Programming") { IsActive = true },
            new Skill(Guid.NewGuid(), "Java", "Programming") { IsActive = true },
            new Skill(Guid.NewGuid(), "SQL", "Programming") { IsActive = true },

            // Frameworks
            new Skill(Guid.NewGuid(), ".NET Core", "Framework") { IsActive = true },
            new Skill(Guid.NewGuid(), "Angular", "Framework") { IsActive = true },
            new Skill(Guid.NewGuid(), "React", "Framework") { IsActive = true },
            new Skill(Guid.NewGuid(), "Vue.js", "Framework") { IsActive = true },
            new Skill(Guid.NewGuid(), "Node.js", "Framework") { IsActive = true },

            // Databases
            new Skill(Guid.NewGuid(), "SQL Server", "Database") { IsActive = true },
            new Skill(Guid.NewGuid(), "PostgreSQL", "Database") { IsActive = true },
            new Skill(Guid.NewGuid(), "MongoDB", "Database") { IsActive = true },
            new Skill(Guid.NewGuid(), "Redis", "Database") { IsActive = true },

            // Cloud
            new Skill(Guid.NewGuid(), "Azure", "Cloud") { IsActive = true },
            new Skill(Guid.NewGuid(), "AWS", "Cloud") { IsActive = true },
            new Skill(Guid.NewGuid(), "Google Cloud", "Cloud") { IsActive = true },
            new Skill(Guid.NewGuid(), "Docker", "DevOps") { IsActive = true },
            new Skill(Guid.NewGuid(), "Kubernetes", "DevOps") { IsActive = true },

            // Soft Skills
            new Skill(Guid.NewGuid(), "Leadership", "Soft Skill") { IsActive = true },
            new Skill(Guid.NewGuid(), "Communication", "Soft Skill") { IsActive = true },
            new Skill(Guid.NewGuid(), "Problem Solving", "Soft Skill") { IsActive = true },
            new Skill(Guid.NewGuid(), "Team Collaboration", "Soft Skill") { IsActive = true },
            new Skill(Guid.NewGuid(), "Agile", "Methodology") { IsActive = true },
            new Skill(Guid.NewGuid(), "Scrum", "Methodology") { IsActive = true }
        );
    }
}

#region Configurations
public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.Title);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.DepartmentId);
        builder.HasIndex(x => new { x.Status, x.PostedDate });
        builder.HasIndex(x => x.PublicSlug).IsUnique().HasFilter("[PublicSlug] IS NOT NULL");
        builder.HasIndex(x => x.IsPubliclyVisible);

        // Properties
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.PublicSlug)
            .HasMaxLength(250);

        builder.Property(x => x.MinSalary)
            .HasPrecision(18, 2);

        builder.Property(x => x.MaxSalary)
            .HasPrecision(18, 2);

        // JSON columns (for SQL Server, these will be nvarchar(max))
        builder.Property(x => x.RequiredSkillsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.PreferredSkillsJson)
            .HasColumnType("nvarchar(max)");

        // Relationships
        builder.HasOne(x => x.Department)
            .WithMany(d => d.Jobs)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Applications)
            .WithOne(a => a.Job)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("Candidates");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.LastName);
        builder.HasIndex(x => new { x.FirstName, x.LastName });
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.OverallAIScore);

        // Properties
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.ExpectedSalary)
            .HasPrecision(18, 2);

        builder.Property(x => x.OverallAIScore)
            .HasPrecision(5, 2);

        // JSON columns
        builder.Property(x => x.SkillsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.EducationJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ExperienceJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.CertificationsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.AIAnalysisJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.TagsJson)
            .HasColumnType("nvarchar(max)");

        // Relationships
        builder.HasMany(x => x.Applications)
            .WithOne(a => a.Candidate)
            .HasForeignKey(a => a.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => new { x.JobId, x.CandidateId }).IsUnique();
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Stage);
        builder.HasIndex(x => x.AIScore);
        builder.HasIndex(x => x.AppliedDate);
        builder.HasIndex(x => new { x.JobId, x.Status });
        builder.HasIndex(x => new { x.CandidateId, x.Status });

        // Properties
        builder.Property(x => x.AIScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.OfferedSalary)
            .HasPrecision(18, 2);

        builder.Property(x => x.CoverLetter)
            .HasMaxLength(5000);

        // JSON columns
        builder.Property(x => x.AIAnalysisDetailsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.SkillMatchScoresJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ScreeningAnswersJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ActivityLogJson)
            .HasColumnType("nvarchar(max)");

        // Relationships
        builder.HasOne(x => x.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Candidate)
            .WithMany(c => c.Applications)
            .HasForeignKey(x => x.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Code).IsUnique().HasFilter("[Code] IS NOT NULL");
        builder.HasIndex(x => x.IsActive);

        // Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Code)
            .HasMaxLength(50);

        // Self-referencing relationship for hierarchy
        builder.HasOne(x => x.ParentDepartment)
            .WithMany(d => d.SubDepartments)
            .HasForeignKey(x => x.ParentDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Jobs)
            .WithOne(j => j.Department)
            .HasForeignKey(j => j.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.UsageCount);

        // Properties
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Category)
            .HasMaxLength(50);

        builder.Property(x => x.SynonymsJson)
            .HasColumnType("nvarchar(max)");
    }
}

public class AIAnalysisResultConfiguration : IEntityTypeConfiguration<AIAnalysisResult>
{
    public void Configure(EntityTypeBuilder<AIAnalysisResult> builder)
    {
        builder.ToTable("AIAnalysisResults");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.ApplicationId);
        builder.HasIndex(x => x.OverallScore);
        builder.HasIndex(x => x.AnalysisDate);

        // Properties
        builder.Property(x => x.OverallScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.SkillMatchScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.ExperienceScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.EducationScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.LocationScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.SalaryExpectationScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.ConfidenceLevel)
            .HasPrecision(5, 2);

        // JSON columns
        builder.Property(x => x.DetailedAnalysisJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.StrengthsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.WeaknessesJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ExtractedKeywordsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.RedFlagsJson)
            .HasColumnType("nvarchar(max)");

        // Relationships
        builder.HasOne(x => x.Application)
            .WithOne()
            .HasForeignKey<AIAnalysisResult>(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.ToTable("ActivityLogs");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.EntityType);
        builder.HasIndex(x => x.EntityId);
        builder.HasIndex(x => new { x.EntityType, x.EntityId });
        builder.HasIndex(x => x.CreationTime);
        builder.HasIndex(x => x.UserId);

        // Properties
        builder.Property(x => x.EntityType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Action)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.OldValuesJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.NewValuesJson)
            .HasColumnType("nvarchar(max)");
    }
}

public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder.ToTable("Interviews");

        builder.ConfigureByConvention();

        // Indexes
        builder.HasIndex(x => x.ApplicationId);
        builder.HasIndex(x => x.ScheduledDate);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.Type);

        // Properties
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        // JSON columns
        builder.Property(x => x.InterviewersJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.QuestionsJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.EvaluationScoresJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.AttachmentsJson)
            .HasColumnType("nvarchar(max)");

        // Relationships
        builder.HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey(x => x.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

#endregion
