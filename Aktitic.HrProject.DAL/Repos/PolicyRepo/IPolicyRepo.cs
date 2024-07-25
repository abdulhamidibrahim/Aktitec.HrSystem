using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IPolicyRepo :IGenericRepo<Policies>
{
    IQueryable<Policies> GlobalSearch(string? searchKey);
    
    IQueryable<Policies> GetAllWithDepartments();
    IQueryable<Policies> GetWithDepartments(int id);
}