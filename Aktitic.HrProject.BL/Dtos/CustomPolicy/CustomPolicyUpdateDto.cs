using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class CustomPolicyUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public short? Days { get; set; }
    
    public string? Type { get; set; }
    public int? EmployeeId { get; set; }
    // public EmployeeDto? Employee { get; set; }


}
