namespace Aktitic.HrProject.DAL.Pagination.Employee;

public class PagedEmployeeResult
{
    public IEnumerable<EmployeeDto> Employees { get; init; } = new List<EmployeeDto>();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

// Path: Aktitic.HrProject.DAL/Pagination/Employee/PagedEmployeeResult.cs