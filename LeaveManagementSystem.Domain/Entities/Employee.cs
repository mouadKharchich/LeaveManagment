using LeaveManagementSystem.Domain.Common;

namespace LeaveManagementSystem.Domain.Entities;

public class Employee : BaseEntity
{
    public string FullName { get; set; }
    public string Department { get; set; }
    public DateTime JoiningDate { get; set; }
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

}