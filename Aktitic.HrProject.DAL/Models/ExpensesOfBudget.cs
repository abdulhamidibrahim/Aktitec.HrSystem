namespace Aktitic.HrProject.DAL.Models;

public class ExpensesOfBudget : BaseEntity
{ 
    public int Id { get; set; }
    public float? ExpensesAmount { get; set; }
    public string? ExpensesTitle { get; set; }
    
    public int? BudgetId { get; set; }
}