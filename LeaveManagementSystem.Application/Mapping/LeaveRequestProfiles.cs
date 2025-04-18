using AutoMapper;
using LeaveManagementSystem.Application.DTOS;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Domain.Enums;

namespace LeaveManagementSystem.Application.Mapping;

public class LeaveRequestProfiles : Profile
{
    public LeaveRequestProfiles()
    {
        CreateMap<LeaveRequest, LeaveRequestReadDTO>()
            .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => src.Employee.FullName))
            .ForMember(dest => dest.EmployeeDepartment, opt => opt.MapFrom(src => src.Employee.Department));

        CreateMap<LeaveRequestCreateUpdateDTO, LeaveRequest>()
            .ForMember(dest => dest.LeaveStatus, opt => opt.MapFrom(src => LeaveStatus.Pending))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
    
}