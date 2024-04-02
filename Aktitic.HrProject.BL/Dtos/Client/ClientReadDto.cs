using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public class ClientReadDto
{
    public int Id { get; set; }

    public string? Email { get; set; } = string.Empty;
    public string? ClientId { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool? Status { get; set; } 
    // public string? FileName { get; set; }=string.Empty;
    // public byte[]? FileContent { get; set; }
    // public string? FileExtension { get; set; }=string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public List<PermissionsDto> Permissions { get; set; } 
}
