using LeaveManagementSystem.Domain.Common;
using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Domain.Entities;

public class LeaveRequest : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Employee? Employee { get; set; }

}