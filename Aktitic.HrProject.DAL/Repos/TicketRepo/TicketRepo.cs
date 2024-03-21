using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class TicketRepo :GenericRepo<Ticket>,ITicketRepo
{
    private readonly HrSystemDbContext _context;

    public TicketRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public IQueryable<Ticket> GlobalSearch(string? searchKey)
    {
        if (_context.Tickets != null)
        {
            var query = _context.Tickets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.Priority!.ToLower().Contains(searchKey) ||
                        x.Subject!.ToLower().Contains(searchKey) ||
                        x.Cc!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Tickets!.AsQueryable();
    }
}
