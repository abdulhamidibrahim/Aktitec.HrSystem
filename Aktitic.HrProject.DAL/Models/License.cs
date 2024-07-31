using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class License : BaseEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool Active { get; set; }
    public Company Company { get; set; } = new();
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
}