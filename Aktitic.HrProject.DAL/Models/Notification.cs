using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Notification : BaseEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
   
    
    // [ForeignKey(nameof(Company))]
    // public int CompanyId { get; set; }
    public bool IsAll { get; set; }
    public bool IsAdmin { get; set; }
    
    public Priority Priority { get; set; }
    
    public ICollection<ReceivedNotification>? Receivers { get; set; }
    
    // public Company Company { get; set; }
    
}