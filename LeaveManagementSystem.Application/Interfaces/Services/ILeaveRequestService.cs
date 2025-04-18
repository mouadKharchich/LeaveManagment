using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Application.DTOS;
using LeaveManagementSystem.Domain.Entities;

namespace LeaveManagementSystem.Application.Interfaces.Services;

public interface ILeaveRequestService
{
    Task<List<LeaveRequestReadDTO>> GetAllAsync();
    Task<LeaveRequestReadDTO?> GetByIdAsync(Guid id);
    Task<LeaveRequestReadDTO> CreateAsync(LeaveRequestCreateUpdateDTO dto);
    Task<bool> UpdateAsync(Guid id, LeaveRequestCreateUpdateDTO dto);
    Task<bool> DeleteAsync(Guid id);

    Task<PagedResult<LeaveRequest>> FilterLeaveRequestsAsync(LeaveRequestFilterDto filter);
    Task<IEnumerable<LeaveReportDto>> GetLeaveReportAsync(int year);
    Task<LeaveRequest> ApproveLeaveRequestAsync(Guid id);
}