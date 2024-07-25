using System.ComponentModel.DataAnnotations;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ClientUpdateDto
{
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    
    [Compare(nameof(Password), ErrorMessage = "Password and ConfirmPassword do not match")]
    public string? ConfirmPassword { get; set; }
    public bool? Status { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
    public string? ClientId { get; set; }

    // public string? FileName { get; set; }=string.Empty;
    // public byte[]? FileContent { get; set; }
    // public string? FileExtension { get; set; }=string.Empty;
    public IFormFile? Image { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    public string? ImgUrl { get; set; }
    public string? Permissions { get; set; } 
}
