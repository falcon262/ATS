using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class AddJobPublicSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("b235133d-5ff3-4a4e-93c9-7a9e3f127f3b"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("c827854a-4d10-46a5-a293-d732d3f45655"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("c8d71b85-0778-4a84-a7e7-633ceb7dc094"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("00e4158a-06cd-43ba-9f1a-5b63b62117d0"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("09e94101-cbc5-4c78-befe-fe1a6d0ccc4e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("0d124afb-ccbb-4be1-b7fb-7d92a54a7ae2"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("1c0da194-78dc-4aaf-9869-65f7af9d3633"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("20d23ffa-28b7-4d43-881a-0e1e0d50c4ba"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("291aa987-2ca7-4ae4-b068-1ab63af4586c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4af71265-7d0d-415a-9662-cb8793664f42"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("555010d0-02e4-4bfa-9941-d7a18d8c35d5"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("73e9869e-da2c-4e43-bb8a-e5f0cf48118f"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("7db4fa43-ae38-4619-bdbf-846292fd781e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("88c8b953-abc7-49e0-9872-0f6972ae0d10"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("91b2627b-bea4-45cc-94e5-9ee1b4b0e8bf"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("95415edb-7ae8-429a-89ea-d8fbb6b65e7e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("977cb267-3fdc-4333-9394-0af6c2bd76fe"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("9ab19fab-664b-47e2-8bea-82108449f676"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("9ad99d63-5e7e-4285-9edd-2d88eb8a8eab"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("9dc1c8af-0200-43e8-81a3-3a02780b5d3d"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("abd2225a-79ee-49e5-aeaf-5e6cde4b6ee8"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b37a6d26-e069-4b96-9f2c-127daffd5ce9"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c7facbb0-998a-424d-bac4-4599598b6d08"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d64d6087-4c8d-4403-9746-e95d70371eea"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("ddecc67b-f473-4dd6-a3fc-529d25ba2e9a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e68e95bf-3088-464e-8b03-9c6c7fd54245"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e885f338-4b2a-4ea2-bf37-066db76dceee"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f03baf7e-06d1-4797-b3a7-bad569cbc041"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f1a752a2-4c67-4906-9c28-0fe11e68deb9"));

            migrationBuilder.AddColumn<bool>(
                name: "IsPubliclyVisible",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PublicSlug",
                table: "Jobs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            // Generate slugs for existing Active jobs using SQL
            // Note: This is a simplified version - in production, you'd use a proper slug generation
            migrationBuilder.Sql(@"
                UPDATE Jobs 
                SET PublicSlug = LOWER(
                    REPLACE(
                        REPLACE(
                            REPLACE(
                                REPLACE(Title, ' ', '-'),
                            '.', ''),
                        '#', ''),
                    '/', '')
                ) + '-' + RIGHT(CAST(Id AS NVARCHAR(36)), 8),
                IsPubliclyVisible = 1
                WHERE Status = 1 AND (PublicSlug IS NULL OR PublicSlug = '')
            ");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreationTime", "CreatorId", "DeleterId", "DeletionTime", "Description", "HeadEmail", "HeadId", "HeadName", "IsActive", "LastModificationTime", "LastModifierId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("040415f8-2c75-4c16-8b41-90e46148aca9"), "ENG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Software Development Department", null, null, null, true, null, null, "Engineering", null, null },
                    { new Guid("83c27661-69f0-43ad-893e-ffb6c2c91955"), "PROD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Product Management Department", null, null, null, true, null, null, "Product", null, null },
                    { new Guid("95600844-375e-4546-b5c2-e9dcc58b92fc"), "DES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "UX/UI Design Department", null, null, null, true, null, null, "Design", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "SynonymsJson", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("01292d57-6398-477a-bb05-f2afc9f3a70e"), "Programming", null, true, "TypeScript", null, 0 },
                    { new Guid("01950ad8-7a8d-4bfb-8ff8-6df4f00308d6"), "Programming", null, true, "SQL", null, 0 },
                    { new Guid("0fe3ad69-880d-4d17-9b76-70b185c5e359"), "Programming", null, true, "Python", null, 0 },
                    { new Guid("1e007aab-b887-4e5a-ad3f-63abc5e8d5ca"), "Framework", null, true, ".NET Core", null, 0 },
                    { new Guid("219dcfc3-24cf-46ee-85a2-4e111ebe67b6"), "Framework", null, true, "Vue.js", null, 0 },
                    { new Guid("2225df6c-e96c-4244-b512-b4992cbeabcb"), "Database", null, true, "MongoDB", null, 0 },
                    { new Guid("3f79b534-ffcd-4af0-809b-c55811abe856"), "DevOps", null, true, "Docker", null, 0 },
                    { new Guid("4466815e-e7b6-4f39-bd1a-8f4e1d92fd43"), "Programming", null, true, "Java", null, 0 },
                    { new Guid("480c459f-9f28-4773-9b9d-e34884582300"), "Methodology", null, true, "Scrum", null, 0 },
                    { new Guid("544166f8-2d17-4894-ad85-78adf5a6bf5a"), "Database", null, true, "SQL Server", null, 0 },
                    { new Guid("578b2b29-ef56-4afc-9bea-324281a48c6a"), "DevOps", null, true, "Kubernetes", null, 0 },
                    { new Guid("5fd73ed2-d469-4fa9-ac80-176523baf18e"), "Soft Skill", null, true, "Leadership", null, 0 },
                    { new Guid("62eb9b89-f760-4b13-80a8-5789b4da347b"), "Cloud", null, true, "Google Cloud", null, 0 },
                    { new Guid("6c44bacf-7df7-406f-ae2d-45be6d9a36c3"), "Framework", null, true, "Angular", null, 0 },
                    { new Guid("720d4468-902c-48ba-a15c-ad575102c998"), "Database", null, true, "PostgreSQL", null, 0 },
                    { new Guid("7b720502-2f32-42b9-9f04-420a8400c036"), "Database", null, true, "Redis", null, 0 },
                    { new Guid("a1d1ee72-9e50-4c75-8781-065e46ae085f"), "Soft Skill", null, true, "Team Collaboration", null, 0 },
                    { new Guid("aadbae0a-d593-43e8-9035-5f2fa6a6aeac"), "Cloud", null, true, "AWS", null, 0 },
                    { new Guid("abf38587-0f8d-44b1-8ed5-d41074a8a5db"), "Framework", null, true, "Node.js", null, 0 },
                    { new Guid("ac4fd4ab-10bd-4ff3-b21b-1c682158a867"), "Soft Skill", null, true, "Communication", null, 0 },
                    { new Guid("b30969ac-c0af-4985-9a28-a143f1730bc3"), "Soft Skill", null, true, "Problem Solving", null, 0 },
                    { new Guid("b7ed72db-4d32-41b9-882e-e2c88f071338"), "Framework", null, true, "React", null, 0 },
                    { new Guid("c3d6e66f-f333-44ee-9178-a5469443127d"), "Programming", null, true, "JavaScript", null, 0 },
                    { new Guid("d727f3bd-a12e-4405-8ec2-6bb14f1b6b86"), "Programming", null, true, "C#", null, 0 },
                    { new Guid("ef73b966-ac31-4028-8e1b-84f43b7841dc"), "Cloud", null, true, "Azure", null, 0 },
                    { new Guid("fb01dd03-c832-4feb-b8fe-642cba55c0d4"), "Methodology", null, true, "Agile", null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IsPubliclyVisible",
                table: "Jobs",
                column: "IsPubliclyVisible");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PublicSlug",
                table: "Jobs",
                column: "PublicSlug",
                unique: true,
                filter: "[PublicSlug] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_IsPubliclyVisible",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_PublicSlug",
                table: "Jobs");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("040415f8-2c75-4c16-8b41-90e46148aca9"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("83c27661-69f0-43ad-893e-ffb6c2c91955"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("95600844-375e-4546-b5c2-e9dcc58b92fc"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("01292d57-6398-477a-bb05-f2afc9f3a70e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("01950ad8-7a8d-4bfb-8ff8-6df4f00308d6"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("0fe3ad69-880d-4d17-9b76-70b185c5e359"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("1e007aab-b887-4e5a-ad3f-63abc5e8d5ca"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("219dcfc3-24cf-46ee-85a2-4e111ebe67b6"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("2225df6c-e96c-4244-b512-b4992cbeabcb"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("3f79b534-ffcd-4af0-809b-c55811abe856"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4466815e-e7b6-4f39-bd1a-8f4e1d92fd43"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("480c459f-9f28-4773-9b9d-e34884582300"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("544166f8-2d17-4894-ad85-78adf5a6bf5a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("578b2b29-ef56-4afc-9bea-324281a48c6a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("5fd73ed2-d469-4fa9-ac80-176523baf18e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("62eb9b89-f760-4b13-80a8-5789b4da347b"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6c44bacf-7df7-406f-ae2d-45be6d9a36c3"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("720d4468-902c-48ba-a15c-ad575102c998"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("7b720502-2f32-42b9-9f04-420a8400c036"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("a1d1ee72-9e50-4c75-8781-065e46ae085f"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("aadbae0a-d593-43e8-9035-5f2fa6a6aeac"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("abf38587-0f8d-44b1-8ed5-d41074a8a5db"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("ac4fd4ab-10bd-4ff3-b21b-1c682158a867"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b30969ac-c0af-4985-9a28-a143f1730bc3"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b7ed72db-4d32-41b9-882e-e2c88f071338"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c3d6e66f-f333-44ee-9178-a5469443127d"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d727f3bd-a12e-4405-8ec2-6bb14f1b6b86"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("ef73b966-ac31-4028-8e1b-84f43b7841dc"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("fb01dd03-c832-4feb-b8fe-642cba55c0d4"));

            migrationBuilder.DropColumn(
                name: "IsPubliclyVisible",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PublicSlug",
                table: "Jobs");

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
        }
    }
}
