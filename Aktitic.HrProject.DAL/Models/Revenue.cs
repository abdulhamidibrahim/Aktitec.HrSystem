namespace Aktitic.HrProject.DAL.Models;

public class Revenue : BaseEntity
{
    public int Id { get; set; }
    public string? RevenueTitle { get; set; }   
    public float? RevenueAmount { get; set; }
    public int BudgetId { get; set; }
}