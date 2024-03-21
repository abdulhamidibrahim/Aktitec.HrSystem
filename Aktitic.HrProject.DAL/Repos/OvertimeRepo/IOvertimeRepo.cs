using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IOvertimeRepo :IGenericRepo<Overtime>
{
    IQueryable<Overtime> GlobalSearch(string? searchKey);

}