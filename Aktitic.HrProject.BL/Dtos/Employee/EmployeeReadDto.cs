namespace Aktitic.HrProject.BL;

public class EmployeeReadDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }

    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? ImgUrl { get; set; }

    public string? Phone { get; set; }

    public byte? Age { get; set; }

    public string? JobPosition { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public byte? YearsOfExperience { get; set; }

    public decimal? Salary { get; set; }

    public int? DepartmentId { get; set; }

    public int? ManagerId { get; set; }

    public EmployeeReadDto? Manager { get; set; }
}
