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
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Tag!.ToLower().Contains(searchKey) ||
                        x.Note.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.StartTime!.ToString().ToLower().Contains(searchKey) ||
                        x.EndTime!.ToString().ToLower().Contains(searchKey) ||
                        x.EndDate!.ToString().ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Shifts!.AsQueryable();
    }
}
