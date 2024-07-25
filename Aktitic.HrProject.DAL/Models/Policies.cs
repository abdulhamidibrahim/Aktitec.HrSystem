namespace Aktitic.HrProject.DAL.Models;

public class Policies : BaseEntity
{
    public int Id  { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public DateOnly? Date { get; set; }

    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }
    
    // public int? FileId { get; set; }
    // public File? File { get; set; }
    
}

        