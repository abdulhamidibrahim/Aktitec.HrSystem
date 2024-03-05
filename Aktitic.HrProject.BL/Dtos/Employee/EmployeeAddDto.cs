using System.ComponentModel.DataAnnotations;
using FileUploadingWebAPI.Filter;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class EmployeeAddDto
{
    public string? FullName { get; set; }
    // [Required]
    // public string Password { get; set; }
   
    // [Required]
    // [EmployeeEmailAddressValidator(ErrorMessage = "Email already exists!")]
    [EmailAddress( ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    public string? Gender { get; set; }
    public IFormFile? Image { get; set; }=null!;
    public string? Phone { get; set; }
    // public string? ImgUrl { get; set; }

    public byte? Age { get; set; }

    public string? JobPosition { get; set; }
    
    public DateOnly? JoiningDate { get; set; }
    
    public byte? YearsOfExperience { get; set; }

    public decimal? Salary { get; set; }
    
    public int? DepartmentId { get; set; }
    
    public int? ManagerId { get; set; }


}
