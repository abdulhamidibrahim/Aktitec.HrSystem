using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public interface IEmployeeManager
{
    public void Add(EmployeeAddDto employeeAddDto);
    public void Update(EmployeeUpdateDto employeeUpdateDto);
    public void Delete(int id);
    public EmployeeReadDto? Get(int id);
    public Task<List<EmployeeReadDto>> GetAll();
    public Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string? term, string? sort, int page, int limit);
    public IEnumerable<EmployeeDto> GetFilteredEmployeesAsync(string column, string value1,[Optional] string? value2,
        string @operator);
}