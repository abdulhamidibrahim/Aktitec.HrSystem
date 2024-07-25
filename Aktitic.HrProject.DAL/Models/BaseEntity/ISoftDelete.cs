namespace Aktitic.HrProject.DAL.Models;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    
    DateTime? DeletedAt { get; set; }
    
}