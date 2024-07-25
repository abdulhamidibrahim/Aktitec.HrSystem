namespace Aktitic.HrProject.DAL.Pagination.Client;

public class BudgetExpensesSearchDto
{
    public int Id { get; set; }
    public double? Amount { get; set; }
    public string? Currency { get; set; }
    public string? Note { get; set; }
    public DateOnly ?Date { get; set; }
    public int? CategoryId { get; set; }
    public string? Category { get; set; }
    public string? Subcategory { get; set; }
}