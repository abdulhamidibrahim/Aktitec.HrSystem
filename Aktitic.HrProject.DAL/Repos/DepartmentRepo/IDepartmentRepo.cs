using Aktitic.HrProject.DAL.Models;


namespace Aktitic.HrProject.DAL.Repos;

public interface IDepartmentRepo :IGenericRepo<Department>
{
    IQueryable<Department> GlobalSearch(string? searchKey);

}