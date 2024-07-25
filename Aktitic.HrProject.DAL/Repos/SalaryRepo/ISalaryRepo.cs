using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ISalaryRepo :IGenericRepo<Salary>
{
    IQueryable<Salary> GlobalSearch(string? searchKey);
    
    IQueryable<Salary> GetWithEmployee(int id);
    Task<IQueryable<Salary>> GetAllWithEmployee();
}