using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

public class EmergencyContact : BaseEntity
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public string? PrimaryName { get; set; }
    public string? PrimaryRelationship { get; set; }
    public string? PrimaryPhone { get; set; }
    public string? PrimaryPhoneTwo { get; set; }
    public string? SecondaryName { get; set; }
    public string? SecondaryRelationship { get; set; }
    public string? SecondaryPhone { get; set; }
    public string? SecondaryPhoneTwo { get; set; }
    
    public ApplicationUser User { get; set; }
}