using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ILicenseRepo :IGenericRepo<License>
{
    IQueryable<License> GlobalSearch(string? searchKey);
    public Task<List<License>> GetAllLicenses();
}