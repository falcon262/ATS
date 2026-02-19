using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseJobFieldLengths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Responsibilities",
                table: "Jobs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Requirements",
                table: "Jobs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreationTime", "CreatorId", "DeleterId", "DeletionTime", "Description", "HeadEmail", "HeadId", "HeadName", "IsActive", "LastModificationTime", "LastModifierId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("545413b6-cddd-4970-8ecb-1cf8a5ccbc15"), "DES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "UX/UI Design Department", null, null, null, true, null, null, "Design", null, null },
                    { new Guid("a906e928-785d-455e-885f-296d4360e4ea"), "PROD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Product Management Department", null, null, null, true, null, null, "Product", null, null },
                    { new Guid("b1ded734-15c4-4ce4-9e71-f0885dfc8166"), "ENG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Software Development Department", null, null, null, true, null, null, "Engineering", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "SynonymsJson", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("127a1dcf-ea0c-4bc3-a4fb-60bcc2807b59"), "Programming", null, true, "Java", null, 0 },
                    { new Guid("1d3d2e1a-8320-4da0-b987-4561ac61f492"), "Framework", null, true, "Node.js", null, 0 },
                    { new Guid("27b7f78c-0c23-4732-a007-b6a5c8b5d532"), "Soft Skill", null, true, "Problem Solving", null, 0 },
                    { new Guid("29e4cbbb-829c-479d-8d27-c1b88f068cc9"), "Soft Skill", null, true, "Team Collaboration", null, 0 },
                    { new Guid("425bfd1f-5f03-49c7-b9ca-c65fadca6aec"), "Programming", null, true, "C#", null, 0 },
                    { new Guid("448419f9-ff36-49cb-ac99-79c9a96db267"), "Methodology", null, true, "Agile", null, 0 },
                    { new Guid("4a71f473-4bf2-4091-94d8-a0777f1769ab"), "DevOps", null, true, "Kubernetes", null, 0 },
                    { new Guid("54d1de20-7498-4e4f-b3d2-7830c800cb81"), "Framework", null, true, "Vue.js", null, 0 },
                    { new Guid("55f23d55-6c99-4f75-9022-e1308c69837d"), "Database", null, true, "SQL Server", null, 0 },
                    { new Guid("5aadce8d-8915-4ec2-8b52-789961d565cb"), "Cloud", null, true, "Google Cloud", null, 0 },
                    { new Guid("655fee19-c3e0-4ab6-bd81-ff7338928bde"), "Framework", null, true, "Angular", null, 0 },
                    { new Guid("6a7002de-cf68-4672-9e96-65a31bd13dab"), "Database", null, true, "MongoDB", null, 0 },
                    { new Guid("6ee7845d-20e8-4e2f-9e6b-047b95b1c421"), "Soft Skill", null, true, "Communication", null, 0 },
                    { new Guid("934b9ed2-5619-4ad9-abc9-64d93cce87fa"), "DevOps", null, true, "Docker", null, 0 },
                    { new Guid("95c51c9b-0c50-48bd-92cd-fd263de59d21"), "Database", null, true, "PostgreSQL", null, 0 },
                    { new Guid("9962ecab-aeb6-4a93-aa7a-4df89681c8dd"), "Methodology", null, true, "Scrum", null, 0 },
                    { new Guid("9a0270c8-281f-4a2d-8fc3-002adfbf7d1d"), "Programming", null, true, "SQL", null, 0 },
                    { new Guid("a21e753b-b79e-4413-8a8c-61bf64636fd9"), "Programming", null, true, "JavaScript", null, 0 },
                    { new Guid("b32b62ef-dd21-4f5c-b2a9-f65bb7932f10"), "Database", null, true, "Redis", null, 0 },
                    { new Guid("c32dd89d-128d-4381-bbf7-1272ef4dea8e"), "Framework", null, true, "React", null, 0 },
                    { new Guid("c4cd31d2-d0ea-4c72-9627-c7e8705f2790"), "Soft Skill", null, true, "Leadership", null, 0 },
                    { new Guid("c9d3312c-19f3-4093-b6af-6295738722a3"), "Programming", null, true, "TypeScript", null, 0 },
                    { new Guid("c9d8464c-b43e-440d-b71b-69f4206fae33"), "Cloud", null, true, "Azure", null, 0 },
                    { new Guid("e9f1f04d-b190-4234-af09-61a06d93a48a"), "Framework", null, true, ".NET Core", null, 0 },
                    { new Guid("f4ecc9e0-52db-4903-a44f-35e5cdc885f9"), "Programming", null, true, "Python", null, 0 },
                    { new Guid("f873304f-8276-4380-bb82-7916e43f0688"), "Cloud", null, true, "AWS", null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("545413b6-cddd-4970-8ecb-1cf8a5ccbc15"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("a906e928-785d-455e-885f-296d4360e4ea"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("b1ded734-15c4-4ce4-9e71-f0885dfc8166"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("127a1dcf-ea0c-4bc3-a4fb-60bcc2807b59"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("1d3d2e1a-8320-4da0-b987-4561ac61f492"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("27b7f78c-0c23-4732-a007-b6a5c8b5d532"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("29e4cbbb-829c-479d-8d27-c1b88f068cc9"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("425bfd1f-5f03-49c7-b9ca-c65fadca6aec"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("448419f9-ff36-49cb-ac99-79c9a96db267"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4a71f473-4bf2-4091-94d8-a0777f1769ab"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("54d1de20-7498-4e4f-b3d2-7830c800cb81"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("55f23d55-6c99-4f75-9022-e1308c69837d"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("5aadce8d-8915-4ec2-8b52-789961d565cb"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("655fee19-c3e0-4ab6-bd81-ff7338928bde"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6a7002de-cf68-4672-9e96-65a31bd13dab"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6ee7845d-20e8-4e2f-9e6b-047b95b1c421"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("934b9ed2-5619-4ad9-abc9-64d93cce87fa"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("95c51c9b-0c50-48bd-92cd-fd263de59d21"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("9962ecab-aeb6-4a93-aa7a-4df89681c8dd"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("9a0270c8-281f-4a2d-8fc3-002adfbf7d1d"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("a21e753b-b79e-4413-8a8c-61bf64636fd9"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b32b62ef-dd21-4f5c-b2a9-f65bb7932f10"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c32dd89d-128d-4381-bbf7-1272ef4dea8e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c4cd31d2-d0ea-4c72-9627-c7e8705f2790"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c9d3312c-19f3-4093-b6af-6295738722a3"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c9d8464c-b43e-440d-b71b-69f4206fae33"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e9f1f04d-b190-4234-af09-61a06d93a48a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f4ecc9e0-52db-4903-a44f-35e5cdc885f9"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f873304f-8276-4380-bb82-7916e43f0688"));

            migrationBuilder.AlterColumn<string>(
                name: "Responsibilities",
                table: "Jobs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Requirements",
                table: "Jobs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

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
        }
    }
}
