using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL;

public class PagedEmployeeResult
{
    public IEnumerable<Employee> Employees { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}