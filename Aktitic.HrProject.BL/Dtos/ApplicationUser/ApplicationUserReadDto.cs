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
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }
    public string Image { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<PermissionsDto>? Permissions { get; set; } 
}

