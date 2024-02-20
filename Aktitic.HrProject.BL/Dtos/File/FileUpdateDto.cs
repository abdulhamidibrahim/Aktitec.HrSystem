namespace Aktitic.HrProject.BL;

public class FileUpdateDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }
    public string? Name { get; set; }

    public string? Extension { get; set; }

    public byte[]? Content { get; set; }

}
