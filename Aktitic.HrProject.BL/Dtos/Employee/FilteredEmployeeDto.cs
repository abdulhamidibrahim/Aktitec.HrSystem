using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.BL.Dtos.Employee;

public class FilteredEmployeeDto
{
    public IEnumerable<EmployeeDto> EmployeeDto { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}