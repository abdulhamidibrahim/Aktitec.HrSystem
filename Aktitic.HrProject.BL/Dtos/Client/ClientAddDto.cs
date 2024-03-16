using Aktitic.HrProject.BL.Dtos.Employee;

namespace Aktitic.HrProject.BL;

public class ClientAddDto
{

    public string? Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;


    // public string? FileName { get; set; }
    // public byte[]? FileContent { get; set; }
    // public string? FileExtension { get; set; }=string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;

}
