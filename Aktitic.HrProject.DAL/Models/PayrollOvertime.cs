namespace Aktitic.HrProject.DAL.Models;

public class PayrollOvertime : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? RateType { get; set; }
    public decimal? Rate { get; set; }
}
