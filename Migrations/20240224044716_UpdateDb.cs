using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18618d4a-b9cf-42e3-936f-2c9763b43c36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d5236d4-0539-4741-ae7e-d28c7613479b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6236bbf-f743-472f-803a-b82727b56c7b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c824c091-b2cf-40c1-b581-781f13eb45d9");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49c8ebf0-b9c3-4d9c-90fb-d341b5f633de", "1e26d399-ed16-4184-8f7e-49467cbf6ac8", "Admin", "ADMIN" },
                    { "91a2e8f0-351a-4abc-bca4-b85a8c4ec6af", "e4f4bd28-5706-4847-b46f-8d6bea63e98b", "Employee", "EMPLOYEE" },
                    { "e78788f9-30c3-4466-9516-0083dea205bf", "9c20631e-8109-4b29-9d23-748eddd0428a", "Client", "CLIENT" },
                    { "f53ada1d-996a-47e6-8b08-35c401d995dc", "5fe96d25-621a-4e1d-8e6d-ce584eba3c36", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49c8ebf0-b9c3-4d9c-90fb-d341b5f633de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91a2e8f0-351a-4abc-bca4-b85a8c4ec6af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e78788f9-30c3-4466-9516-0083dea205bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f53ada1d-996a-47e6-8b08-35c401d995dc");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18618d4a-b9cf-42e3-936f-2c9763b43c36", "990958d5-7442-4f92-8516-eb2a04edf782", "Admin", "ADMIN" },
                    { "9d5236d4-0539-4741-ae7e-d28c7613479b", "f283bf1e-9700-4be1-a641-4e19d333cd49", "Manager", "MANAGER" },
                    { "b6236bbf-f743-472f-803a-b82727b56c7b", "116ec0f9-ba99-4e76-9ad4-37a23029c32a", "Client", "CLIENT" },
                    { "c824c091-b2cf-40c1-b581-781f13eb45d9", "6980ddd7-2bf6-4ea3-b749-d290c6f7c892", "Employee", "EMPLOYEE" }
                });
        }
    }
}
