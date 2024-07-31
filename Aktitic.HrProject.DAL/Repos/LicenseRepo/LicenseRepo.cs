using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class LicenseRepo :GenericRepo<License>,ILicenseRepo
{
    private readonly HrSystemDbContext _context;

    public LicenseRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<License> GlobalSearch(string? searchKey)
    {
        if (_context.Licenses != null)
        {
            var query = _context.Licenses
                .Include(x=>x.Company)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.StartDate == searchDate ||
                            x.EndDate == searchDate );
                    return query;
                }
                
                
                if(query.Any(x => x.Company.CompanyName != null && x.Company.CompanyName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Company.CompanyName.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.Price!.ToString().Contains(searchKey) ||
                        x.Active!.ToString().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Licenses!.AsQueryable();
    }

    public async Task<List<License>> GetAllLicenses()
    {
        if (_context.Licenses != null)
            return await _context.Licenses
                .Include(x => x.Company)
                .AsQueryable()
                .ToListAsync();
        return new List<License>();
    }
}
