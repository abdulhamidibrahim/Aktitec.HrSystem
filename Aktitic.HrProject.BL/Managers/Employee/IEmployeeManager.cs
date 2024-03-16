using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public interface IEmployeeManager
{
    public Task<int> Add(EmployeeAddDto employeeAddDto, IFormFile? image);
    public Task<int> Update(EmployeeUpdateDto employeeUpdateDto,int id, IFormFile? image);
    public Task<int> Delete(int id);
    public EmployeeReadDto? Get(int id);
    public Task<List<EmployeeReadDto>> GetAll();
    public Task<PagedEmployeeResult> GetEmployeesAsync(string? term, string? sort, int page, int limit);
    public Task<FilteredEmployeeDto> GetFilteredEmployeesAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<EmployeeDto>> GlobalSearch(string searchKey,string? column);
    bool IsEmailUnique(string email);
    public Task<List<ManagerTree>> GetManagersTreeAsync();
}