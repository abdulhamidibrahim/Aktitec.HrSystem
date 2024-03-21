using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IShiftRepo :IGenericRepo<Shift>
{
    IQueryable<Shift> GlobalSearch(string? searchKey);

}