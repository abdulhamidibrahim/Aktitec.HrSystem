using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

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
                query = query
                    .Where(x =>
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.Employee!.FullName!.ToLower().Contains(searchKey) ||
                        x.OtDate.ToString()!.ToLower().Contains(searchKey));

                return query;
            }
        }
        return _context.Overtimes!.AsQueryable();
    }
}
