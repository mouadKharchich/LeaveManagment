using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("9e10d3e2-d71a-442f-b858-4cb2ad6bfdf3"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FullName", "JoiningDate" },
                values: new object[] { new Guid("3eb2d37e-b2a9-40d5-a579-908b146a3f90"), "DEV", "John Doe", new DateTime(2020, 4, 18, 13, 56, 35, 139, DateTimeKind.Utc).AddTicks(3387) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("3eb2d37e-b2a9-40d5-a579-908b146a3f90"));

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FullName", "JoiningDate" },
                values: new object[] { new Guid("9e10d3e2-d71a-442f-b858-4cb2ad6bfdf3"), "DEV", "John Doe", new DateTime(2020, 4, 18, 13, 42, 46, 408, DateTimeKind.Utc).AddTicks(9565) });
        }
    }
}
