using LeaveManagementSystem.Application.Common;
using LeaveManagementSystem.Application.DTOS;
using LeaveManagementSystem.Application.Interfaces.Repositories;
using LeaveManagementSystem.Domain.Common;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Enums;
using LeaveManagementSystem.Persistence;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Infrastructure.Repositories;

public class LeaveRequestRepository : ILeaveRequestRepository
{
    private readonly CoreDbContext _context;
    public LeaveRequestRepository(CoreDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<LeaveRequest>> GetAllAsync()
    {
        return await _context.LeaveRequests.Include(lr => lr.Employee).ToListAsync();
    }

    public async Task<LeaveRequest?> GetByIdAsync(Guid id)
    {
        return await _context.LeaveRequests.Include(lrs => lrs.Employee).FirstOrDefaultAsync(lr => lr.Id == id);
    }

    public async Task AddAsync(LeaveRequest leaveRequest)
    {
        try
        {
            var employee = new Employee
            {
                    Id = Guid.NewGuid(),
                    FullName = "Alice Dev",
                    Department = "Engineering",
                    JoiningDate = DateTime.UtcNow.AddYears(-2)
            };
           

            leaveRequest.Employee = employee;
            leaveRequest.EmployeeId = employee.Id;
            await _context.LeaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error to add leaveRequest",ex);
        }
    }

    public async  Task UpdateAsync(LeaveRequest leaveRequest)
    {
        _context.LeaveRequests.Update(leaveRequest);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.LeaveRequests.FindAsync(id);
        if (entity != null)
        {
            _context.LeaveRequests.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
    //**********************************************************//
    //FILTER
    
    public async Task<PagedResult<LeaveRequest>> GetFilteredAsync(LeaveRequestFilterDto filter)
    {
        var predicate = PredicateBuilder.New<LeaveRequest>(true);

        if (filter.EmployeeId.HasValue)
        {
            var employeeGuid = new Guid(filter.EmployeeId.Value.ToString("D").PadLeft(32, '0'));
            predicate = predicate.And(lr => lr.EmployeeId == employeeGuid);
        }

        if (filter.LeaveType.HasValue)
        {
            predicate = predicate.And(lr => lr.LeaveType == filter.LeaveType.Value);
        }

        if (filter.Status.HasValue)
        {
            predicate = predicate.And(lr => lr.LeaveStatus == filter.Status.Value);
        }

        if (filter.StartDate.HasValue)
            predicate = predicate.And(lr => lr.StartDate >= filter.StartDate);

        if (filter.EndDate.HasValue)
            predicate = predicate.And(lr => lr.EndDate <= filter.EndDate);

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
            predicate = predicate.And(lr => lr.Reason.Contains(filter.Keyword));

        var query = _context.LeaveRequests
            .AsExpandableEFCore()
            .Where(predicate);

        query = filter.SortOrder.ToLower() == "desc"
            ? query.OrderByDescendingDynamic(filter.SortBy)
            : query.OrderByDynamic(filter.SortBy);

        var total = await query.CountAsync();

        var data = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<LeaveRequest>
        {
            TotalCount = total,
            Page = filter.Page,
            PageSize = filter.PageSize,
            Items = data
        };
    }

    // report
    public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByYearAsync(int year)
    {
        return await _context.LeaveRequests
            .Include(lr => lr.Employee)
            .Where(lr => lr.StartDate.Year == year)
            .ToListAsync();
    }
    
    //saved
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}