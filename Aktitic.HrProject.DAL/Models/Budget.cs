namespace Aktitic.HrProject.DAL.Models;

public class Budget : BaseEntity
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
   
    public List<ExpensesOfBudget>? Expenses { get; set; }
   
    public List<Revenue>? Revenues { get; set; }
}

