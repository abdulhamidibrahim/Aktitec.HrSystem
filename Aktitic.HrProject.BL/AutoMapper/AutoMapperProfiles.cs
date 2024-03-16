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
        CreateMap<Employee, EmployeeDto>();
        CreateMap<Holiday, HolidayDto>();
        CreateMap<Client, ClientDto>();
        CreateMap<Project,ProjectDto>();
        CreateMap<Task, TaskDto>();
        CreateMap<Ticket, TicketDto>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<Designation, DesignationDto>();
        CreateMap<Leaves, LeavesDto>();
        
    }
    
    // implement mapping 
    
}