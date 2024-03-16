using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL.Dtos.Employee;
public class EmployeeDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }=string.Empty;
    public  string? Email { get; set; }
    public string? ImgUrl { get; set; }
    public int? ImgId { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public byte? Age { get; set; }

    public string? JobPosition { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public byte? YearsOfExperience { get; set; }

    public decimal? Salary { get; set; }

    public string? FileName { get; set; }
    public string? FileContent { get; set; }
    public string? FileExtension { get; set; }
    
    public bool? TeamLeader { get; set; }

    public int? DepartmentId { get; set; }

    public string? Manager { get; set; } 
    
    public int? ProjectId { get; set; }
    public string? Department { get; set; }
}