using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IAssetsRepo :IGenericRepo<Asset>
{
    IQueryable<Asset> GlobalSearch(string? searchKey);
    Task<IEnumerable<Asset>>  GetAllAssets();
    Task<Asset> GetAssetById(int id);
}