using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Application.DTOS;

public class LeaveRequestFilterDto
{
    public Guid? EmployeeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public LeaveStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Keyword { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "StartDate";
    public string SortOrder { get; set; } = "asc";
}