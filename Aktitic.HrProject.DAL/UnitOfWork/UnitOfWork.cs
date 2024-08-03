using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.ClientRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.Repos.GoalListRepo;
using Aktitic.HrProject.DAL.Repos.InvoiceRepo;
using Aktitic.HrProject.DAL.Repos.TrainingListRepo;
using ExpensesOfBudgetRepo = Aktitic.HrProject.DAL.Repos.AttendanceRepo.ExpensesOfBudgetRepo;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly HrSystemDbContext _context;
    private NotesRepo _notesRepo;
    private AttendanceRepo _attendanceRepo;
    private ApplicationUserRepo _applicationUser;
    private DepartmentRepo _departmentRepo;
    private EmployeeRepo _employeeRepo;
    private ClientRepo _clientRepo;
    private CustomPolicyRepo _customPolicyRepo;
    private ContactsRepo _contactsRepo;
    private ContractsRepo _contractsRepo;
    private CompanyRepo _companyRepo;
    private DesignationRepo _designationRepo;
    private EmployeeProjectsRepo _employeeProjectsRepo;
    private FileRepo _fileRepo;
    private FileUsersRepo _fileUsersRepo;
    private HolidayRepo _holidayRepo;
    private LeavesRepo _leavesRepo;
    private LeaveSettingRepo _leaveSettingsRepo;
    private ProjectRepo _projectRepo;
    private TaskRepo _taskRepo;
    private OvertimeRepo _overtimeRepo;
    private ShiftRepo _shiftRepo;
    private TimesheetRepo _timeSheetRepo;
    private SchedulingRepo _schedulingRepo;
    private TaskBoardRepo _taskBoardRepo;
    private TaxRepo _taxRepo;
    private MessageRepo _messageRepo;
    private PaymentRepo _paymentRepo;
    private BudgetRepo _budgetRepo;
    private BudgetExpensesRepo _budgetExpensesRepo;
    private BudgetRevenuesRepo _budgetRevenuesRepo;
    private ExpensesOfBudgetRepo _expensesOfBudget;
    private TaskListRepo _taskList;
    private CategoryRepo _categoryRepo;
    private EstimateRepo _estimateRepo;
    private ExpensesRepo _expensesRepo;
    private GoalListRepo _goalListRepo;
    private GoalTypeRepo _goalTypeRepo;
    private InvoiceRepo _invoiceRepo;
    private ItemRepo _itemRepo;
    private PayrollAdditionRepo _payrollAdditionRepo;
    private PayrollDeductionRepo _payrollDeductionRepo;
    private PayrollOvertimeRepo _payrollOvertimeRepo;
    private PerformanceIndicatorRepo _performanceIndicatorRepo;
    private PerformanceAppraisalRepo _performanceAppraisalRepo;
    private PermissionsRepo _permissionsRepo;
    private LicenseRepo _licenseRepo;
    private PolicyRepo _policyRepo;
    private PromotionRepo _promotionRepo;
    private ProvidentFundsRepo _providentFundsRepo;
    private ResignationRepo _resignationRepo;
    private RevenuesRepo _revenuesRepo;
    private SalaryRepo _salaryRepo;
    private TerminationRepo _terminationRepo;
    private TicketFollowersRepo _ticketFollowersRepo;
    private TicketRepo _ticketRepo;
    private TrainerRepo _trainerRepo;
    private ITrainingListRepo _trainingListRepo;
    private TrainingTypeRepo _trainingTypeRepo;
    private EventsRepo _eventsRepo;
    private NotificationRepo _notificationRepo;
    private ChatGroupRepo _chatGroupRepo;
    private ChatGroupUsersRepo _chatGroupUsersRepo;


    // complete all other repos
        
    
    
    public UnitOfWork(HrSystemDbContext context)
    {
        _context = context;
        
    }
   public INotesRepo Notes => _notesRepo ??= new NotesRepo(_context);
    
    public IApplicationUserRepo ApplicationUser => _applicationUser ??= new ApplicationUserRepo(_context);
    public IAttendanceRepo Attendance => _attendanceRepo ??= new AttendanceRepo(_context);
    
    public IDepartmentRepo Department => _departmentRepo ??= new DepartmentRepo(_context);
    
    public IEmployeeRepo Employee => _employeeRepo ??= new EmployeeRepo(_context);
    
    public IClientRepo Client => _clientRepo ??= new ClientRepo(_context);
    
    public ICustomPolicyRepo CustomPolicy => _customPolicyRepo ??= new CustomPolicyRepo(_context);
    public IContactsRepo Contacts => _contactsRepo ??= new ContactsRepo(_context);
    public IContractsRepo Contracts => _contractsRepo ??= new ContractsRepo(_context);
    public ICompanyRepo Company => _companyRepo ??= new CompanyRepo(_context);
    
    public IDesignationRepo Designation => _designationRepo ??= new DesignationRepo(_context);
    
    public IEmployeeProjectsRepo EmployeeProjects => _employeeProjectsRepo ??= new EmployeeProjectsRepo(_context);
    
    public IFileRepo File => _fileRepo ??= new FileRepo(_context);
    public IFileUsersRepo FileUsers => _fileUsersRepo ??= new FileUsersRepo(_context);
    
    public IHolidayRepo Holiday => _holidayRepo ??= new HolidayRepo(_context);
    
    public ILeavesRepo Leaves => _leavesRepo ??= new LeavesRepo(_context);
    
    public ILeaveSettingRepo LeaveSettings => _leaveSettingsRepo ??= new LeaveSettingRepo(_context);
    
    public IProjectRepo Project => _projectRepo ??= new ProjectRepo(_context);
    
    public ITaskRepo Task => _taskRepo ??= new TaskRepo(_context);
    
    public IOvertimeRepo Overtime => _overtimeRepo ??= new OvertimeRepo(_context);
    
    public IShiftRepo Shift => _shiftRepo ??= new ShiftRepo(_context);
    
    public ITimesheetRepo TimeSheet => _timeSheetRepo ??= new TimesheetRepo(_context);
    
    public ISchedulingRepo Scheduling => _schedulingRepo ??= new SchedulingRepo(_context);
    
    public ITaskBoardRepo TaskBoard => _taskBoardRepo ??= new TaskBoardRepo(_context);
    
    public ITaxRepo Tax => _taxRepo ??= new TaxRepo(_context);

    public IItemRepo Item => _itemRepo ??= new ItemRepo(_context);
    public IMessageRepo Message => _messageRepo ??= new MessageRepo(_context);
    public IExpensesRepo Expenses => _expensesRepo ??= new ExpensesRepo(_context);

    public IPaymentRepo Payment => _paymentRepo ??= new PaymentRepo(_context);
        
    public ITaskListRepo TaskList => _taskList ??= new TaskListRepo(_context);
        
    public ITrainingListRepo TrainingList => _trainingListRepo ??= new TrainingListRepo(_context);
    
    public ITrainingTypeRepo TrainingType => _trainingTypeRepo ??= new TrainingTypeRepo(_context);
    
    public ITrainerRepo Trainer => _trainerRepo ??= new TrainerRepo(_context);
    
    
    public IInvoiceRepo Invoice => _invoiceRepo ??= new InvoiceRepo(_context);
        
    public ITicketRepo Ticket => _ticketRepo ??= new TicketRepo(_context);
    
    public ITicketFollowersRepo TicketFollowers => _ticketFollowersRepo ??= new TicketFollowersRepo(_context);
    
    public IEstimateRepo Estimate => _estimateRepo ??= new EstimateRepo(_context);
    public IBudgetRevenuesRepo BudgetsRevenue => _budgetRevenuesRepo ??= new BudgetRevenuesRepo(_context);
    public IBudgetRepo Budget => _budgetRepo ??= new BudgetRepo(_context);
    public IBudgetExpensesRepo BudgetExpenses => _budgetExpensesRepo ??= new BudgetExpensesRepo(_context);
    public IExpensesOfBudgetRepo ExpensesOfBudget => _expensesOfBudget ??= new ExpensesOfBudgetRepo(_context);
    public IRevenuesRepo Revenues => _revenuesRepo ??= new RevenuesRepo(_context);
    public IGoalListRepo GoalList => _goalListRepo ??= new GoalListRepo(_context);
    public IGoalTypeRepo GoalType => _goalTypeRepo ??= new GoalTypeRepo(_context);
    public IPayrollAdditionRepo PayrollAddition => _payrollAdditionRepo ??= new PayrollAdditionRepo(_context);
    public IPayrollDeductionRepo PayrollDeduction => _payrollDeductionRepo ??= new PayrollDeductionRepo(_context);
    public ILicenseRepo License => _licenseRepo ??= new LicenseRepo(_context);
    public IPolicyRepo Policies => _policyRepo ??= new PolicyRepo(_context);
    public IPayrollOvertimeRepo PayrollOvertime => _payrollOvertimeRepo ??= new PayrollOvertimeRepo(_context);
    public IPerformanceIndicatorRepo PerformanceIndicator => _performanceIndicatorRepo ??= new PerformanceIndicatorRepo(_context);
    public IPerformanceAppraisalRepo PerformanceAppraisal => _performanceAppraisalRepo ??= new PerformanceAppraisalRepo(_context);
    public IPermissionsRepo Permission => _permissionsRepo ??= new PermissionsRepo(_context);
    public IPromotionRepo Promotion => _promotionRepo ??= new PromotionRepo(_context);
    public IProvidentFundsRepo ProvidentFunds => _providentFundsRepo ??= new ProvidentFundsRepo(_context);
    public ICategoryRepo Category => _categoryRepo ??= new CategoryRepo(_context);
    public IResignationRepo Resignation => _resignationRepo ??= new ResignationRepo(_context);
    public ISalaryRepo Salary => _salaryRepo ??= new SalaryRepo(_context);
    public ITerminationRepo Termination => _terminationRepo ??= new TerminationRepo(_context);
    public IEventsRepo Events => _eventsRepo ??= new EventsRepo(_context);
    public INotificationRepo Notification => _notificationRepo ??= new NotificationRepo(_context);
    public IChatGroupRepo ChatGroup => _chatGroupRepo ??= new ChatGroupRepo(_context);
    public IChatGroupUsersRepo ChatGroupUsers => _chatGroupUsersRepo ??= new ChatGroupUsersRepo(_context);
                
    

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

   
}