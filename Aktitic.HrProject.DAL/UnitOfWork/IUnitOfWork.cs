using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.Repos.InvoiceRepo;

namespace Aktitic.HrProject.DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task RollbackAsync();
    
    Task CommitAsync();

    INotesRepo Notes { get; }
    IApplicationUserRepo ApplicationUser { get; }
    IAttendanceRepo Attendance { get; }
    IDepartmentRepo Department { get; }
    IEmployeeRepo Employee { get; }
    IClientRepo Client { get; }
    ICustomPolicyRepo CustomPolicy { get; }
    IContactsRepo Contacts { get; }
    IContractsRepo Contracts { get; }
    ICompanyRepo Company { get; }
    IDesignationRepo Designation { get; }
    IEmployeeProjectsRepo EmployeeProjects { get; }
    IDocumentRepo Documents { get; }
    IFilesRepo Files { get; }
    IFileUsersRepo FileUsers { get; }
    IHolidayRepo Holiday { get; }
    ILeavesRepo Leaves { get; }
    ILeaveSettingRepo LeaveSettings { get; }
    IProjectRepo Project { get; }
    IOvertimeRepo Overtime { get; }
    IShiftRepo Shift { get; }
    ITimesheetRepo TimeSheet { get; }
    IPermissionsRepo Permission { get; }
    ISchedulingRepo Scheduling { get; }
    ITaskRepo Tasks { get; }
    
    ITaskBoardRepo TaskBoard { get; }
    ITaskListRepo TaskList { get; }
    ITicketRepo Ticket { get; }
    ITicketFollowersRepo TicketFollowers { get; }
    IEstimateRepo Estimate { get; }
    IItemRepo Item { get; }
    IMessageRepo Message { get; }
    IExpensesRepo Expenses { get; }
    IPaymentRepo Payment { get; }
    ITaxRepo Tax { get; }
    IProvidentFundsRepo ProvidentFunds { get; }
    ICategoryRepo Category { get; }
    IBudgetRevenuesRepo BudgetsRevenue { get; }
    IBudgetRepo Budget { get; }
    IBudgetExpensesRepo BudgetExpenses { get; }
    IExpensesOfBudgetRepo ExpensesOfBudget { get; }
    ISalaryRepo Salary { get; }
    ILicenseRepo License { get; }
    IPolicyRepo Policies { get; }
    IPayrollOvertimeRepo PayrollOvertime { get; }
    IPayrollDeductionRepo PayrollDeduction { get; }
    IPayrollAdditionRepo PayrollAddition { get; }
    ITerminationRepo Termination { get; }
    IResignationRepo Resignation { get; }
    IRevenuesRepo Revenues { get; }
    IPromotionRepo Promotion { get; }
    ITrainerRepo Trainer { get; }
    ITrainingTypeRepo TrainingType { get; }
    ITrainingListRepo TrainingList { get; }
    IGoalTypeRepo GoalType { get; }
    IGoalListRepo GoalList { get; }
    IInvoiceRepo Invoice { get; }
    IEventsRepo Events { get; }
    IPerformanceAppraisalRepo PerformanceAppraisal { get; }
    IPerformanceIndicatorRepo PerformanceIndicator { get; }
    INotificationRepo Notification { get; }
    IChatGroupRepo ChatGroup { get; }
    IChatGroupUsersRepo ChatGroupUsers { get; }
    IAssetsRepo Assets { get; }
    IJobsRepo Jobs { get; }
    IShortlistsRepo Shortlists { get; }
    IInterviewQuestionsRepo InterviewQuestions { get; }
    IOfferApprovalsRepo OfferApprovals { get; }
    IExperiencesRepo Experiences { get; }
    ICandidatesRepo Candidates { get; }
    IScheduleTimingsRepo ScheduleTimings { get; }
    IAptitudeResultsRepo AptitudeResults { get; }
    IJobApplicantsRepo JobApplicants { get; }
    ILogsRepo Logs { get; }
    IEmailsRepo Emails { get; }
    IDocumentFilesRepo DocumentFiles { get; }
    IRevisorsRepo Revisors { get; }
    IAppModulesRepo AppModules { get; }
    IAppSubModulesRepo AppSubModules { get; }
    IAppPagesRepo AppPages { get; }
    ICompanyModulesRepo CompanyModules { get; }
    ICompanyRolesRepo CompanyRoles { get; }
    IRolePermissionsRepo RolePermissions { get; }
}