using LeaveManagementSystem.Application.Interfaces.Repositories;
using LeaveManagementSystem.Application.Interfaces.Services;
using LeaveManagementSystem.Application.Mapping;
using LeaveManagementSystem.Application.Services;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Enums;
using LeaveManagementSystem.Infrastructure.Repositories;
using LeaveManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAutoMapper(typeof(LeaveRequestProfiles).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

   builder.Services.AddDbContext<CoreDbContext>(options =>
       options.UseSqlite("Data Source=leave_management.db",
           x => x.MigrationsAssembly("LeaveManagementSystem.Persistence")));

builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddAutoMapper(typeof(LeaveRequestProfiles).Assembly);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
    //db.Database.EnsureCreated();
    
    if (!db.Employees.Any())
    {
        var employee = new Employee
        {
            Id = Guid.Parse("12345678-1234-1234-1234-123456789abc"),
            FullName = "Alice Dev",
            Department = "Engineering",
            JoiningDate = DateTime.UtcNow.AddYears(-2)
        };
        db.Employees.Add(employee);

        var leave = new LeaveRequest
        {
            Id = Guid.NewGuid(),
            EmployeeId = employee.Id,
            LeaveType = LeaveType.Annual,
            LeaveStatus = LeaveStatus.Pending,
            StartDate = DateTime.UtcNow.AddDays(5),
            EndDate = DateTime.UtcNow.AddDays(10),
            Reason = "Test leave",
            CreatedAt = DateTime.UtcNow,
            Employee = employee
        };
        db.LeaveRequests.Add(leave);
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();