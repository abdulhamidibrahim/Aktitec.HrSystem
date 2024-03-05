using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aktitic.HrProject.DAL.Models;

public class ApplicationUser : IdentityUser<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int Id { get; set; }
    public Employee? Employee { get; set; }
    public int? EmployeeId { get; set; }
    public Client? Client { get; set; }
    public int? ClientId { get; set; }
}