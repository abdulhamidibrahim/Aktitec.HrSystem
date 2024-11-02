using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class ProfileExperience : BaseEntity
{
    public int Id { get; set; }
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public string? CompanyName { get; set; }
    public string? Location { get; set; }
    public string? JobPosition { get; set; }
    public DateOnly? PeriodFrom { get; set; }
    public DateOnly? PeriodTo { get; set; }
    
    public ApplicationUser User { get; set; }
}