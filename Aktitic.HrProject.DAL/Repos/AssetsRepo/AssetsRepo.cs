using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class AssetsRepo :GenericRepo<Asset>,IAssetsRepo
{
    private readonly HrSystemDbContext _context;

    public AssetsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Asset> GlobalSearch(string? searchKey)
    {
        if (_context.Assets != null)
        {
            var query = _context.Assets
                .Include(x=>x.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.PurchaseFrom == searchDate ||
                            x.PurchaseTo == searchDate );
                    return query;
                }
                
                
                if(query.Any(x => x.User.FullName != null && x.User.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.User.FullName.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.Manufacturer!.ToLower().Contains(searchKey) ||
                        x.Model!.ToLower().Contains(searchKey) ||
                        x.SerialNumber!.ToLower().Contains(searchKey) ||
                        x.Supplier!.ToLower().Contains(searchKey) ||
                        x.Condition!.ToLower().Contains(searchKey) ||
                        x.Value!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Assets!.AsQueryable();
    }

    public async Task<IEnumerable<Asset>> GetAllAssets()
    {
        return await _context.Assets!.Include(x=>x.User).ToListAsync();
    }

    public Task<Asset> GetAssetById(int id)
    {
        return _context.Assets!
            .Include(x=>x.User)
            .FirstOrDefaultAsync(x=>x.Id == id);
    }
}
