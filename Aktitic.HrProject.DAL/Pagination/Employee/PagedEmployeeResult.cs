namespace Aktitic.HrProject.DAL.Pagination.Employee;

public class PagedEmployeeResult
{
    public IEnumerable<Models.Employee> Employees { get; init; } = new List<Models.Employee>();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}