using Aktitic.HrProject.BL.Managers;
using Aktitic.HrProject.DAL.Context;

namespace Aktitic.HrProject.DAL.Repos.DatabaseSizeRepo;

public class GetDatabaseSize(HrSystemDbContext unitOfWork)
{
    public async Task<long> GetTotalActiveDataSize()
    {
        var totalSize = 0L;

        totalSize += unitOfWork.ApplicationUsers
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Attendances
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Departments
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Designations
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Employees
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Documents
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.FileUsers
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Holidays
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Leaves
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Overtimes
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Schedulings
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Shifts
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Clients
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.CustomPolicies
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Projects
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Tasks
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TaskLists
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TaskBoards
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Tickets
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TicketFollowers
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Timesheets
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Notes
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.LeaveSettings
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Permissions
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.EmployeeProjects
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Messages
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Estimates
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Items
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Invoices
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Expenses
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Payments
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Taxes
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ProvidentFunds
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Categories
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.BudgetsRevenues
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.BudgetsExpenses
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Budgets
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Revenues
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ExpensesOfBudgets
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Polices
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Salaries
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PayrollOvertimes
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PayrollDeductions
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PayrollAdditions
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PerformanceAppraisals
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PerformanceIndicators
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Terminations
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Resignations
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Promotions
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Trainers
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TrainingTypes
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TrainingLists
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.GoalTypes
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.GoalLists
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Events
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Contacts
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Contracts
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Companies
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Licenses
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Notifications
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ReceivedNotifications
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ChatGroups
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ChatGroupUsers
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Assets
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Jobs
            .Where(e => !e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        return totalSize;
    }
    
    
    public async Task<long> GetTotalNonActiveDataSize()
    {
        var totalSize = 0L;

        totalSize += unitOfWork.ApplicationUsers
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Attendances
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Departments
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Designations
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Employees
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Documents
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.FileUsers
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Holidays
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Leaves
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Overtimes
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Schedulings
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Shifts
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Clients
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.CustomPolicies
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Projects
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Tasks
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TaskLists
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TaskBoards
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Tickets
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TicketFollowers
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Timesheets
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Notes
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.LeaveSettings
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Permissions
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.EmployeeProjects
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Messages
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Estimates
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Items
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Invoices
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Expenses
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Payments
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Taxes
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ProvidentFunds
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Categories
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.BudgetsRevenues
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.BudgetsExpenses
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Budgets
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Revenues
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ExpensesOfBudgets
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Polices
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Salaries
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PayrollOvertimes
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PayrollDeductions
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PayrollAdditions
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PerformanceAppraisals
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.PerformanceIndicators
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Terminations
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Resignations
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Promotions
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Trainers
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TrainingTypes
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.TrainingLists
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.GoalTypes
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.GoalLists
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Events
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Contacts
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Contracts
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Companies
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Licenses
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Notifications
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ReceivedNotifications
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ChatGroups
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.ChatGroupUsers
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Assets
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        totalSize += unitOfWork.Jobs
            .Where(e => e.IsDeleted).AsEnumerable()
            .Sum(e => e.GetEstimatedSize());

        return totalSize;
    }

   public void DeleteNonActiveData()
{
    unitOfWork.ApplicationUsers?.RemoveRange(unitOfWork.ApplicationUsers.Where(e => e.IsDeleted));
    unitOfWork.Attendances?.RemoveRange(unitOfWork.Attendances.Where(e => e.IsDeleted));
    unitOfWork.Departments?.RemoveRange(unitOfWork.Departments.Where(e => e.IsDeleted));
    unitOfWork.Designations?.RemoveRange(unitOfWork.Designations.Where(e => e.IsDeleted));
    unitOfWork.Employees?.RemoveRange(unitOfWork.Employees.Where(e => e.IsDeleted));
    unitOfWork.Documents?.RemoveRange(unitOfWork.Documents.Where(e => e.IsDeleted));
    unitOfWork.FileUsers?.RemoveRange(unitOfWork.FileUsers.Where(e => e.IsDeleted));
    unitOfWork.Holidays?.RemoveRange(unitOfWork.Holidays.Where(e => e.IsDeleted));
    unitOfWork.Leaves?.RemoveRange(unitOfWork.Leaves.Where(e => e.IsDeleted));
    unitOfWork.Overtimes?.RemoveRange(unitOfWork.Overtimes.Where(e => e.IsDeleted));
    unitOfWork.Schedulings?.RemoveRange(unitOfWork.Schedulings.Where(e => e.IsDeleted));
    unitOfWork.Shifts?.RemoveRange(unitOfWork.Shifts.Where(e => e.IsDeleted));
    unitOfWork.Clients?.RemoveRange(unitOfWork.Clients.Where(e => e.IsDeleted));
    unitOfWork.CustomPolicies?.RemoveRange(unitOfWork.CustomPolicies.Where(e => e.IsDeleted));
    unitOfWork.Projects?.RemoveRange(unitOfWork.Projects.Where(e => e.IsDeleted));
    unitOfWork.Tasks?.RemoveRange(unitOfWork.Tasks.Where(e => e.IsDeleted));
    unitOfWork.TaskLists?.RemoveRange(unitOfWork.TaskLists.Where(e => e.IsDeleted));
    unitOfWork.TaskBoards?.RemoveRange(unitOfWork.TaskBoards.Where(e => e.IsDeleted));
    unitOfWork.Tickets?.RemoveRange(unitOfWork.Tickets.Where(e => e.IsDeleted));
    unitOfWork.TicketFollowers?.RemoveRange(unitOfWork.TicketFollowers.Where(e => e.IsDeleted));
    unitOfWork.Timesheets?.RemoveRange(unitOfWork.Timesheets.Where(e => e.IsDeleted));
    unitOfWork.Notes?.RemoveRange(unitOfWork.Notes.Where(e => e.IsDeleted));
    unitOfWork.LeaveSettings?.RemoveRange(unitOfWork.LeaveSettings.Where(e => e.IsDeleted));
    unitOfWork.Permissions?.RemoveRange(unitOfWork.Permissions.Where(e => e.IsDeleted));
    unitOfWork.EmployeeProjects?.RemoveRange(unitOfWork.EmployeeProjects.Where(e => e.IsDeleted));
    unitOfWork.Messages?.RemoveRange(unitOfWork.Messages.Where(e => e.IsDeleted));
    unitOfWork.Estimates?.RemoveRange(unitOfWork.Estimates.Where(e => e.IsDeleted));
    unitOfWork.Items?.RemoveRange(unitOfWork.Items.Where(e => e.IsDeleted));
    unitOfWork.Invoices?.RemoveRange(unitOfWork.Invoices.Where(e => e.IsDeleted));
    unitOfWork.Expenses?.RemoveRange(unitOfWork.Expenses.Where(e => e.IsDeleted));
    unitOfWork.Payments?.RemoveRange(unitOfWork.Payments.Where(e => e.IsDeleted));
    unitOfWork.Taxes?.RemoveRange(unitOfWork.Taxes.Where(e => e.IsDeleted));
    unitOfWork.ProvidentFunds?.RemoveRange(unitOfWork.ProvidentFunds.Where(e => e.IsDeleted));
    unitOfWork.Categories?.RemoveRange(unitOfWork.Categories.Where(e => e.IsDeleted));
    unitOfWork.BudgetsRevenues?.RemoveRange(unitOfWork.BudgetsRevenues.Where(e => e.IsDeleted));
    unitOfWork.BudgetsExpenses?.RemoveRange(unitOfWork.BudgetsExpenses.Where(e => e.IsDeleted));
    unitOfWork.Budgets?.RemoveRange(unitOfWork.Budgets.Where(e => e.IsDeleted));
    unitOfWork.Revenues?.RemoveRange(unitOfWork.Revenues.Where(e => e.IsDeleted));
    unitOfWork.ExpensesOfBudgets?.RemoveRange(unitOfWork.ExpensesOfBudgets.Where(e => e.IsDeleted));
    unitOfWork.Polices?.RemoveRange(unitOfWork.Polices.Where(e => e.IsDeleted));
    unitOfWork.Salaries?.RemoveRange(unitOfWork.Salaries.Where(e => e.IsDeleted));
    unitOfWork.PayrollOvertimes?.RemoveRange(unitOfWork.PayrollOvertimes.Where(e => e.IsDeleted));
    unitOfWork.PayrollDeductions?.RemoveRange(unitOfWork.PayrollDeductions.Where(e => e.IsDeleted));
    unitOfWork.PayrollAdditions?.RemoveRange(unitOfWork.PayrollAdditions.Where(e => e.IsDeleted));
    unitOfWork.PerformanceAppraisals?.RemoveRange(unitOfWork.PerformanceAppraisals.Where(e => e.IsDeleted));
    unitOfWork.PerformanceIndicators?.RemoveRange(unitOfWork.PerformanceIndicators.Where(e => e.IsDeleted));
    unitOfWork.Terminations?.RemoveRange(unitOfWork.Terminations.Where(e => e.IsDeleted));
    unitOfWork.Resignations?.RemoveRange(unitOfWork.Resignations.Where(e => e.IsDeleted));
    unitOfWork.Promotions?.RemoveRange(unitOfWork.Promotions.Where(e => e.IsDeleted));
    unitOfWork.Trainers?.RemoveRange(unitOfWork.Trainers.Where(e => e.IsDeleted));
    unitOfWork.TrainingTypes?.RemoveRange(unitOfWork.TrainingTypes.Where(e => e.IsDeleted));
    unitOfWork.TrainingLists?.RemoveRange(unitOfWork.TrainingLists.Where(e => e.IsDeleted));
    unitOfWork.GoalTypes?.RemoveRange(unitOfWork.GoalTypes.Where(e => e.IsDeleted));
    unitOfWork.GoalLists?.RemoveRange(unitOfWork.GoalLists.Where(e => e.IsDeleted));
    unitOfWork.Events?.RemoveRange(unitOfWork.Events.Where(e => e.IsDeleted));
    unitOfWork.Contacts?.RemoveRange(unitOfWork.Contacts.Where(e => e.IsDeleted));
    unitOfWork.Contracts?.RemoveRange(unitOfWork.Contracts.Where(e => e.IsDeleted));
    unitOfWork.Companies?.RemoveRange(unitOfWork.Companies.Where(e => e.IsDeleted));
    unitOfWork.Licenses?.RemoveRange(unitOfWork.Licenses.Where(e => e.IsDeleted));
    unitOfWork.Notifications?.RemoveRange(unitOfWork.Notifications.Where(e => e.IsDeleted));
    unitOfWork.ReceivedNotifications?.RemoveRange(unitOfWork.ReceivedNotifications.Where(e => e.IsDeleted));
    unitOfWork.ChatGroups?.RemoveRange(unitOfWork.ChatGroups.Where(e => e.IsDeleted));
    unitOfWork.ChatGroupUsers?.RemoveRange(unitOfWork.ChatGroupUsers.Where(e => e.IsDeleted));
    unitOfWork.Assets?.RemoveRange(unitOfWork.Assets.Where(e => e.IsDeleted));
    unitOfWork.Jobs?.RemoveRange(unitOfWork.Jobs.Where(e => e.IsDeleted));
    unitOfWork.Shortlists?.RemoveRange(unitOfWork.Shortlists.Where(e => e.IsDeleted));
    unitOfWork.InterviewQuestions?.RemoveRange(unitOfWork.InterviewQuestions.Where(e => e.IsDeleted));
    unitOfWork.OfferApprovals?.RemoveRange(unitOfWork.OfferApprovals.Where(e => e.IsDeleted));
    unitOfWork.Experiences?.RemoveRange(unitOfWork.Experiences.Where(e => e.IsDeleted));
    unitOfWork.Candidates?.RemoveRange(unitOfWork.Candidates.Where(e => e.IsDeleted));
    unitOfWork.ScheduleTimings?.RemoveRange(unitOfWork.ScheduleTimings.Where(e => e.IsDeleted));
    unitOfWork.AptitudeResults?.RemoveRange(unitOfWork.AptitudeResults.Where(e => e.IsDeleted));
    unitOfWork.JobApplicants?.RemoveRange(unitOfWork.JobApplicants.Where(e => e.IsDeleted));

    // Make sure to save changes to persist the deletions
    unitOfWork.SaveChanges();
}
}