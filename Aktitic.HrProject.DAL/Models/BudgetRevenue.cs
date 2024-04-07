namespace Aktitic.HrProject.DAL.Models;

public class BudgetRevenue
{
    public int Id { get; set; }
    public double? Amount { get; set; }
    public string? Currency { get; set; }
    public string? Note { get; set; }
    public DateOnly? Date { get; set; }
    public int? CategoryId { get; set; }
    public string? Subcategory { get; set; }
        
    public virtual Category? Category { get; set; }
}

// {
// "categoryName": "Material",
// "subcategoryName": "Material Expenses"
// }