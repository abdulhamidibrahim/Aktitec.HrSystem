using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Item : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? Quantity { get; set; }
    public float? UnitCost { get; set; }
    public float? Amount { get; set; }
    public Invoice? Invoice { get; set; }
    public Estimate? Estimate { get; set; }
    public int? InvoiceId { get; set; }
    public int? EstimateId { get; set; }
}