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
            dest.Department, opt =>
            opt.MapFrom(src => src.Department == null ? null : src.Department.Name));
        
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
        
        CreateMap<FileDto,File>();
        CreateMap<Project,ProjectDto>();
        CreateMap<Task, TaskDto>().ForMember(dest =>
            dest.AssignEmployee, opt =>
            opt.MapFrom(src => src.AssignEmployee == null ? null : new EmployeeDto
            {
                FullName = src.AssignEmployee.FullName!,
                Email = src.AssignEmployee.Email,
                ImgUrl = src.AssignEmployee.ImgUrl,
                JobPosition = src.AssignEmployee.JobPosition,
                // DepartmentDto = src.AssignEmployee.DepartmentId == null ? null : new DepartmentDto
                // {
                //     FileName = src.AssignEmployee.DepartmentId.FileName!
                // }
            })).ForMember(dest => dest.Project, opt =>
                opt.MapFrom(src => src.Project == null ? null : new ProjectDto
            {
                Name = src.Project.Name!,
            }));
        CreateMap<ApplicationUser, ApplicationUserDto>();
        CreateMap<Tax, TaxDto>();
        CreateMap<Contact, ContactDto>();
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
        CreateMap<TaskList, TaskListDto>();
        CreateMap<Salary, SalaryDto>();
        CreateMap<Shift, ShiftDto>();
        CreateMap<Item, ItemDto>();
        CreateMap<ItemDto, Item>();
        CreateMap<Message, MessageDto>();
        CreateMap<Estimate, EstimateDto>();
        CreateMap<Budget, BudgetDto>();
        CreateMap<BudgetRevenue, BudgetRevenuesSearchDto>();
        CreateMap<BudgetRevenue, BudgetRevenuesDto>();
        CreateMap<BudgetExpenses, BudgetExpensesDto>();
        CreateMap<BudgetExpenses, BudgetExpensesSearchDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Revenue, RevenueDto>();
        CreateMap<Revenue, RevenuesBudget>();
        CreateMap<RevenuesCreateDto,Revenue>();
        CreateMap<Revenue,RevenuesCreateDto>();
        CreateMap<ProvidentFunds, ProvidentFundsDto>();
        //map all expenses properties without purchasedBy   
        CreateMap<ExpensesOfBudget,ExpensesCreateDto >();
        CreateMap<ExpensesCreateDto,ExpensesOfBudget>();
        CreateMap<ExpensesOfBudget,ExpensesBudget >();
        CreateMap<Expenses, ExpensesDto>().ForMember(dest=> 
            dest.PurchasedBy ,opt =>
            opt.MapFrom(src =>src.PurchasedBy == null? null: new EmployeeDto
            {
                FullName = src.PurchasedBy.FullName!,
            }));
        CreateMap<PayrollDeduction, PayrollDeductionDto>();
        CreateMap<PayrollAddition, PayrollAdditionDto>();
        CreateMap<Payment, PaymentDto>();
        CreateMap<Policies, PolicyDto>();
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<Scheduling, ScheduleDto>();
          
        CreateMap<PerformanceAppraisal, PerformanceAppraisalDto>();
        CreateMap<PerformanceIndicator, PerformanceIndicatorDto>();
        CreateMap<GoalList, GoalListDto>();
        CreateMap<GoalType, GoalTypeDto>();
        CreateMap<Trainer, TrainerDto>();
        CreateMap<TaskList, MappedTaskList>();
        CreateMap<TrainingType, TrainingTypeDto>();
        CreateMap<TrainingList, TrainingListDto>();
        CreateMap<Resignation, ResignationDto>();
        CreateMap<Promotion, PromotionDto>();
        CreateMap<Termination, TerminationDto>();
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