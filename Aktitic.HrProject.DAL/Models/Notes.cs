using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Notes : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? SenderId { get; set; }
    public Employee? Sender { get; set; }
    public int? ReceiverId { get; set; }
    public Employee? Receiver { get; set; }
    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public bool? Starred { get; set; }
        
}