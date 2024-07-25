
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrProject.DAL.Pagination.Client;

public class BudgetDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Type { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public float? OverallExpense { get; set; }
    public float? OverallRevenue { get; set; }
    public float? ExpectedProfit { get; set; }
    public float? Tax { get; set; }
    public float? BudgetAmount { get; set; }
    public ICollection<ExpensesBudget>? Expenses { get; set; }
    public ICollection<RevenuesBudget>? Revenues { get; set; }
}