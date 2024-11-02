using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class EducationInformation : BaseEntity
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public string? Institution { get; set; }
    public string? Subject { get; set; }
    public DateOnly? StartingDate { get; set; }
    public DateOnly? CompleteDate { get; set; }
    public string? Degree { get; set; }
    public string? Grade { get; set; }
        
    public ApplicationUser User { get; set; }
}