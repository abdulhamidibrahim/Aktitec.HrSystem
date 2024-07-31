using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class Notification 
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string DeletedBy { get; set; }
    public DateTime DeletedAt { get; set; }
    
    public string UpdatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public bool IsDeleted { get; set; }
    
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public bool IsAll { get; set; }
    
    public Priority Priority { get; set; }
    
    public ICollection<ReceivedNotification>? Receivers { get; set; }
    
    public Company Company { get; set; }
    
}