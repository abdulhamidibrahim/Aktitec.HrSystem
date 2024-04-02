namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PermissionsDto
{
    public string? Permission { get; set; }
    public bool? Read { get; set; } = false;
    public bool? Write { get; set; } = false;
    public bool? Delete { get; set; } = false;
    public bool? Create { get; set; } = false;
    public bool? Import { get; set; } = false;
    public bool? Export { get; set; } = false;
}