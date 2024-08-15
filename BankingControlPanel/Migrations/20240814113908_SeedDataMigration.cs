using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankingControlPanel.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62ce3bec-b602-403d-b2fe-97dcd500800d", null, "User", "USER" },
                    { "867bca98-6dbe-4f3f-b9ee-894ce3bfc6ba", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "477a0e13-0f5c-4780-abab-ae70b8713796", 0, "d0620dc1-c885-4697-b262-75aa96c3a5e1", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAELtGW6ktBwUZwoxIeSye5wRl9vHXlL1itF2el3a2aIv2R2ayXXP4Jvd0GAq1c5JrPA==", null, false, "287a691c-a9f1-4319-9405-61eea3fab7b6", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "City", "Country", "Email", "FirstName", "LastName", "MobileNumber", "PersonalId", "Sex", "Street", "ZipCode" },
                values: new object[] { 1, "Ramallah", "Palestine", "mousa.majdi@gmail.com", "Mousa", "Mousa", "+970569375987", "401441456", "Male", "123 Main St", "00970" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "ClientId" },
                values: new object[] { 1, "ACC123456789", 1000.00m, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62ce3bec-b602-403d-b2fe-97dcd500800d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "867bca98-6dbe-4f3f-b9ee-894ce3bfc6ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "477a0e13-0f5c-4780-abab-ae70b8713796");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
