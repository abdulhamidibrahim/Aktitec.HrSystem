using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.ClientRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.Repos.GoalListRepo;
using Aktitic.HrProject.DAL.Repos.InvoiceRepo;
using Aktitic.HrProject.DAL.Repos.TrainingListRepo;
using ExpensesOfBudgetRepo = Aktitic.HrProject.DAL.Repos.AttendanceRepo.ExpensesOfBudgetRepo;

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
    private DocumentRepo _documentRepo;
    private FilesRepo _filesRepo;
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
    private TrainingListRepo _trainingListRepo;
    private TrainingTypeRepo _trainingTypeRepo;
    private EventsRepo _eventsRepo;
    private NotificationRepo _notificationRepo;
    private ChatGroupRepo _chatGroupRepo;
    private ChatGroupUsersRepo _chatGroupUsersRepo;
    private UserUtility _userUtility;
    private AssetsRepo _assetsRepo;
    private JobsRepo _jobsRepo;
    private ShortlistsRepo _shortlistsRepo;
    private InterviewQuestionsRepo _interviewQuestions;
    private OfferApprovalsRepo _offerApprovalsRepo;
    private ExperiencesRepo _experiencesRepo;
    private CandidatesRepo _candidatesRepo;
    private ScheduleTimingsRepo _scheduleTimingsRepo;
    private AptitudeResultsRepo _aptitudeResultsRepo;
    private JobApplicantsRepo _jobApplicantsRepo;
    private LogsRepo _logsRepo;
    private EmailsRepo _emailsRepo;
    private DocumentFilesRepo _documentFilesRepo;
    private RevisorsRepo _revisorsRepo;
    private AppModulesRepo _appModulesRepo;
    private AppSubModulesRepo _appSubModulesRepo;
    private AppPagesRepo _appPagesRepo;
    private CompanyModulesRepo _companyModulesRepo;
    private CompanyRolesRepo _companyRolesRepo;
    private RolePermissionsRepo _rolePermissionsRepo;


    // complete all other repos
        
    
    
    public UnitOfWork(HrSystemDbContext context)
    {
        _context = context;
        
    }

    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
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
    
    public IDocumentRepo Documents => _documentRepo ??= new DocumentRepo(_context);
    public IFilesRepo Files => _filesRepo ??= new FilesRepo(_context);
    public IFileUsersRepo FileUsers => _fileUsersRepo ??= new FileUsersRepo(_context);
    
    public IHolidayRepo Holiday => _holidayRepo ??= new HolidayRepo(_context);
    
    public ILeavesRepo Leaves => _leavesRepo ??= new LeavesRepo(_context);
    
    public ILeaveSettingRepo LeaveSettings => _leaveSettingsRepo ??= new LeaveSettingRepo(_context);
    
    public IProjectRepo Project => _projectRepo ??= new ProjectRepo(_context);
    
    public ITaskRepo Tasks => _taskRepo ??= new TaskRepo(_context);
    
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
    public IAssetsRepo Assets => _assetsRepo ??= new AssetsRepo(_context);
    public IJobsRepo Jobs => _jobsRepo ??= new JobsRepo(_context);
    public IShortlistsRepo Shortlists => _shortlistsRepo ??= new ShortlistsRepo(_context);
    public IInterviewQuestionsRepo InterviewQuestions => _interviewQuestions ??= new InterviewQuestionsRepo(_context);
    public IOfferApprovalsRepo OfferApprovals => _offerApprovalsRepo ??= new OfferApprovalsRepo(_context);
    public IExperiencesRepo Experiences => _experiencesRepo ??= new ExperiencesRepo(_context);
    public ICandidatesRepo Candidates => _candidatesRepo ??= new CandidatesRepo(_context);
    public IScheduleTimingsRepo ScheduleTimings => _scheduleTimingsRepo ??= new ScheduleTimingsRepo(_context);
    public IAptitudeResultsRepo AptitudeResults => _aptitudeResultsRepo ??= new AptitudeResultsRepo(_context);
    public IJobApplicantsRepo JobApplicants => _jobApplicantsRepo ??= new JobApplicantsRepo(_context);
    public ILogsRepo Logs => _logsRepo ??= new LogsRepo(_context);
    public IEmailsRepo Emails => _emailsRepo ??= new EmailsRepo(_context);
    public IDocumentFilesRepo DocumentFiles => _documentFilesRepo ??= new DocumentFilesRepo(_context);
    public IRevisorsRepo Revisors => _revisorsRepo ??= new RevisorsRepo(_context);
    public IAppModulesRepo AppModules => _appModulesRepo ??= new AppModulesRepo(_context);
    public IAppSubModulesRepo AppSubModules => _appSubModulesRepo ??= new AppSubModulesRepo(_context);
    public IAppPagesRepo AppPages => _appPagesRepo ??= new AppPagesRepo(_context);
    public ICompanyModulesRepo CompanyModules => _companyModulesRepo ??= new CompanyModulesRepo(_context);
    public ICompanyRolesRepo CompanyRoles => _companyRolesRepo ??= new CompanyRolesRepo(_context);
    public IRolePermissionsRepo RolePermissions => _rolePermissionsRepo ??= new RolePermissionsRepo(_context);


    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public Task BeginTransactionAsync()
    {
        _context.Database.BeginTransaction();
        return Task.CompletedTask;
    }

    public Task RollbackAsync()
    {
        _context.Database.RollbackTransaction();
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _context.Dispose();
    }

   
}