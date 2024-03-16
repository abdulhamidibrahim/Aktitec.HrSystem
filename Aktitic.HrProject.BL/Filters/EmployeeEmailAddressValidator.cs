using System.ComponentModel.DataAnnotations;
using Aktitic.HrProject.BL;

namespace FileUploadingWebAPI.Filter;

public class EmployeeEmailAddressValidator : ValidationAttribute
{
    private readonly IEmployeeManager _employeeManager;
    
    public override bool IsValid(object? value)
    {
        if (value is string email)
        {
            return _employeeManager.IsEmailUnique(email);
        }
        return false;
    }
    

    public EmployeeEmailAddressValidator(IEmployeeManager employeeManager)
    {
        _employeeManager = employeeManager;
    }

    public EmployeeEmailAddressValidator()
    { }
}