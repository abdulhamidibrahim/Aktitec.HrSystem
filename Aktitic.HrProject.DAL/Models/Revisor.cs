using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Revisor : BaseEntity
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }
    
    [ForeignKey(nameof(Document))]
    public int DocumentId { get; set; }
    
    public string? Notes { get; set; }
    
    public bool IsReviewed { get; set; }
    
    public DateTime? RevisionDate { get; set; }
    
    //Relations
    public Employee Employee { get; set; }
    public Document Document { get; set; }
    public string? DigitalSignature { get; set; }
}