using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IGenericRepo<Notes> Notes { get; }
    IGenericRepo<Attendance> Attendance { get; }
    IGenericRepo<Department> Department { get; }
    IGenericRepo<Employee> Employee { get; }
    IGenericRepo<Client> Client { get; }
    IGenericRepo<CustomPolicy> CustomPolicy { get; }
    IGenericRepo<Designation> Designation { get; }
    IGenericRepo<EmployeeProjects> EmployeeProjects { get; }
    IGenericRepo<File> File { get; }
    IGenericRepo<Holiday> Holiday { get; }
    IGenericRepo<Leaves> Leaves { get; }
    IGenericRepo<LeaveSettings> LeaveSettings { get; }
    IGenericRepo<Project> Project { get; }
    IGenericRepo<Overtime> Overtime { get; }
    IGenericRepo<Shift> Shift { get; }
    IGenericRepo<TimeSheet> TimeSheet { get; }
    IGenericRepo<Permission> Permission { get; }
    IGenericRepo<Scheduling> Scheduling { get; }
    IGenericRepo<Task> Task { get; }
    
    IGenericRepo<TaskBoard> TaskBoard { get; }
    IGenericRepo<TaskList> TaskList { get; }
    IGenericRepo<Ticket> Ticket { get; }
    IGenericRepo<TicketFollowers> TicketFollowers { get; }
    IGenericRepo<Estimate> Estimate { get; }
    IGenericRepo<Item> Item { get; }
    IGenericRepo<Message> Message { get; }
    IGenericRepo<Expenses> Expenses { get; }
    IGenericRepo<Payment> Payment { get; }
    IGenericRepo<Tax> Tax { get; }
    IGenericRepo<ProvidentFunds> ProvidentFunds { get; }
    IGenericRepo<Category> Category { get; }
    IGenericRepo<BudgetRevenue> BudgetsRevenue { get; }
    IGenericRepo<Budget> Budget { get; }
    
    
}