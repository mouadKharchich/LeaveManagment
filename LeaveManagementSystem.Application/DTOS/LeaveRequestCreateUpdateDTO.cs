using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Application.DTOS;

public class LeaveRequestCreateUpdateDTO
{
    public Guid EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
}