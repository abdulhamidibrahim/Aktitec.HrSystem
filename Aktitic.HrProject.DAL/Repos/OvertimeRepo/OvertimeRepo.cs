using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class OvertimeRepo :GenericRepo<Overtime>,IOvertimeRepo
{
    private readonly HrSystemDbContext _context;

    public OvertimeRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Overtime> GlobalSearch(string? searchKey)
    {
        if (_context.Overtimes != null)
        {
            var query = _context.Overtimes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.OtDate == searchDate);
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.Employee!.FullName!.ToLower().Contains(searchKey));

                return query;
            }
        }
        return _context.Overtimes!.AsQueryable();
    }

    public Task<IEnumerable<Overtime>> GetOvertimesWithEmployeeAndApprovedBy()
    {
        return Task.FromResult(_context.Overtimes!
            .Include(x => x.Employee)
            .Include(x => x.ApprovedByNavigation)
            .AsEnumerable());
    }

    public Task<Overtime> GetOvertimesWithEmployeeAndApprovedBy(int id)
    {
        return _context.Overtimes
            .Include(x => x.Employee)
            .Include(x => x.ApprovedByNavigation)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
