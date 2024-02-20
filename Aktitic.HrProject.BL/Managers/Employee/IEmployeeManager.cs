using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public interface IEmployeeManager
{
    public void Add(EmployeeAddDto employeeAddDto);
    public void Update(EmployeeUpdateDto employeeUpdateDto);
    public void Delete(EmployeeDeleteDto employeeDeleteDto);
    public EmployeeReadDto? Get(int id);
    public Task<List<EmployeeReadDto>> GetAll();
}