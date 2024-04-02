using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Task = System.Threading.Tasks.Task;

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

    public async Task<Ticket?> GetTicketsWithEmployeesAsync(int id)
    {
        if (_context.Tickets != null)
        {
            return await _context.Tickets
                .Include(x => x.AssignedTo)
                .Include(x =>x.CreatedBy)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        return new Ticket();
    }

    public List<Ticket> GetTicketsWithEmployeesAsync()
    {
        
        if (_context.Tickets != null)
        {
            return _context.Tickets
                .Include(x => x.AssignedTo)
                .Include(x => x.CreatedBy).ToList();
        }

        return new List<Ticket>();
    }
}
