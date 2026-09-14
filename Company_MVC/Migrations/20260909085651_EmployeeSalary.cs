using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_MVC.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 5000);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Salary",
                value: 5000);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Salary",
                value: 5000);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Salary",
                value: 5000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employees");
        }
    }
}
