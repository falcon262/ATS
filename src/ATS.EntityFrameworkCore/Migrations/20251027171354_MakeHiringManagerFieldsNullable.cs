using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class MakeHiringManagerFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            // Drop the index before altering columns
            migrationBuilder.DropIndex(
                name: "IX_Jobs_PublicSlug",
                table: "Jobs");

            // Alter HiringManagerEmail to nullable
            migrationBuilder.AlterColumn<string>(
                name: "HiringManagerEmail",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            // Alter HiringManagerName to nullable
            migrationBuilder.AlterColumn<string>(
                name: "HiringManagerName",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            // Alter PublicSlug to nullable
            migrationBuilder.AlterColumn<string>(
                name: "PublicSlug",
                table: "Jobs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            // Recreate the index
            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PublicSlug",
                table: "Jobs",
                column: "PublicSlug");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreationTime", "CreatorId", "DeleterId", "DeletionTime", "Description", "HeadEmail", "HeadId", "HeadName", "IsActive", "LastModificationTime", "LastModifierId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("0f72c76b-9f2d-4721-aa19-70efd0f55582"), "DES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "UX/UI Design Department", null, null, null, true, null, null, "Design", null, null },
                    { new Guid("36f8a5b3-40f7-4ea0-be90-4a97ce541b5f"), "ENG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Software Development Department", null, null, null, true, null, null, "Engineering", null, null },
                    { new Guid("744b3b40-7e93-492c-a2d5-90141f1f34b9"), "PROD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Product Management Department", null, null, null, true, null, null, "Product", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "SynonymsJson", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("32f72ddc-edcf-4ce0-9fa3-bb801cbfe26c"), "Framework", null, true, ".NET Core", null, 0 },
                    { new Guid("362dfacd-c7e1-4ea4-bbd4-469560c239db"), "Database", null, true, "SQL Server", null, 0 },
                    { new Guid("3ff080f2-b223-423b-a78d-87c6f2569559"), "Soft Skill", null, true, "Problem Solving", null, 0 },
                    { new Guid("435272c4-1fd5-4d86-96cf-d2765b938da1"), "DevOps", null, true, "Docker", null, 0 },
                    { new Guid("51716bea-bcc3-4445-bb2d-3dfb0fb4184b"), "Cloud", null, true, "Google Cloud", null, 0 },
                    { new Guid("6c146bdb-4bd9-4fe1-84ac-7318e375359b"), "Framework", null, true, "Node.js", null, 0 },
                    { new Guid("78dfa850-dbab-4a48-83f2-753598283188"), "Framework", null, true, "Angular", null, 0 },
                    { new Guid("7cadb809-3259-41b2-b7ce-495af00a7846"), "Programming", null, true, "Python", null, 0 },
                    { new Guid("86f2c078-7642-40f9-b56e-2f34890a5479"), "Database", null, true, "Redis", null, 0 },
                    { new Guid("8e23d2cd-6da5-459b-9521-bc2d2095b9f2"), "Programming", null, true, "TypeScript", null, 0 },
                    { new Guid("a56e838b-6b79-4631-bda6-dd159ffdc02c"), "Soft Skill", null, true, "Leadership", null, 0 },
                    { new Guid("a57b7bb8-4307-435b-bcd9-be699023d886"), "Framework", null, true, "React", null, 0 },
                    { new Guid("ac2b0202-9621-4f34-9562-35d5f7d49c7c"), "Methodology", null, true, "Agile", null, 0 },
                    { new Guid("b532cb4e-50c3-47c1-868a-152fc4a6a4e6"), "Programming", null, true, "Java", null, 0 },
                    { new Guid("b6eee2a4-63fd-426a-88fd-d4100a5d4f22"), "Methodology", null, true, "Scrum", null, 0 },
                    { new Guid("b862ea52-b4e9-4a4b-9cdd-5d64175f5aa0"), "Cloud", null, true, "Azure", null, 0 },
                    { new Guid("bd4f9ff5-9add-4aa2-aecb-708915601c9a"), "Framework", null, true, "Vue.js", null, 0 },
                    { new Guid("d121fd3b-753a-494c-93f3-677637ef2349"), "Cloud", null, true, "AWS", null, 0 },
                    { new Guid("d212d140-ecb3-47a4-a56f-eefcb2117ef1"), "DevOps", null, true, "Kubernetes", null, 0 },
                    { new Guid("d97f8256-b782-44e6-af7c-da10a09e8d92"), "Database", null, true, "MongoDB", null, 0 },
                    { new Guid("dbba83e1-d062-4f29-af0b-995d84e612b1"), "Programming", null, true, "JavaScript", null, 0 },
                    { new Guid("de19ee93-5175-4b2c-a04c-ec8dead59336"), "Database", null, true, "PostgreSQL", null, 0 },
                    { new Guid("e1599c18-0c8b-4bd5-8d0c-3ef9c02ce7ff"), "Programming", null, true, "SQL", null, 0 },
                    { new Guid("ed5d0da0-d59c-4f27-bcb7-303961c3cf5e"), "Programming", null, true, "C#", null, 0 },
                    { new Guid("f25df1ba-0dcc-4148-aeeb-a2de865c7558"), "Soft Skill", null, true, "Team Collaboration", null, 0 },
                    { new Guid("f6af4896-4bfb-44e8-83b6-5b7abb64ba6a"), "Soft Skill", null, true, "Communication", null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("0f72c76b-9f2d-4721-aa19-70efd0f55582"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("36f8a5b3-40f7-4ea0-be90-4a97ce541b5f"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("744b3b40-7e93-492c-a2d5-90141f1f34b9"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("32f72ddc-edcf-4ce0-9fa3-bb801cbfe26c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("362dfacd-c7e1-4ea4-bbd4-469560c239db"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("3ff080f2-b223-423b-a78d-87c6f2569559"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("435272c4-1fd5-4d86-96cf-d2765b938da1"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("51716bea-bcc3-4445-bb2d-3dfb0fb4184b"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6c146bdb-4bd9-4fe1-84ac-7318e375359b"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("78dfa850-dbab-4a48-83f2-753598283188"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("7cadb809-3259-41b2-b7ce-495af00a7846"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("86f2c078-7642-40f9-b56e-2f34890a5479"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("8e23d2cd-6da5-459b-9521-bc2d2095b9f2"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("a56e838b-6b79-4631-bda6-dd159ffdc02c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("a57b7bb8-4307-435b-bcd9-be699023d886"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("ac2b0202-9621-4f34-9562-35d5f7d49c7c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b532cb4e-50c3-47c1-868a-152fc4a6a4e6"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b6eee2a4-63fd-426a-88fd-d4100a5d4f22"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b862ea52-b4e9-4a4b-9cdd-5d64175f5aa0"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("bd4f9ff5-9add-4aa2-aecb-708915601c9a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d121fd3b-753a-494c-93f3-677637ef2349"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d212d140-ecb3-47a4-a56f-eefcb2117ef1"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d97f8256-b782-44e6-af7c-da10a09e8d92"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("dbba83e1-d062-4f29-af0b-995d84e612b1"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("de19ee93-5175-4b2c-a04c-ec8dead59336"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e1599c18-0c8b-4bd5-8d0c-3ef9c02ce7ff"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("ed5d0da0-d59c-4f27-bcb7-303961c3cf5e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f25df1ba-0dcc-4148-aeeb-a2de865c7558"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f6af4896-4bfb-44e8-83b6-5b7abb64ba6a"));

            // Drop the index before altering columns
            migrationBuilder.DropIndex(
                name: "IX_Jobs_PublicSlug",
                table: "Jobs");

            // Revert PublicSlug to non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "PublicSlug",
                table: "Jobs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            // Revert HiringManagerName to non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "HiringManagerName",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            // Revert HiringManagerEmail to non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "HiringManagerEmail",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            // Recreate the index
            migrationBuilder.CreateIndex(
                name: "IX_Jobs_PublicSlug",
                table: "Jobs",
                column: "PublicSlug");

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
    }
}
