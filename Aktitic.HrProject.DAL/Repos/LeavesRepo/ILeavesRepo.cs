using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ILeavesRepo :IGenericRepo<Leaves>
{
    IQueryable<Leaves> GlobalSearch(string? searchKey);
    List<Leaves> GetLeavesWithEmployee();
}