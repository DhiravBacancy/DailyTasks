using EFCore_Day_5_Full_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFCore_Day_5_Full_CRUD.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> appDbContext) : base(appDbContext) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(i => i.Email).IsUnique();

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Project>()
                .HasKey(p => p.ProjectId);

            modelBuilder.Entity<Department>()
                .HasKey(d => d.DepartmentId);

            // Composite Key MUST be first
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            // One-to-Many: Department -> Employee
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many: Employee <-> EmployeeProject <-> Project
            // Employee -> EmployeeProject (One-to-Many)
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)  
                .HasForeignKey(ep => ep.EmployeeId);

            // Project -> EmployeeProject (One-to-Many)
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);

            // ------------- Data Seeding ------------- 
            // Seeding Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" },
                new Department { DepartmentId = 3, DepartmentName = "Finance" },
                new Department { DepartmentId = 4, DepartmentName = "Marketing" },
                new Department { DepartmentId = 5, DepartmentName = "Operations" }
            );

            // Seeding Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Alice Johnson", Email = "alice@company.com", DepartmentId = 1 },
                new Employee { EmployeeId = 2, Name = "Bob Smith", Email = "bob@company.com", DepartmentId = 1 },
                new Employee { EmployeeId = 3, Name = "Charlie Brown", Email = "charlie@company.com", DepartmentId = 2 },
                new Employee { EmployeeId = 4, Name = "David Wilson", Email = "david@company.com", DepartmentId = 3 },
                new Employee { EmployeeId = 5, Name = "Eve Adams", Email = "eve@company.com", DepartmentId = 2 },
                new Employee { EmployeeId = 6, Name = "Frank Thomas", Email = "frank@company.com", DepartmentId = 4 },
                new Employee { EmployeeId = 7, Name = "Grace Lee", Email = "grace@company.com", DepartmentId = 5 },
                new Employee { EmployeeId = 8, Name = "Henry Clark", Email = "henry@company.com", DepartmentId = 3 },
                new Employee { EmployeeId = 9, Name = "Isabella Moore", Email = "isabella@company.com", DepartmentId = 1 },
                new Employee { EmployeeId = 10, Name = "Jack White", Email = "jack@company.com", DepartmentId = 4 }
            );

            // Seeding Projects
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "Cloud Migration", StartDate = new DateOnly(2023, 06, 01) },
                new Project { ProjectId = 2, ProjectName = "E-commerce Platform", StartDate = new DateOnly(2023, 07, 15) },
                new Project { ProjectId = 3, ProjectName = "AI Chatbot", StartDate = new DateOnly(2023, 09, 10) },
                new Project { ProjectId = 4, ProjectName = "Data Analytics Dashboard", StartDate = new DateOnly(2023, 10, 05) },
                new Project { ProjectId = 5, ProjectName = "CRM System", StartDate = new DateOnly(2024, 01, 20) },
                new Project { ProjectId = 6, ProjectName = "ERP Upgrade", StartDate = new DateOnly(2024, 03, 12) }
            );

            // Seeding EmployeeProjects (Many-to-Many Relationships)
            modelBuilder.Entity<EmployeeProject>().HasData(
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Role = "Lead Developer" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 1, Role = "DevOps Engineer" },
                new EmployeeProject { EmployeeId = 3, ProjectId = 2, Role = "Project Manager" },
                new EmployeeProject { EmployeeId = 4, ProjectId = 3, Role = "Financial Analyst" },
                new EmployeeProject { EmployeeId = 5, ProjectId = 2, Role = "HR Consultant" },
                new EmployeeProject { EmployeeId = 1, ProjectId = 2, Role = "Full Stack Developer" },
                new EmployeeProject { EmployeeId = 3, ProjectId = 3, Role = "Data Scientist" },
                new EmployeeProject { EmployeeId = 4, ProjectId = 1, Role = "Tester" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 3, Role = "Backend Engineer" },
                new EmployeeProject { EmployeeId = 6, ProjectId = 4, Role = "Marketing Specialist" },
                new EmployeeProject { EmployeeId = 7, ProjectId = 5, Role = "Operations Manager" },
                new EmployeeProject { EmployeeId = 8, ProjectId = 6, Role = "Business Analyst" },
                new EmployeeProject { EmployeeId = 9, ProjectId = 4, Role = "Frontend Developer" },
                new EmployeeProject { EmployeeId = 10, ProjectId = 5, Role = "UI/UX Designer" },
                new EmployeeProject { EmployeeId = 5, ProjectId = 6, Role = "Scrum Master" }
            );
        }
    }
}