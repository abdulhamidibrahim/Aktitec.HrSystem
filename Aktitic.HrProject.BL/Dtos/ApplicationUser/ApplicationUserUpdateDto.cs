using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class ApplicationUserUpdateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
    
    [PasswordPropertyText,Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Role { get; set; } 
    public int? CompanyId { get; set; } 
    public int? EmployeeId { get; set; }
    public IFormFile? Image { get; set; } 
    public DateTime Date { get; set; }
    
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PinCode { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Address { get; set; } 
    public bool? Gender { get; set; }
    public string? PassportNumber { get; set; } 
    public DateTime? PassportExpDate { get; set; }
    public string? Tel { get; set; } 
    public string? Nationality { get; set; } 
    public string? Religion { get; set; } 
    public string? MatritalStatus { get; set; } 
    public string? EmploymentSpouse { get; set; } 
    public int? ChildrenNumber { get; set; }
    public int? ReportsTo { get; set; }
    public int? ClientId { get; set; }
}
