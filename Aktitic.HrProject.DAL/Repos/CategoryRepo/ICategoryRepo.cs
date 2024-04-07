using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ICategoryRepo :IGenericRepo<Category>
{
    IQueryable<Category> GlobalSearch(string? searchKey);
    
}