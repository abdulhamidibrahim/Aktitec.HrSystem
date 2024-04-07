using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class BudgetUpdateDto
{
    public string? Title { get; set; }
    public string? Type { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public float? OverallExpense { get; set; }
    public float? OverallRevenue { get; set; }
    public float? ExpectedProfit { get; set; }
    public float? Tax { get; set; }
    public float? BudgetAmount { get; set; }
    public int? ExpensesId { get; set; }
    public int? RevenueId { get; set; }
    public List<ExpensesCreateDto>? Expenses { get; set; }
    public List<RevenuesCreateDto>? Revenue { get; set; }
}
