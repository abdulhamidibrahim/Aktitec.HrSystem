using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Aktitic.HrProject.DAL.Models;

public abstract class ApplicationUser : IdentityUser<int>
{
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public override int Id { get; set; }
    
}