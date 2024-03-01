using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IEmployeeRepo :IGenericRepo<Employee>
{
    Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit);
    // Task<Employee> GetFilteredEmployees(Filter filter);
}