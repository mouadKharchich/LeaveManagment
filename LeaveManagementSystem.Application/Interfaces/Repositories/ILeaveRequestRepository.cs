using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Application.DTOS;
using LeaveManagementSystem.Domain.Entities;

namespace LeaveManagementSystem.Application.Interfaces.Repositories;

public interface ILeaveRequestRepository
{
    Task<IEnumerable<LeaveRequest>> GetAllAsync();
    Task<LeaveRequest?> GetByIdAsync(Guid id);
    Task AddAsync(LeaveRequest leaveRequest);
    Task UpdateAsync(LeaveRequest leaveRequest);
    Task DeleteAsync(Guid id);
    
    //FILTER
    Task<PagedResult<LeaveRequest>> GetFilteredAsync(LeaveRequestFilterDto filter);
    //report
    Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByYearAsync(int year);
    Task SaveAsync();
}