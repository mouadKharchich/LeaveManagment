using AutoMapper;
using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Application.DTOS;
using LeaveManagementSystem.Application.Interfaces.Repositories;
using LeaveManagementSystem.Application.Interfaces.Services;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Application.Services;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestReadDTO>> GetAllAsync()
    {
        var leaveRequests = await _leaveRequestRepository.GetAllAsync();
        return _mapper.Map<List<LeaveRequestReadDTO>>(leaveRequests);
    }

    public async Task<LeaveRequestReadDTO?> GetByIdAsync(Guid id)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
        return leaveRequest == null ? null : _mapper.Map<LeaveRequestReadDTO>(leaveRequest);
    }

    public async Task<LeaveRequestReadDTO> CreateAsync(LeaveRequestCreateUpdateDTO dto)
    {
        try
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(dto);
            leaveRequest.CreatedAt = DateTime.UtcNow;
            leaveRequest.LeaveStatus = LeaveStatus.Pending;

            await _leaveRequestRepository.AddAsync(leaveRequest);
            var leaveRequestResult = await _leaveRequestRepository.GetByIdAsync(leaveRequest.Id);

            return _mapper.Map<LeaveRequestReadDTO>(leaveRequestResult);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the leave request.", ex);
        }
    }

    public async Task<bool> UpdateAsync(Guid id, LeaveRequestCreateUpdateDTO dto)
    {
        try
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
            if (leaveRequest == null) return false;

            _mapper.Map(dto, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while updating the leave request with ID {id}.", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
            if (leaveRequest == null) return false;

            await _leaveRequestRepository.DeleteAsync(id);

            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while deleting the leave request with ID {id}.", ex);
        }
    }
    
    //***************************************//
    //FILTER
    public async Task<PagedResult<LeaveRequest>> FilterLeaveRequestsAsync(LeaveRequestFilterDto filter)
    {
        var results = await _leaveRequestRepository.GetFilteredAsync(filter);
        return results;
    }
    
    //report
    public async Task<IEnumerable<LeaveReportDto>> GetLeaveReportAsync(int year)
    {
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsByYearAsync(year);

        var report = leaveRequests
            .GroupBy(lr => new { lr.Employee.Id, lr.Employee.FullName,lr.Employee.Department})
            .Select(group => new LeaveReportDto
            {
                EmployeeId = group.Key.Id,
                EmployeeFullName = $"{group.Key.FullName}",
                EmployeeDepartment = group.Key.Department,
                TotalLeaves = group.Count(),
                AnnualLeaves = group.Count(lr => lr.LeaveType == LeaveType.Annual),
                SickLeaves = group.Count(lr => lr.LeaveType == LeaveType.Sick)
            })
            .ToList();

        return report;
    }
    
    //approve
    
    public async Task<LeaveRequest> ApproveLeaveRequestAsync(Guid id)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
        
        if (leaveRequest == null)
        {
            return null; 
        }

        if (leaveRequest.LeaveStatus!= LeaveStatus.Pending)
        {
            return null;
        }

        // Set status to Approved
        leaveRequest.LeaveStatus = LeaveStatus.Approved;

        await _leaveRequestRepository.SaveAsync();

        return leaveRequest;
    }
}
