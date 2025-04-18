using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Application.DTOS;

public class LeaveRequestReadDTO
{ 
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeFullName { get; set; }
    public string EmployeeDepartment { get; set; }
    public LeaveType LeaveType { get; set; }
    public LeaveStatus LeaveStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    
}