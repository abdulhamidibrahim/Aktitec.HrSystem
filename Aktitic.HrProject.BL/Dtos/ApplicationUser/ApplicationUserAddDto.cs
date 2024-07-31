using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ApplicationUserAddDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
    
    [PasswordPropertyText, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    // eleven character starts with 0
        
    [Required, RegularExpression("^\\+?\\d{11}$")]
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public int? EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public IFormFile? Image { get; set; }
    public string Permissions { get; set; } = string.Empty;
}
