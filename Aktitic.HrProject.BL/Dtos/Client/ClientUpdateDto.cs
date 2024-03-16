namespace Aktitic.HrProject.BL;

public class ClientUpdateDto
{
    public string? Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;


    // public string? FileName { get; set; }=string.Empty;
    // public byte[]? FileContent { get; set; }
    // public string? FileExtension { get; set; }=string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = string.Empty;
    public string? ImgUrl { get; set; }
}
