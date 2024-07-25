
namespace Aktitic.HrProject.DAL.Models;

public class Promotion : BaseEntity
{
    public int Id { get; set; }
    public int? EmployeeId { get; set; }
    public string? PromotionFrom { get; set; }
    public int? PromotionToId { get; set; }
    public Designation? PromotionTo { get; set; }
    public DateOnly? Date { get; set; }
    public Employee? Employee { get; set; }
}
