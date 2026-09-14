using Company_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_MVC.Data
{
    public class CompanyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
               .Property(e => e.Salary)
               .HasDefaultValue(5000);

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "IT"
                },
                new Department
                {
                    Id = 2,
                    Name = "HR"
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Mahmoud",
                    Age = 30,
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    Name = "Sarah",
                    Age = 25,
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 3,
                    Name = "Ahmed",
                    Age = 28,
                    DepartmentId = 1
                }
            );
        }
    }
}