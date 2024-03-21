using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using AutoMapper;
using EmployeeDto = Aktitic.HrProject.DAL.Pagination.Employee.EmployeeDto;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.BL.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Employee, EmployeeDto>().ForMember(dest => 
            dest.DepartmentDto, opt => 
            opt.MapFrom(src => src.Department == null ? null : new DepartmentDto()
            {
                Id = src.Department.Id,
                Name = src.Department.Name!
            }));
        CreateMap<Holiday, HolidayDto>();
        CreateMap<Client, ClientDto>();
        CreateMap<Project,ProjectDto>();
        CreateMap<Task, TaskDto>();
        CreateMap<Ticket, TicketDto>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<Designation, DesignationDto>().ForMember(dest => 
            dest.Department, opt => 
            opt.MapFrom(src => src.Department == null ? null : new DepartmentDto
            {
                Id = src.Department.Id,
                Name = src.Department.Name!,
            }));
        CreateMap<Leaves, LeavesDto>().ForMember(dest => dest.Employee, 
            opt => opt.MapFrom(
                src => src.Employee == null 
                    ? null 
                    : new EmployeeDto()
            {
                FullName = src.Employee.FullName!,
                Email = src.Employee.Email,
                ImgUrl = src.Employee.ImgUrl,
                JobPosition = src.Employee.JobPosition,
                DepartmentDto = src.Employee.Department == null ? null : new DepartmentDto
                {
                    Id = src.Employee.Department.Id,
                    Name = src.Employee.Department.Name!
                }
            })).ForMember(dest => dest.ApprovedBy, 
            opt => opt.MapFrom(
                src => src.ApprovedByNavigation == null 
                    ? null 
                    : new EmployeeDto()
                    {
                        FullName = src.Employee.FullName!,
                        Email = src.Employee.Email,
                        ImgUrl = src.Employee.ImgUrl,
                        JobPosition = src.Employee.JobPosition,
                        DepartmentDto = src.Employee.Department == null ? null : new DepartmentDto
                        {
                            Name = src.Employee.Department.Name!
                        }
                    }));
        CreateMap<Attendance, AttendanceDto>();
        CreateMap<TimeSheet, TimeSheetDto>()

            .ForMember(dest => dest.IdNavigation,
                opt =>
                    opt.MapFrom(src => src.Employee == null
                        ? null
                        : new EmployeeDto()
                        {
                            FullName = src.Employee.FullName!,
                            Email = src.Employee.Email,
                            ImgUrl = src.Employee.ImgUrl,
                            JobPosition = src.Employee.JobPosition,
                            DepartmentDto = src.Employee.Department == null ? null : new DepartmentDto
                            {
                                Name = src.Employee.Department.Name!
                            }
                        }
                    )).ForMember(dest => dest.ProjectDto, opt =>
                        opt.MapFrom(src => src.Project == null ? null : new ProjectDto
                        {
                            Name = src.Project.Name!,
                            Client = src.Project.Client == null ? null : new ClientDto
                            {
                                FirstName = src.Project.Client.FirstName!,
                                LastName = src.Project.Client.LastName!
                            }
                        }));
        CreateMap<Shift, ShiftDto>();
        CreateMap<Scheduling, ScheduleDto>().ForMember(dest => 
            dest.Employee, opt => 
            opt.MapFrom(src => src.Employee == null ? null : new EmployeeDto
            {
                FullName = src.Employee.FullName!,
                Email = src.Employee.Email,
                ImgUrl = src.Employee.ImgUrl,
                JobPosition = src.Employee.JobPosition,
                DepartmentDto = src.Employee.Department == null ? null : new DepartmentDto
                {
                    Name = src.Employee.Department.Name!
                }
            }));
        CreateMap<Overtime, OvertimeDto>();
        
    }
    

    // private DepartmentDto MapDepartment(Designation source)
    // {
    //     if (source.Department == null)
    //     {
    //         // Handle null Department case here
    //         return null;
    //     }
    //
    //     // Map Department to DepartmentDto
    //     // var departmentDto = Mapper.Map<Department, DepartmentDto>(source.Department);
    //     // return departmentDto;
    //     
    //     var mapper = new MapperConfiguration(cfg =>
    //     {
    //         cfg.CreateMap<Department, DepartmentDto>();
    //     }).CreateMapper();
    //
    //     return mapper.Map<Department, DepartmentDto>(source.Department);
    // }
    
    // implement mapping 
    
}