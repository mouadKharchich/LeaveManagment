namespace LeaveManagementSystem.Application.DTOS;

public class LeaveReportDto
{
    public Guid EmployeeId { get; set; }
    public string EmployeeFullName { get; set; }
    public string EmployeeDepartment { get; set; }
    public int TotalLeaves { get; set; }
    public int AnnualLeaves { get; set; }
    public int SickLeaves { get; set; }
}