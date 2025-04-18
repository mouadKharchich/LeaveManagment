using LeaveManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Persistence;

public class CoreDbContext :DbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options){}

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                Department = "DEV",
                JoiningDate = DateTime.UtcNow.AddYears(-5)
            }
        );
    }
}