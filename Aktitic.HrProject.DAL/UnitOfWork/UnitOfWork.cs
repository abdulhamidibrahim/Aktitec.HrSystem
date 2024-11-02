using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.ClientRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.Repos.GoalListRepo;
using Aktitic.HrProject.DAL.Repos.InvoiceRepo;
using Aktitic.HrProject.DAL.Repos.TrainingListRepo;
using ExpensesOfBudgetRepo = Aktitic.HrProject.DAL.Repos.AttendanceRepo.ExpensesOfBudgetRepo;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    
    #region Fields

    private readonly HrSystemDbContext _context;
    private readonly Lazy<INotesRepo> _notesRepo;
    private readonly Lazy<IApplicationUserRepo> _applicationUser;
    private readonly Lazy<IAttendanceRepo> _attendanceRepo;
    private readonly Lazy<IDepartmentRepo> _departmentRepo;
    private readonly Lazy<IEmployeeRepo> _employeeRepo;
    private readonly Lazy<IClientRepo> _clientRepo;
    private readonly Lazy<ICustomPolicyRepo> _customPolicyRepo;
    private readonly Lazy<IContactsRepo> _contactsRepo;
    private readonly Lazy<IContractsRepo> _contractsRepo;
    private readonly Lazy<ICompanyRepo> _companyRepo;
    private readonly Lazy<IDesignationRepo> _designationRepo;
    private readonly Lazy<IEmployeeProjectsRepo> _employeeProjectsRepo;
    private readonly Lazy<IDocumentRepo> _documentRepo;
    private readonly Lazy<IFilesRepo> _filesRepo;
    private readonly Lazy<IFileUsersRepo> _fileUsersRepo;
    private readonly Lazy<IHolidayRepo> _holidayRepo;
    private readonly Lazy<ILeavesRepo> _leavesRepo;
    private readonly Lazy<ILeaveSettingRepo> _leaveSettingsRepo;
    private readonly Lazy<IProjectRepo> _projectRepo;
    private readonly Lazy<ITaskRepo> _taskRepo;
    private readonly Lazy<IOvertimeRepo> _overtimeRepo;
    private readonly Lazy<IShiftRepo> _shiftRepo;
    private readonly Lazy<ITimesheetRepo> _timeSheetRepo;
    private readonly Lazy<ISchedulingRepo> _schedulingRepo;
    private readonly Lazy<ITaskBoardRepo> _taskBoardRepo;
    private readonly Lazy<ITaxRepo> _taxRepo;
    private readonly Lazy<IMessageRepo> _messageRepo;
    private readonly Lazy<IExpensesRepo> _expensesRepo;
    private readonly Lazy<IPaymentRepo> _paymentRepo;
    private readonly Lazy<ITaskListRepo> _taskList;
    private readonly Lazy<ITrainingListRepo> _trainingListRepo;
    private readonly Lazy<ITrainingTypeRepo> _trainingTypeRepo;
    private readonly Lazy<ITrainerRepo> _trainerRepo;
    private readonly Lazy<IInvoiceRepo> _invoiceRepo;
    private readonly Lazy<ITicketRepo> _ticketRepo;
    private readonly Lazy<ITicketFollowersRepo> _ticketFollowersRepo;
    private readonly Lazy<IEstimateRepo> _estimateRepo;
    private readonly Lazy<IBudgetRevenuesRepo> _budgetRevenuesRepo;
    private readonly Lazy<IBudgetRepo> _budgetRepo;
    private readonly Lazy<IBudgetExpensesRepo> _budgetExpensesRepo;
    private readonly Lazy<IExpensesOfBudgetRepo> _expensesOfBudget;
    private readonly Lazy<IRevenuesRepo> _revenuesRepo;
    private readonly Lazy<IGoalListRepo> _goalListRepo;
    private readonly Lazy<IGoalTypeRepo> _goalTypeRepo;
    private readonly Lazy<IPayrollAdditionRepo> _payrollAdditionRepo;
    private readonly Lazy<IPayrollDeductionRepo> _payrollDeductionRepo;
    private readonly Lazy<ILicenseRepo> _licenseRepo;
    private readonly Lazy<IPolicyRepo> _policyRepo;
    private readonly Lazy<IPayrollOvertimeRepo> _payrollOvertimeRepo;
    private readonly Lazy<IPerformanceIndicatorRepo> _performanceIndicatorRepo;
    private readonly Lazy<IPerformanceAppraisalRepo> _performanceAppraisalRepo;
    private readonly Lazy<IPermissionsRepo> _permissionsRepo;
    private readonly Lazy<IPromotionRepo> _promotionRepo;
    private readonly Lazy<IProvidentFundsRepo> _providentFundsRepo;
    private readonly Lazy<ICategoryRepo> _categoryRepo;
    private readonly Lazy<IResignationRepo> _resignationRepo;
    private readonly Lazy<ISalaryRepo> _salaryRepo;
    private readonly Lazy<ITerminationRepo> _terminationRepo;
    private readonly Lazy<IEventsRepo> _eventsRepo;
    private readonly Lazy<INotificationRepo> _notificationRepo;
    private readonly Lazy<IChatGroupRepo> _chatGroupRepo;
    private readonly Lazy<IChatGroupUsersRepo> _chatGroupUsersRepo;
    private readonly Lazy<IAssetsRepo> _assetsRepo;
    private readonly Lazy<IJobsRepo> _jobsRepo;
    private readonly Lazy<IShortlistsRepo> _shortlistsRepo;
    private readonly Lazy<IInterviewQuestionsRepo> _interviewQuestions;
    private readonly Lazy<IOfferApprovalsRepo> _offerApprovalsRepo;
    private readonly Lazy<IExperiencesRepo> _experiencesRepo;
    private readonly Lazy<ICandidatesRepo> _candidatesRepo;
    private readonly Lazy<IScheduleTimingsRepo> _scheduleTimingsRepo;
    private readonly Lazy<IAptitudeResultsRepo> _aptitudeResultsRepo;
    private readonly Lazy<IJobApplicantsRepo> _jobApplicantsRepo;
    private readonly Lazy<ILogsRepo> _logsRepo;
    private readonly Lazy<IEmailsRepo> _emailsRepo;
    private readonly Lazy<IDocumentFilesRepo> _documentFilesRepo;
    private readonly Lazy<IRevisorsRepo> _revisorsRepo;
    private readonly Lazy<IAppModulesRepo> _appModulesRepo;
    private readonly Lazy<IAppSubModulesRepo> _appSubModulesRepo;
    private readonly Lazy<IAppPagesRepo> _appPagesRepo;
    private readonly Lazy<ICompanyModulesRepo> _companyModulesRepo;
    private readonly Lazy<ICompanyRolesRepo> _companyRolesRepo;
    private readonly Lazy<IRolePermissionsRepo> _rolePermissionsRepo;
    private readonly Lazy<INotificationSettingsRepo> _notificationSettingsRepo;
    private readonly Lazy<IItemRepo> _itemRepo;
    private readonly Lazy<IFamilyInformantionRepo> _familyInformantionRepo;
    private readonly Lazy<IEmergencyContactRepo> _emergencyContactRepo;
    private readonly Lazy<IEducationInformationRepo> _educationInformationRepo;
    private readonly Lazy<IProfileExperienceRepo> _profileExperienceRepo;

    #endregion
    
   public UnitOfWork(HrSystemDbContext context)
{
    _context = context;
    
    _notesRepo = new(() => new NotesRepo(_context));
    _notificationSettingsRepo = new(() => new NotificationSettingsRepo(_context));
    
    _applicationUser = new(() => new ApplicationUserRepo(_context));
    _attendanceRepo = new(() => new AttendanceRepo(_context));
    _departmentRepo = new(() => new DepartmentRepo(_context));
    _employeeRepo = new(() => new EmployeeRepo(_context));
    _clientRepo = new(() => new ClientRepo(_context));
    _customPolicyRepo = new(() => new CustomPolicyRepo(_context));
    _contactsRepo = new(() => new ContactsRepo(_context));
    _contractsRepo = new(() => new ContractsRepo(_context));
    _companyRepo = new(() => new CompanyRepo(_context));
    _designationRepo = new(() => new DesignationRepo(_context));
    _employeeProjectsRepo = new(() => new EmployeeProjectsRepo(_context));
    _documentRepo = new(() => new DocumentRepo(_context));
    _filesRepo = new(() => new FilesRepo(_context));
    _fileUsersRepo = new(() => new FileUsersRepo(_context));
    _holidayRepo = new(() => new HolidayRepo(_context));
    _leavesRepo = new(() => new LeavesRepo(_context));
    _leaveSettingsRepo = new(() => new LeaveSettingRepo(_context));
    _projectRepo = new(() => new ProjectRepo(_context));
    _taskRepo = new(() => new TaskRepo(_context));
    _overtimeRepo = new(() => new OvertimeRepo(_context));
    _shiftRepo = new(() => new ShiftRepo(_context));
    _licenseRepo = new(() => new LicenseRepo(context));
    _timeSheetRepo = new(() => new TimesheetRepo(_context));
    _schedulingRepo = new(() => new SchedulingRepo(_context));
    _taskBoardRepo = new(() => new TaskBoardRepo(_context));
    _taxRepo = new(() => new TaxRepo(_context));
    _itemRepo = new(() => new ItemRepo(_context));
    _messageRepo = new(() => new MessageRepo(_context));
    _expensesRepo = new(() => new ExpensesRepo(_context));
    _paymentRepo = new(() => new PaymentRepo(_context));
    _taskList = new(() => new TaskListRepo(_context));
    _trainingListRepo = new(() => new TrainingListRepo(_context));
    _trainingTypeRepo = new(() => new TrainingTypeRepo(_context));
    _trainerRepo = new(() => new TrainerRepo(_context));
    _invoiceRepo = new(() => new InvoiceRepo(_context));
    _ticketRepo = new(() => new TicketRepo(_context));
    _policyRepo = new(() => new PolicyRepo(context));
    _ticketFollowersRepo = new(() => new TicketFollowersRepo(_context));
    _estimateRepo = new(() => new EstimateRepo(_context));
    _budgetRevenuesRepo = new(() => new BudgetRevenuesRepo(_context));
    _budgetRepo = new(() => new BudgetRepo(_context));
    _budgetExpensesRepo = new(() => new BudgetExpensesRepo(_context));
    _expensesOfBudget = new(() => new ExpensesOfBudgetRepo(_context));
    _revenuesRepo = new(() => new RevenuesRepo(_context));
    _goalListRepo = new(() => new GoalListRepo(_context));
    _goalTypeRepo = new(() => new GoalTypeRepo(_context));
    _payrollAdditionRepo = new(() => new PayrollAdditionRepo(_context));
    _payrollDeductionRepo = new(() => new PayrollDeductionRepo(_context));
    _payrollOvertimeRepo = new(() => new PayrollOvertimeRepo(_context));
    _performanceIndicatorRepo = new(() => new PerformanceIndicatorRepo(_context));
    _performanceAppraisalRepo = new(() => new PerformanceAppraisalRepo(_context));
    _permissionsRepo = new(() => new PermissionsRepo(_context));
    _promotionRepo = new(() => new PromotionRepo(_context));
    _providentFundsRepo = new(() => new ProvidentFundsRepo(_context));
    _categoryRepo = new(() => new CategoryRepo(_context));
    _resignationRepo = new(() => new ResignationRepo(_context));
    _salaryRepo = new(() => new SalaryRepo(_context));
    _terminationRepo = new(() => new TerminationRepo(_context));
    _eventsRepo = new(() => new EventsRepo(_context));
    _notificationRepo = new(() => new NotificationRepo(_context));
    _chatGroupRepo = new(() => new ChatGroupRepo(_context));
    _chatGroupUsersRepo = new(() => new ChatGroupUsersRepo(_context));
    _assetsRepo = new(() => new AssetsRepo(_context));
    _jobsRepo = new(() => new JobsRepo(_context));
    _shortlistsRepo = new(() => new ShortlistsRepo(_context));
    _interviewQuestions = new(() => new InterviewQuestionsRepo(_context));
    _offerApprovalsRepo = new(() => new OfferApprovalsRepo(_context));
    _experiencesRepo = new(() => new ExperiencesRepo(_context));
    _candidatesRepo = new(() => new CandidatesRepo(_context));
    _scheduleTimingsRepo = new(() => new ScheduleTimingsRepo(_context));
    _aptitudeResultsRepo = new(() => new AptitudeResultsRepo(_context));
    _jobApplicantsRepo = new(() => new JobApplicantsRepo(_context));
    _logsRepo = new(() => new LogsRepo(_context));
    _emailsRepo = new(() => new EmailsRepo(_context));
    _documentFilesRepo = new(() => new DocumentFilesRepo(_context));
    _revisorsRepo = new(() => new RevisorsRepo(_context));
    _appModulesRepo = new(() => new AppModulesRepo(_context));
    _appSubModulesRepo = new(() => new AppSubModulesRepo(_context));
    _appPagesRepo = new(() => new AppPagesRepo(_context));
    _companyModulesRepo = new(() => new CompanyModulesRepo(_context));
    _companyRolesRepo = new(() => new CompanyRolesRepo(_context));
    _rolePermissionsRepo = new(() => new RolePermissionsRepo(_context));
    _emergencyContactRepo = new(() => new EmergencyContactRepo(_context));
    _familyInformantionRepo = new(() => new FamilyInformantionRepo(_context));
    _educationInformationRepo = new(() => new EducationInformationRepo(_context));
    _profileExperienceRepo = new(() => new ProfileExperienceRepo(_context));
}
   
   
    public Task CommitAsync()
    {
        return _context.SaveChangesAsync();
    }

    #region ImplementMembers
    
    public INotesRepo Notes => _notesRepo.Value;
    public IApplicationUserRepo ApplicationUser => _applicationUser.Value;
    public IAttendanceRepo Attendance => _attendanceRepo.Value;
    public IDepartmentRepo Department => _departmentRepo.Value;
    public IEmployeeRepo Employee => _employeeRepo.Value;
    public IClientRepo Client => _clientRepo.Value;
    public ICustomPolicyRepo CustomPolicy => _customPolicyRepo.Value;
    public IContactsRepo Contacts => _contactsRepo.Value;
    public IContractsRepo Contracts => _contractsRepo.Value;
    public ICompanyRepo Company => _companyRepo.Value;
    public IDesignationRepo Designation => _designationRepo.Value;
    public IEmployeeProjectsRepo EmployeeProjects => _employeeProjectsRepo.Value;
    public IDocumentRepo Documents => _documentRepo.Value;
    public IFilesRepo Files => _filesRepo.Value;
    public IFileUsersRepo FileUsers => _fileUsersRepo.Value;
    public IHolidayRepo Holiday => _holidayRepo.Value;
    public ILeavesRepo Leaves => _leavesRepo.Value;
    public ILeaveSettingRepo LeaveSettings => _leaveSettingsRepo.Value;
    public IProjectRepo Project => _projectRepo.Value;
    public ITaskRepo Tasks => _taskRepo.Value;
    public IOvertimeRepo Overtime => _overtimeRepo.Value;
    public IShiftRepo Shift => _shiftRepo.Value;
    public ITimesheetRepo TimeSheet => _timeSheetRepo.Value;
    public ISchedulingRepo Scheduling => _schedulingRepo.Value;
    public ITaskBoardRepo TaskBoard => _taskBoardRepo.Value;
    public ITaxRepo Tax => _taxRepo.Value;
    public IItemRepo Item => _itemRepo.Value;
    public IMessageRepo Message => _messageRepo.Value;
    public IExpensesRepo Expenses => _expensesRepo.Value;
    public IPaymentRepo Payment => _paymentRepo.Value;
    public ITaskListRepo TaskList => _taskList.Value;
    public ITrainingListRepo TrainingList => _trainingListRepo.Value;
    public ITrainingTypeRepo TrainingType => _trainingTypeRepo.Value;
    public ITrainerRepo Trainer => _trainerRepo.Value;
    public IInvoiceRepo Invoice => _invoiceRepo.Value;
    public ITicketRepo Ticket => _ticketRepo.Value;
    public ITicketFollowersRepo TicketFollowers => _ticketFollowersRepo.Value;
    public IEstimateRepo Estimate => _estimateRepo.Value;
    public IBudgetRevenuesRepo BudgetsRevenue => _budgetRevenuesRepo.Value;
    public IBudgetRepo Budget => _budgetRepo.Value;
    public IBudgetExpensesRepo BudgetExpenses => _budgetExpensesRepo.Value;
    public IExpensesOfBudgetRepo ExpensesOfBudget => _expensesOfBudget.Value;
    public IRevenuesRepo Revenues => _revenuesRepo.Value;
    public IGoalListRepo GoalList => _goalListRepo.Value;
    public IGoalTypeRepo GoalType => _goalTypeRepo.Value;
    public IPayrollAdditionRepo PayrollAddition => _payrollAdditionRepo.Value;
    public IPayrollDeductionRepo PayrollDeduction => _payrollDeductionRepo.Value;
    public ILicenseRepo License => _licenseRepo.Value;
    public IPolicyRepo Policies => _policyRepo.Value;
    public IPayrollOvertimeRepo PayrollOvertime => _payrollOvertimeRepo.Value;
    public IPerformanceIndicatorRepo PerformanceIndicator => _performanceIndicatorRepo.Value;
    public IPerformanceAppraisalRepo PerformanceAppraisal => _performanceAppraisalRepo.Value;
    public IPermissionsRepo Permission => _permissionsRepo.Value;
    public IPromotionRepo Promotion => _promotionRepo.Value;
    public IProvidentFundsRepo ProvidentFunds => _providentFundsRepo.Value;
    public ICategoryRepo Category => _categoryRepo.Value;
    public IResignationRepo Resignation => _resignationRepo.Value;
    public ISalaryRepo Salary => _salaryRepo.Value;
    public ITerminationRepo Termination => _terminationRepo.Value;
    public IEventsRepo Events => _eventsRepo.Value;
    public INotificationRepo Notification => _notificationRepo.Value;
    public IChatGroupRepo ChatGroup => _chatGroupRepo.Value;
    public IChatGroupUsersRepo ChatGroupUsers => _chatGroupUsersRepo.Value;
    public IAssetsRepo Assets => _assetsRepo.Value;
    public IJobsRepo Jobs => _jobsRepo.Value;
    public IShortlistsRepo Shortlists => _shortlistsRepo.Value;
    public IInterviewQuestionsRepo InterviewQuestions => _interviewQuestions.Value;
    public IOfferApprovalsRepo OfferApprovals => _offerApprovalsRepo.Value;
    public IExperiencesRepo Experiences => _experiencesRepo.Value;
    public ICandidatesRepo Candidates => _candidatesRepo.Value;
    public IScheduleTimingsRepo ScheduleTimings => _scheduleTimingsRepo.Value;
    public IAptitudeResultsRepo AptitudeResults => _aptitudeResultsRepo.Value;
    public IJobApplicantsRepo JobApplicants => _jobApplicantsRepo.Value;
    public ILogsRepo Logs => _logsRepo.Value;
    public IEmailsRepo Emails => _emailsRepo.Value;
    public IDocumentFilesRepo DocumentFiles => _documentFilesRepo.Value;
    public IRevisorsRepo Revisors => _revisorsRepo.Value;
    public IAppModulesRepo AppModules => _appModulesRepo.Value;
    public IAppSubModulesRepo AppSubModules => _appSubModulesRepo.Value;
    public IAppPagesRepo AppPages => _appPagesRepo.Value;
    public ICompanyModulesRepo CompanyModules => _companyModulesRepo.Value;
    public ICompanyRolesRepo CompanyRoles => _companyRolesRepo.Value;
    public IRolePermissionsRepo RolePermissions => _rolePermissionsRepo.Value;
    public INotificationSettingsRepo NotificationSettings => _notificationSettingsRepo.Value;
    public IFamilyInformantionRepo FamilyInformation => _familyInformantionRepo.Value;
    public IEmergencyContactRepo EmergencyContact => _emergencyContactRepo.Value;
    public IEducationInformationRepo EducationInformation => _educationInformationRepo.Value;
    public IProfileExperienceRepo ProfileExperience => _profileExperienceRepo.Value;
    
    #endregion
    
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