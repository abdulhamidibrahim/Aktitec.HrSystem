using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly HrSystemDbContext _context;

    public UnitOfWork(HrSystemDbContext context)
    {
        _context = context;
        Budget= new GenericRepo<Budget>(context);
        BudgetsRevenue= new GenericRepo<BudgetRevenue>(context);
        Category= new GenericRepo<Category>(context);
        ProvidentFunds= new GenericRepo<ProvidentFunds>(context);
        Tax= new GenericRepo<Tax>(context);
        Payment= new GenericRepo<Payment>(context);
        Expenses= new GenericRepo<Expenses>(context);
        Estimate = new GenericRepo<Estimate>(context);
        Item = new GenericRepo<Item>(context);
        Message = new GenericRepo<Message>(context);
        Notes = new GenericRepo<Notes>(context);
        Attendance = new GenericRepo<Attendance>(context);
        Department = new GenericRepo<Department>(context);
        Employee = new GenericRepo<Employee>(context);
        Client = new GenericRepo<Client>(context);
        CustomPolicy = new GenericRepo<CustomPolicy>(context);
        Designation = new GenericRepo<Designation>(context);
        EmployeeProjects = new GenericRepo<EmployeeProjects>(context);
        File = new GenericRepo<File>(context);
        Holiday = new GenericRepo<Holiday>(context);
        Leaves = new GenericRepo<Leaves>(context);
        LeaveSettings = new GenericRepo<LeaveSettings>(context);
        Project = new GenericRepo<Project>(context);
        Overtime = new GenericRepo<Overtime>(context);                
        Task = new GenericRepo<Task>(context);
        Shift = new GenericRepo<Shift>(context);
        TimeSheet = new GenericRepo<TimeSheet>(context);
        Permission = new GenericRepo<Permission>(context);
        Scheduling = new GenericRepo<Scheduling>(context);
        TaskBoard = new GenericRepo<TaskBoard>(context);
        TaskList = new GenericRepo<TaskList>(context);
        Ticket = new GenericRepo<Ticket>(context);
        TicketFollowers = new GenericRepo<TicketFollowers>(context);
        
    }
    

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public IGenericRepo<Notes> Notes { get; }
    public IGenericRepo<Attendance> Attendance { get; }
    public IGenericRepo<Department> Department { get; }
    public IGenericRepo<Employee> Employee { get; }
    public IGenericRepo<Client> Client { get; }
    public IGenericRepo<CustomPolicy> CustomPolicy { get; }
    public IGenericRepo<Designation> Designation { get; }
    public IGenericRepo<EmployeeProjects> EmployeeProjects { get; }
    public IGenericRepo<File> File { get; }
    public IGenericRepo<Holiday> Holiday { get; }
    public IGenericRepo<Leaves> Leaves { get; }
    public IGenericRepo<LeaveSettings> LeaveSettings { get; }
    public IGenericRepo<Project> Project { get; }
    public IGenericRepo<Overtime> Overtime { get; }
    public IGenericRepo<Shift> Shift { get; }
    public IGenericRepo<TimeSheet> TimeSheet { get; }
    public IGenericRepo<Permission> Permission { get; }
    public IGenericRepo<Scheduling> Scheduling { get; }
    public IGenericRepo<Task> Task { get; }
    public IGenericRepo<TaskBoard> TaskBoard { get; }
    public IGenericRepo<TaskList> TaskList { get; }
    public IGenericRepo<Ticket> Ticket { get; }
    public IGenericRepo<TicketFollowers> TicketFollowers { get; }
    public IGenericRepo<Estimate> Estimate { get; }
    public IGenericRepo<Item> Item { get; }
    public IGenericRepo<Message> Message { get; }
    public IGenericRepo<Expenses> Expenses { get; }
    public IGenericRepo<Payment> Payment { get; }
    public IGenericRepo<Tax> Tax { get; }
    public IGenericRepo<ProvidentFunds> ProvidentFunds { get; }
    public IGenericRepo<Category> Category { get; }
    public IGenericRepo<BudgetRevenue> BudgetsRevenue { get; }
    public IGenericRepo<Budget> Budget { get; }
}