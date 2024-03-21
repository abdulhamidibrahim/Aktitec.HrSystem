using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class HolidayRepo :GenericRepo<Holiday>,IHolidayRepo
{
    private readonly HrSystemDbContext _context;

    public HolidayRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Holiday> GlobalSearch(string? searchKey)
    {
        if (_context.Holidays != null)
        {
            var query = _context.Holidays.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Title!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Holidays!.AsQueryable();
    }
    
}
