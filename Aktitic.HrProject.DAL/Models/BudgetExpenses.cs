namespace Aktitic.HrProject.DAL.Models;

public class BudgetExpenses
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public string? Currency { get; set; }
    public string? Note { get; set; }
    public DateOnly ?Date { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? Subcategory { get; set; }
    
}
