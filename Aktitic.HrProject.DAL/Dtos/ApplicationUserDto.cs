using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Dtos;

public class ApplicationUserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }
    public EmployeeDto? Employee { get; set; }
    public string Image { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    // public List<PermissionsDto>? Permissions { get; set; } 
}