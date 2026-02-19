using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class SetupOfBaseEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OldValuesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValuesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlternatePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CurrentJobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CurrentCompany = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ResumeUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CoverLetterUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PortfolioUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GitHubUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PersonalWebsite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SkillsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificationsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PreferredCurrency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsWillingToRelocate = table.Column<bool>(type: "bit", nullable: false),
                    IsOpenToRemote = table.Column<bool>(type: "bit", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoticePeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReferredBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AIAnalysisJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverallAIScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    TagsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ConsentToProcess = table.Column<bool>(type: "bit", nullable: false),
                    ConsentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HeadName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HeadEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ParentDepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentDepartmentId",
                        column: x => x.ParentDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SynonymsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsRemote = table.Column<bool>(type: "bit", nullable: false),
                    EmploymentType = table.Column<int>(type: "int", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: false),
                    MinSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MaxSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequiredSkillsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredSkillsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HiringManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HiringManagerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HiringManagerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    ApplicationCount = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    AIScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    AIMatchSummary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AIAnalysisDetailsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillMatchScoresJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: true),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ScreeningAnswersJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedToName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InterviewLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InterviewNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RejectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OfferedSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OfferExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OfferAccepted = table.Column<bool>(type: "bit", nullable: true),
                    ScreeningCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstInterviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalInterviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityLogJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailsSent = table.Column<int>(type: "int", nullable: false),
                    LastEmailDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AIAnalysisResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OverallScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    SkillMatchScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ExperienceScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    EducationScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    LocationScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    SalaryExpectationScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    AIProvider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DetailedAnalysisJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrengthsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeaknessesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RecommendationType = table.Column<int>(type: "int", nullable: false),
                    ExtractedKeywordsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalysisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessingTimeMs = table.Column<int>(type: "int", nullable: false),
                    ConfidenceLevel = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    RedFlagsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIAnalysisResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIAnalysisResults_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MeetingLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InterviewersJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    EvaluationScoresJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decision = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AttachmentsJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreationTime", "CreatorId", "DeleterId", "DeletionTime", "Description", "HeadEmail", "HeadId", "HeadName", "IsActive", "LastModificationTime", "LastModifierId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("b235133d-5ff3-4a4e-93c9-7a9e3f127f3b"), "PROD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Product Management Department", null, null, null, true, null, null, "Product", null, null },
                    { new Guid("c827854a-4d10-46a5-a293-d732d3f45655"), "DES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "UX/UI Design Department", null, null, null, true, null, null, "Design", null, null },
                    { new Guid("c8d71b85-0778-4a84-a7e7-633ceb7dc094"), "ENG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Software Development Department", null, null, null, true, null, null, "Engineering", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "SynonymsJson", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("00e4158a-06cd-43ba-9f1a-5b63b62117d0"), "Programming", null, true, "TypeScript", null, 0 },
                    { new Guid("09e94101-cbc5-4c78-befe-fe1a6d0ccc4e"), "Programming", null, true, "C#", null, 0 },
                    { new Guid("0d124afb-ccbb-4be1-b7fb-7d92a54a7ae2"), "Programming", null, true, "JavaScript", null, 0 },
                    { new Guid("1c0da194-78dc-4aaf-9869-65f7af9d3633"), "Framework", null, true, ".NET Core", null, 0 },
                    { new Guid("20d23ffa-28b7-4d43-881a-0e1e0d50c4ba"), "Database", null, true, "MongoDB", null, 0 },
                    { new Guid("291aa987-2ca7-4ae4-b068-1ab63af4586c"), "Methodology", null, true, "Agile", null, 0 },
                    { new Guid("4af71265-7d0d-415a-9662-cb8793664f42"), "Framework", null, true, "Angular", null, 0 },
                    { new Guid("555010d0-02e4-4bfa-9941-d7a18d8c35d5"), "Framework", null, true, "Node.js", null, 0 },
                    { new Guid("73e9869e-da2c-4e43-bb8a-e5f0cf48118f"), "Cloud", null, true, "AWS", null, 0 },
                    { new Guid("7db4fa43-ae38-4619-bdbf-846292fd781e"), "Cloud", null, true, "Google Cloud", null, 0 },
                    { new Guid("88c8b953-abc7-49e0-9872-0f6972ae0d10"), "Soft Skill", null, true, "Leadership", null, 0 },
                    { new Guid("91b2627b-bea4-45cc-94e5-9ee1b4b0e8bf"), "Cloud", null, true, "Azure", null, 0 },
                    { new Guid("95415edb-7ae8-429a-89ea-d8fbb6b65e7e"), "Framework", null, true, "React", null, 0 },
                    { new Guid("977cb267-3fdc-4333-9394-0af6c2bd76fe"), "Programming", null, true, "SQL", null, 0 },
                    { new Guid("9ab19fab-664b-47e2-8bea-82108449f676"), "Database", null, true, "Redis", null, 0 },
                    { new Guid("9ad99d63-5e7e-4285-9edd-2d88eb8a8eab"), "Soft Skill", null, true, "Problem Solving", null, 0 },
                    { new Guid("9dc1c8af-0200-43e8-81a3-3a02780b5d3d"), "Programming", null, true, "Java", null, 0 },
                    { new Guid("abd2225a-79ee-49e5-aeaf-5e6cde4b6ee8"), "Database", null, true, "PostgreSQL", null, 0 },
                    { new Guid("b37a6d26-e069-4b96-9f2c-127daffd5ce9"), "DevOps", null, true, "Kubernetes", null, 0 },
                    { new Guid("c7facbb0-998a-424d-bac4-4599598b6d08"), "Soft Skill", null, true, "Communication", null, 0 },
                    { new Guid("d64d6087-4c8d-4403-9746-e95d70371eea"), "Methodology", null, true, "Scrum", null, 0 },
                    { new Guid("ddecc67b-f473-4dd6-a3fc-529d25ba2e9a"), "Soft Skill", null, true, "Team Collaboration", null, 0 },
                    { new Guid("e68e95bf-3088-464e-8b03-9c6c7fd54245"), "Programming", null, true, "Python", null, 0 },
                    { new Guid("e885f338-4b2a-4ea2-bf37-066db76dceee"), "Database", null, true, "SQL Server", null, 0 },
                    { new Guid("f03baf7e-06d1-4797-b3a7-bad569cbc041"), "Framework", null, true, "Vue.js", null, 0 },
                    { new Guid("f1a752a2-4c67-4906-9c28-0fe11e68deb9"), "DevOps", null, true, "Docker", null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_CreationTime",
                table: "ActivityLogs",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_EntityId",
                table: "ActivityLogs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_EntityType",
                table: "ActivityLogs",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_EntityType_EntityId",
                table: "ActivityLogs",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AIAnalysisResults_AnalysisDate",
                table: "AIAnalysisResults",
                column: "AnalysisDate");

            migrationBuilder.CreateIndex(
                name: "IX_AIAnalysisResults_ApplicationId",
                table: "AIAnalysisResults",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AIAnalysisResults_OverallScore",
                table: "AIAnalysisResults",
                column: "OverallScore");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AIScore",
                table: "Applications",
                column: "AIScore");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppliedDate",
                table: "Applications",
                column: "AppliedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CandidateId_Status",
                table: "Applications",
                columns: new[] { "CandidateId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId_CandidateId",
                table: "Applications",
                columns: new[] { "JobId", "CandidateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId_Status",
                table: "Applications",
                columns: new[] { "JobId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Stage",
                table: "Applications",
                column: "Stage");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Status",
                table: "Applications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_FirstName_LastName",
                table: "Candidates",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_LastName",
                table: "Candidates",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_OverallAIScore",
                table: "Candidates",
                column: "OverallAIScore");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Status",
                table: "Candidates",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Code",
                table: "Departments",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_IsActive",
                table: "Departments",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentDepartmentId",
                table: "Departments",
                column: "ParentDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ApplicationId",
                table: "Interviews",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ScheduledDate",
                table: "Interviews",
                column: "ScheduledDate");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_Status",
                table: "Interviews",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_Type",
                table: "Interviews",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Status",
                table: "Jobs",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Status_PostedDate",
                table: "Jobs",
                columns: new[] { "Status", "PostedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Title",
                table: "Jobs",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Category",
                table: "Skills",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_IsActive",
                table: "Skills",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UsageCount",
                table: "Skills",
                column: "UsageCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AIAnalysisResults");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
