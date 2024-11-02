using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class NotificationSettings 
{
    public int Id { get; set; }
    public bool Active { get; set; }

    [ForeignKey(nameof(Page))] 
    public string PageCode { get; set; }
    public AppPages Page { get; set; }
    
    [ForeignKey(nameof(Models.Company))]
    public int CompanyId { get; set; }
    
    public Company Company { get; set; }
    
}