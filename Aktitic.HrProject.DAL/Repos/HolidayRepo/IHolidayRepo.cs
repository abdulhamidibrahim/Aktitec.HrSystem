using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IHolidayRepo :IGenericRepo<Holiday>
{
    IQueryable<Holiday> GlobalSearch(string? searchKey);

}