using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATS.Migrations
{
    /// <inheritdoc />
    public partial class AddBenefitsToJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Removed seed data deletion/insertion to avoid FK constraint violations
            // Only adding the Benefits column
            
            /*migrationBuilder.DeleteData(
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
                keyValue: new Guid("f6af4896-4bfb-44e8-83b6-5b7abb64ba6a"));*/

            migrationBuilder.AlterColumn<string>(
                name: "HiringManagerName",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "HiringManagerEmail",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Benefits",
                table: "Jobs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true); // Changed to nullable to avoid issues with existing data

            /*migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "CreationTime", "CreatorId", "DeleterId", "DeletionTime", "Description", "HeadEmail", "HeadId", "HeadName", "IsActive", "LastModificationTime", "LastModifierId", "Name", "ParentDepartmentId", "TenantId" },
                values: new object[,]
                {
                    { new Guid("2f0fda25-d8b8-47a2-b34e-880b97f62bf4"), "ENG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Software Development Department", null, null, null, true, null, null, "Engineering", null, null },
                    { new Guid("4f24b5c8-4acc-42db-afcc-6a24e56112a0"), "PROD", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Product Management Department", null, null, null, true, null, null, "Product", null, null },
                    { new Guid("5dca3d39-9fd7-4fb4-9e30-b0fd29f508ed"), "DES", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "UX/UI Design Department", null, null, null, true, null, null, "Design", null, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "SynonymsJson", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("1b1b451e-da99-4893-aec3-bef57de0731a"), "Methodology", null, true, "Agile", null, 0 },
                    { new Guid("280db316-b245-4d88-ac12-5f8e25d7eca2"), "Programming", null, true, "Python", null, 0 },
                    { new Guid("2a1c780e-10d1-420e-b2a4-821ab552a16c"), "Framework", null, true, "Node.js", null, 0 },
                    { new Guid("2e97de8f-44d9-4fd2-99b7-48feff25cf4f"), "Database", null, true, "PostgreSQL", null, 0 },
                    { new Guid("3489d0c7-0fa2-4f87-aa0b-f8b4360442e0"), "Programming", null, true, "TypeScript", null, 0 },
                    { new Guid("41e7e125-8a8c-4d85-80fc-f0a3cbe355a1"), "Database", null, true, "MongoDB", null, 0 },
                    { new Guid("4a19b893-e80e-45ac-b34f-73dd84abc54d"), "Database", null, true, "SQL Server", null, 0 },
                    { new Guid("4c23d208-d4bc-439f-b4d5-673c5616b80e"), "Framework", null, true, "Angular", null, 0 },
                    { new Guid("4e0338e7-ca46-49c4-833e-540fc6edec63"), "Programming", null, true, "C#", null, 0 },
                    { new Guid("4ff11bb2-a8e8-4232-8be5-9a6091c53f72"), "Cloud", null, true, "AWS", null, 0 },
                    { new Guid("6989d367-802e-4942-8590-7c410b21f926"), "Framework", null, true, "React", null, 0 },
                    { new Guid("6ef069b5-b80c-444b-9746-881f8234c624"), "Database", null, true, "Redis", null, 0 },
                    { new Guid("852d0ac3-c8ee-4478-a4d9-9c2f241a3441"), "Framework", null, true, "Vue.js", null, 0 },
                    { new Guid("b21994b6-a706-4b1a-8bae-15026f77cba9"), "Programming", null, true, "JavaScript", null, 0 },
                    { new Guid("b7c8e0b4-e529-43b0-9b9b-2d34c043bfcf"), "Soft Skill", null, true, "Team Collaboration", null, 0 },
                    { new Guid("bdb291ed-a9f6-4e94-b9cb-a8e76f6b7fa2"), "Programming", null, true, "SQL", null, 0 },
                    { new Guid("c4b25801-e933-4f45-b31a-6317295490c6"), "Programming", null, true, "Java", null, 0 },
                    { new Guid("cdf0fec7-52ae-40bc-99b0-16519ad9728b"), "Soft Skill", null, true, "Problem Solving", null, 0 },
                    { new Guid("d6a6e108-a017-467a-9b59-498f5c5727bc"), "Cloud", null, true, "Azure", null, 0 },
                    { new Guid("df26921f-8d92-4ed6-bb87-be869a4c5ccd"), "Soft Skill", null, true, "Communication", null, 0 },
                    { new Guid("e2c8e84f-b4cf-4779-a01e-79111693cbe2"), "Framework", null, true, ".NET Core", null, 0 },
                    { new Guid("eda71a31-39be-4624-af16-ccf906b56b4a"), "Cloud", null, true, "Google Cloud", null, 0 },
                    { new Guid("f0674c75-3285-46a7-9666-aab9029fe3be"), "DevOps", null, true, "Docker", null, 0 },
                    { new Guid("f7fa4c00-8736-4a24-943a-25f7c1ed0b1a"), "DevOps", null, true, "Kubernetes", null, 0 },
                    { new Guid("fa4d84c8-4757-493b-a360-1db5bcf33393"), "Methodology", null, true, "Scrum", null, 0 },
                    { new Guid("feeee452-e2f8-4c03-b2c6-7391e9740167"), "Soft Skill", null, true, "Leadership", null, 0 }
                });*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Only drop the Benefits column on rollback
            
            /*migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("2f0fda25-d8b8-47a2-b34e-880b97f62bf4"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("4f24b5c8-4acc-42db-afcc-6a24e56112a0"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("5dca3d39-9fd7-4fb4-9e30-b0fd29f508ed"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("1b1b451e-da99-4893-aec3-bef57de0731a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("280db316-b245-4d88-ac12-5f8e25d7eca2"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("2a1c780e-10d1-420e-b2a4-821ab552a16c"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("2e97de8f-44d9-4fd2-99b7-48feff25cf4f"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("3489d0c7-0fa2-4f87-aa0b-f8b4360442e0"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("41e7e125-8a8c-4d85-80fc-f0a3cbe355a1"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4a19b893-e80e-45ac-b34f-73dd84abc54d"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4c23d208-d4bc-439f-b4d5-673c5616b80e"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4e0338e7-ca46-49c4-833e-540fc6edec63"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("4ff11bb2-a8e8-4232-8be5-9a6091c53f72"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6989d367-802e-4942-8590-7c410b21f926"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("6ef069b5-b80c-444b-9746-881f8234c624"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("852d0ac3-c8ee-4478-a4d9-9c2f241a3441"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b21994b6-a706-4b1a-8bae-15026f77cba9"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("b7c8e0b4-e529-43b0-9b9b-2d34c043bfcf"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("bdb291ed-a9f6-4e94-b9cb-a8e76f6b7fa2"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("c4b25801-e933-4f45-b31a-6317295490c6"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("cdf0fec7-52ae-40bc-99b0-16519ad9728b"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("d6a6e108-a017-467a-9b59-498f5c5727bc"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("df26921f-8d92-4ed6-bb87-be869a4c5ccd"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("e2c8e84f-b4cf-4779-a01e-79111693cbe2"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("eda71a31-39be-4624-af16-ccf906b56b4a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f0674c75-3285-46a7-9666-aab9029fe3be"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("f7fa4c00-8736-4a24-943a-25f7c1ed0b1a"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("fa4d84c8-4757-493b-a360-1db5bcf33393"));

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("feeee452-e2f8-4c03-b2c6-7391e9740167"));

            migrationBuilder.DropColumn(
                name: "Benefits",
                table: "Jobs");

            /*migrationBuilder.AlterColumn<string>(
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
                });*/
        }
    }
}
