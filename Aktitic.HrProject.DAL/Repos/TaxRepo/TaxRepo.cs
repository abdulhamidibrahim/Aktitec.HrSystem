using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TaxRepo :GenericRepo<Tax>,ITaxRepo
{
    private readonly HrSystemDbContext _context;

    public TaxRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Tax> GlobalSearch(string? searchKey)
    {
        if (_context.Taxes != null)
        {
            var query = _context.Taxes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                //
                // if( DateOnly.TryParse(searchKey, out var searchDate))
                // {
                //     query = query
                //         .Where(x =>
                //             x.Date == searchDate );
                //     return query;
                // }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Percentage!.ToString().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Taxes!.AsQueryable();
    }
}
