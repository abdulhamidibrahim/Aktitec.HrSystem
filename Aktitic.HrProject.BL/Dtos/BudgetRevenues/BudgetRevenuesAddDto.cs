using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class BudgetRevenuesAddDto
{
    public double? Amount { get; set; }
    public string? Currency { get; set; }
    public string? Note { get; set; }
    public DateOnly? Date { get; set; }
    public int? CategoryId { get; set; }
    public string? Subcategory { get; set; }
        
}
