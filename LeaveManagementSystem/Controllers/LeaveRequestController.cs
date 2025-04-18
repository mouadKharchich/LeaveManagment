using LeaveManagementSystem.Application.DTOS;
using LeaveManagementSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestController : ControllerBase
{ 
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }
    
    // Get all leave requests
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveRequestReadDTO>>> GetAll()
    {
        try
        {
            var result = await _leaveRequestService.GetAllAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    // Get leave request by ID

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveRequestReadDTO>> GetById(Guid id)
    {
        try
        {
            var result = await _leaveRequestService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    // Create a leave request
    [HttpPost]
    public async Task<ActionResult<LeaveRequestReadDTO>> Create([FromBody] LeaveRequestCreateUpdateDTO dto)
    {
        try
        {
            var result = await _leaveRequestService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    // Update a leave request
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] LeaveRequestCreateUpdateDTO dto)
    {
        try
        {
            var success = await _leaveRequestService.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    
    // Delete a leave request
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var success = await _leaveRequestService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
    
    // FILTER
    //****************************************************************************//
    
    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] LeaveRequestFilterDto filterDto)
    {
        var result = await _leaveRequestService.FilterLeaveRequestsAsync(filterDto);
        return Ok(result);
    }
    
    //report
    [HttpGet("report")]
    public async Task<IActionResult> GetLeaveReport([FromQuery] int year)
    {
        if (year <= 0)
        {
            return BadRequest("Invalid year.");
        }

        var report = await _leaveRequestService.GetLeaveReportAsync(year);
        return Ok(report);
    }
    
    //approve
    [HttpPost("{id}/approve")]
    public async Task<IActionResult> ApproveLeaveRequest(Guid id)
    {
        var result = await _leaveRequestService.ApproveLeaveRequestAsync(id);
        
        if (result == null)
        {
            return NotFound("Leave request not found.");
        }

        return Ok(result);
    }
}