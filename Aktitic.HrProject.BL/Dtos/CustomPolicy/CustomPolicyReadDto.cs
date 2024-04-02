using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL;

public class CustomPolicyReadDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public short? Days { get; set; }
    public string? Type { get; set; }
    public int? EmployeeId { get; set; }
    public string? Employee { get; set; }

}
