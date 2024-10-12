namespace Aktitic.HrProject.DAL.Models;

public enum Pages
{
    // Main Module
    AdminDashboard,
    EmployeeDashboard,
    DocumentsDetailsView,
    DocumentsManager,
    DocumentsWorkflows,
    Chat,
    Calendar,
    Contacts,
    Email,

    // Employees Module
    AllEmployees,
    Contracts,
    Holidays,
    LeavesAdmin,
    LeavesEmployee,
    LeaveSettings,
    AttendanceAdmin,
    AttendanceEmployee,
    Departments,
    Designations,
    ShiftSchedule,
    Overtime,

    // Clients Module
    Clients,

    // Projects Module
    Projects,
    Tasks,
    TaskBoard,

    // Leads Module
    Leads,

    // Tickets Module
    Tickets,

    // HR Module - Sales SubModule
    Estimate,
    Invoices,
    Payments,
    Expenses,
    ProvidentFund,
    Taxes,

    // HR Module - Accounting SubModule
    Categories,
    Budgets,
    BudgetsExpenses,
    BudgetsRevenues,

    // HR Module - Payroll SubModule
    EmployeeSalary,
    PayrollItems,

    // HR Module - Policies SubModule
    Policies,

    // HR Module - Reports SubModule
    ExpenseReport,
    InvoiceReport,
    PaymentsReport,
    ProjectReport,
    TaskReport,
    UserReport,
    EmployeeReport,
    PayslipReport,
    AttendanceReport,
    LeaveReport,
    DailyReport,

    // Performance Module
    PerformanceIndicator,
    PerformanceReview,
    PerformanceAppraisal,

    // Goals SubModule
    GoalList,
    GoalType,

    // Training SubModule
    TrainingList,
    Trainers,
    TrainingType,

    // Promotion SubModule
    Promotion,

    // Resignation SubModule
    Resignation,

    // Termination SubModule
    Termination,

    // Administration Module - Assets SubModule
    Assets

}