using Aktitic.HrProject.BL.Dtos.AppModules;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ApplicationUserReadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int? Role { get; set; } 
    public int? CompanyId { get; set; }
    public string Password { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }
    public string Image { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}



public class UserReadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<AppModuleDto> Role { get; set; } 
    public int? CompanyId { get; set; }
    public string Password { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }
    public string Image { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }
    
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

}

