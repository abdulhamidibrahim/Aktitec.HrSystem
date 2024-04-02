using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class ShiftRepo :GenericRepo<Shift>,IShiftRepo
{
    private readonly HrSystemDbContext _context;

    public ShiftRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Shift> GlobalSearch(string? searchKey)
    {
        if (_context.Shifts != null)
        {
            var query = _context.Shifts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.EndDate == searchDate );
                    return query;
                }else if(TimeOnly.TryParse(searchKey,out var searchDateTime))
                {
                    query = query
                        .Where(x =>
                            x.StartTime == searchDateTime ||
                            x.EndTime == searchDateTime ||
                            x.BreakeTime == searchDateTime);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Tag!.ToLower().Contains(searchKey) ||
                        x.Note.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Shifts!.AsQueryable();
    }
}
