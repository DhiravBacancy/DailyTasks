using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore_Day_5_Full_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => new { x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "IT" },
                    { 2, "HR" },
                    { 3, "Finance" },
                    { 4, "Marketing" },
                    { 5, "Operations" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "ProjectName", "StartDate" },
                values: new object[,]
                {
                    { 1, "Cloud Migration", new DateOnly(2023, 6, 1) },
                    { 2, "E-commerce Platform", new DateOnly(2023, 7, 15) },
                    { 3, "AI Chatbot", new DateOnly(2023, 9, 10) },
                    { 4, "Data Analytics Dashboard", new DateOnly(2023, 10, 5) },
                    { 5, "CRM System", new DateOnly(2024, 1, 20) },
                    { 6, "ERP Upgrade", new DateOnly(2024, 3, 12) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DepartmentId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 1, "alice@company.com", "Alice Johnson" },
                    { 2, 1, "bob@company.com", "Bob Smith" },
                    { 3, 2, "charlie@company.com", "Charlie Brown" },
                    { 4, 3, "david@company.com", "David Wilson" },
                    { 5, 2, "eve@company.com", "Eve Adams" },
                    { 6, 4, "frank@company.com", "Frank Thomas" },
                    { 7, 5, "grace@company.com", "Grace Lee" },
                    { 8, 3, "henry@company.com", "Henry Clark" },
                    { 9, 1, "isabella@company.com", "Isabella Moore" },
                    { 10, 4, "jack@company.com", "Jack White" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId", "Role" },
                values: new object[,]
                {
                    { 1, 1, "Lead Developer" },
                    { 1, 2, "Full Stack Developer" },
                    { 2, 1, "DevOps Engineer" },
                    { 2, 3, "Backend Engineer" },
                    { 3, 2, "Project Manager" },
                    { 3, 3, "Data Scientist" },
                    { 4, 1, "Tester" },
                    { 4, 3, "Financial Analyst" },
                    { 5, 2, "HR Consultant" },
                    { 5, 6, "Scrum Master" },
                    { 6, 4, "Marketing Specialist" },
                    { 7, 5, "Operations Manager" },
                    { 8, 6, "Business Analyst" },
                    { 9, 4, "Frontend Developer" },
                    { 10, 5, "UI/UX Designer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
