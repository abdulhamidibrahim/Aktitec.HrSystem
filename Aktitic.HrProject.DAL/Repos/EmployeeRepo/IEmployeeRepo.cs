using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;

namespace Aktitic.HrProject.DAL.Repos.EmployeeRepo;

public interface IEmployeeRepo :IGenericRepo<Employee>
{
    Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit);
    Task<Employee?> GetByEmail(string email);
    
    Task<Employee?>? GetByManager(int managerId);
    Task<List<Employee>> GetAllManagersAsync();
    
    IQueryable<Employee> GlobalSearch(string? column);
    public Task<List<Employee>> GetSubordinatesAsync(int employeeId);

    public Task<List<Employee>> GetEmployeeWithDepartment();

}