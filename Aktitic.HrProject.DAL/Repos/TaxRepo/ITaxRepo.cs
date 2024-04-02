using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITaxRepo :IGenericRepo<Tax>
{
    IQueryable<Tax> GlobalSearch(string? searchKey);
    
}