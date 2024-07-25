using System.ComponentModel.DataAnnotations.Schema;

namespace Aktitic.HrProject.DAL.Models;

[Table("Permissions", Schema = "project")]
public class Permission : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool? Read { get; set; }
    public bool? Write { get; set; }
    public bool? Delete { get; set; }
    public bool? Create { get; set; }
    public bool? Import { get; set; }
    public bool? Export { get; set; }
    public int? ClientId { get; set; }
    public Client? Client { get; set; }
    
    [ForeignKey(nameof(ApplicationUser))]
    public int? UserId { get; set; }
    
    public ApplicationUser? ApplicationUser { get; set; }
}