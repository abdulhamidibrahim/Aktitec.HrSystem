using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using AutoMapper;
using EmployeeDto = Aktitic.HrProject.DAL.Pagination.Employee.EmployeeDto;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.BL.AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Employee, EmployeeDto>().ForMember(dest =>
            dest.DepartmentDto, opt =>
            opt.MapFrom(src => src.Department == null ? null : new DepartmentDto
            {
                Id = src.Department.Id,
                Name = src.Department.Name!
            }));
        CreateMap<Holiday, HolidayDto>();
        CreateMap<Client, ClientDto>()
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                src.Permissions == null ? null : src.Permissions.Select(permission => new PermissionsDto
                {
                    Permission = permission.Name,
                    Read = permission.Read,
                    Write = permission.Write,
                    Delete = permission.Delete,
                    Create = permission.Create,
                    Import = permission.Import,
                    Export = permission.Export
                }).ToList()));
        
        CreateMap<Project,ProjectDto>();
        CreateMap<Task, TaskDto>().ForMember(dest =>
            dest.AssignEmployee, opt =>
            opt.MapFrom(src => src.AssignEmployee == null ? null : new EmployeeDto
            {
                FullName = src.AssignEmployee.FullName!,
                Email = src.AssignEmployee.Email,
                ImgUrl = src.AssignEmployee.ImgUrl,
                JobPosition = src.AssignEmployee.JobPosition,
                // DepartmentDto = src.AssignEmployee.Department == null ? null : new DepartmentDto
                // {
                //     Name = src.AssignEmployee.Department.Name!
                // }
            })).ForMember(dest => dest.Project, opt =>
                opt.MapFrom(src => src.Project == null ? null : new ProjectDto
            {
                Name = src.Project.Name!,
            }));
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
        CreateMap<Item, ItemDto>();
        CreateMap<ItemDto, Item>();
        CreateMap<Message, MessageDto>();
        CreateMap<Estimate, EstimateDto>();
        //map all expenses properties without purchasedBy   
        CreateMap<Expenses, ExpensesDto>().ForMember(dest=> 
            dest.PurchasedBy ,opt =>
            opt.MapFrom(src =>src.PurchasedBy == null? null: new EmployeeDto
            {
                FullName = src.PurchasedBy.FullName!,
            }));
        CreateMap<Payment, PaymentDto>();
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<Scheduling, ScheduleDto>();
          
        CreateMap<Overtime, OvertimeDto>();
        CreateMap<FileDto, File>();
        CreateMap<Permission, PermissionsDto>()
            .ForMember( dest => dest.Permission, 
                opt => 
                    opt.MapFrom(src => src.Name));
        
        CreateMap<PermissionsDto, Permission>()
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt
                .MapFrom(src => src.Permission));
    }
    

  
    
}