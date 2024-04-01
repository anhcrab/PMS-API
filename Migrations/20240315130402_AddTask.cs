using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21f8d762-043e-4635-b25a-3c7c6dac4536");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3962376e-e3ad-4d08-836d-77a39190b077");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78727ff8-b5a9-441d-adf5-71c2be81d0aa");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1aadbad0-afa7-408b-9a86-a31d69364334", "2a8fc130-103a-4a39-9188-e8fa68b2d66d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1aadbad0-afa7-408b-9a86-a31d69364334", "2c6a180d-590e-42ac-b0ab-346c162473d3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aadbad0-afa7-408b-9a86-a31d69364334");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a8fc130-103a-4a39-9188-e8fa68b2d66d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c6a180d-590e-42ac-b0ab-346c162473d3");

            migrationBuilder.AddColumn<string>(
                name: "Deadline",
                table: "WorkTasks",
                type: "longtext",
                nullable: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11e0a8ae-8cec-4e8d-9b7f-21fe0af58b74", "d8cb802a-f38d-47fe-b3ad-4949f859fd66", "Admin", "ADMIN" },
                    { "a7b9681f-9efa-4a46-9e2b-dafcd3ef7f1f", "aa366bcc-1d3d-4190-bd80-904f371324fc", "Client", "CLIENT" },
                    { "aa00124d-de5f-4f38-b7a2-af54104f1b82", "b28859f4-c1c6-431f-911d-15cbe459da56", "Employee", "EMPLOYEE" },
                    { "b370661d-b0ca-4ad3-8d54-2211dd7e0fb2", "6abd503b-33d1-4a7d-9c36-69bc67dd7f20", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdditionalInfo", "Address", "ConcurrencyStamp", "CreationDate", "DeletedDate", "Department", "Description", "Dob", "Email", "EmailConfirmed", "FirstName", "Hometown", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SecurityStamp", "Sex", "Status", "SupervisorId", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { "1476e100-92cf-44b4-b384-82d87939fc95", 0, "", "", "f3df191f-2cd4-4a0a-9222-222fdcc6af47", new DateTime(2024, 3, 15, 20, 4, 1, 235, DateTimeKind.Local).AddTicks(6989), null, 0, "", "", "anhcrafter@gmail.com", true, "Quang Anh", "", "Đặng", false, null, "ANHCRAFTER@GMAIL.COM", "DEV", "AQAAAAIAAYagAAAAEP2oO3aEMmPklNGr3KrZvQ8DfgfSNjvIzh+UZtWi7AziSJaS7O3aTSbev+3HCUXrZA==", null, false, "", "b6a67828-3aa9-4fd8-96c9-7c5e8e7104c6", "male", 0, null, false, new DateTime(2024, 3, 15, 20, 4, 1, 235, DateTimeKind.Local).AddTicks(7001), "dev" },
                    { "40e56087-76f8-4c6b-b2b5-e6306014a8a2", 0, "", "", "46feb90c-c183-4869-9671-703e94e0af66", new DateTime(2024, 3, 15, 20, 4, 1, 166, DateTimeKind.Local).AddTicks(9696), null, 0, "", "", "phucthinhterus@gmail.com", true, "Phúc Thịnh", "", "Phan", false, null, "PHUCTHINHTERUS@GMAIL.COM", "TERUS", "AQAAAAIAAYagAAAAEADQpleDrVIkmjfiU+4gWm+9XtzVwalbS9NMLtgwTBqidRCa1myb2JjDUV0OHl67tw==", null, false, "", "21517668-e08e-445c-a535-07f455d9745a", "male", 0, null, false, new DateTime(2024, 3, 15, 20, 4, 1, 166, DateTimeKind.Local).AddTicks(9709), "terus" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "11e0a8ae-8cec-4e8d-9b7f-21fe0af58b74", "1476e100-92cf-44b4-b384-82d87939fc95" },
                    { "11e0a8ae-8cec-4e8d-9b7f-21fe0af58b74", "40e56087-76f8-4c6b-b2b5-e6306014a8a2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7b9681f-9efa-4a46-9e2b-dafcd3ef7f1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa00124d-de5f-4f38-b7a2-af54104f1b82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b370661d-b0ca-4ad3-8d54-2211dd7e0fb2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11e0a8ae-8cec-4e8d-9b7f-21fe0af58b74", "1476e100-92cf-44b4-b384-82d87939fc95" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11e0a8ae-8cec-4e8d-9b7f-21fe0af58b74", "40e56087-76f8-4c6b-b2b5-e6306014a8a2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11e0a8ae-8cec-4e8d-9b7f-21fe0af58b74");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1476e100-92cf-44b4-b384-82d87939fc95");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40e56087-76f8-4c6b-b2b5-e6306014a8a2");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "WorkTasks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aadbad0-afa7-408b-9a86-a31d69364334", "6c1444e6-9bc4-4ce1-8b15-23e208a25e4d", "Admin", "ADMIN" },
                    { "21f8d762-043e-4635-b25a-3c7c6dac4536", "de379c7e-4de4-4070-b908-19e164331368", "Client", "CLIENT" },
                    { "3962376e-e3ad-4d08-836d-77a39190b077", "85fb0a25-99f6-4ca7-82f7-06158b1d87e0", "Manager", "MANAGER" },
                    { "78727ff8-b5a9-441d-adf5-71c2be81d0aa", "c4ad4ee1-9c1e-47ab-bd99-1028fcd3c11f", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdditionalInfo", "Address", "ConcurrencyStamp", "CreationDate", "DeletedDate", "Department", "Description", "Dob", "Email", "EmailConfirmed", "FirstName", "Hometown", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Position", "SecurityStamp", "Sex", "Status", "SupervisorId", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { "2a8fc130-103a-4a39-9188-e8fa68b2d66d", 0, "", "", "fca3932b-a5a0-4f8d-b94b-16d24fb4c759", new DateTime(2024, 3, 6, 20, 58, 43, 649, DateTimeKind.Local).AddTicks(9082), null, 0, "", "", "phucthinhterus@gmail.com", true, "Phúc Thịnh", "", "Phan", false, null, "PHUCTHINHTERUS@GMAIL.COM", "TERUS", "AQAAAAIAAYagAAAAEHMOylivT5W+ir84bObV1Yjyx79c7qSsAlwgIblUp+120yiuy2m9S2te3St6W+GlsQ==", null, false, "", "9d3f6283-432f-439d-86c8-4d2cffff2fb1", "male", 0, null, false, new DateTime(2024, 3, 6, 20, 58, 43, 649, DateTimeKind.Local).AddTicks(9092), "terus" },
                    { "2c6a180d-590e-42ac-b0ab-346c162473d3", 0, "", "", "91492b1e-058b-448b-94d1-7f384ec391c0", new DateTime(2024, 3, 6, 20, 58, 43, 710, DateTimeKind.Local).AddTicks(5915), null, 0, "", "", "anhcrafter@gmail.com", true, "Quang Anh", "", "Đặng", false, null, "ANHCRAFTER@GMAIL.COM", "DEV", "AQAAAAIAAYagAAAAELKhEzl3Mpi4gqVYR6m3O+Ja1qxMl48hs1/XPMCOovqs3WxsWTfQyT20CbTcpFihUA==", null, false, "", "fad05160-9680-4557-95bd-80604761d214", "male", 0, null, false, new DateTime(2024, 3, 6, 20, 58, 43, 710, DateTimeKind.Local).AddTicks(5926), "dev" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1aadbad0-afa7-408b-9a86-a31d69364334", "2a8fc130-103a-4a39-9188-e8fa68b2d66d" },
                    { "1aadbad0-afa7-408b-9a86-a31d69364334", "2c6a180d-590e-42ac-b0ab-346c162473d3" }
                });
        }
    }
}
