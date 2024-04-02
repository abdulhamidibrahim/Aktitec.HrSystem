using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ClientAddDto
{
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? ConfirmPassword { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ClientId { get; set; }
    public IFormFile? Image { get; set; }
    
    public string Mobile { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public bool? Status { get; set; } = false;
    public string? Permissions { get; set; }
    
}
