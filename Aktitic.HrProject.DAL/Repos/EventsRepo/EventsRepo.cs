using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class EventsRepo :GenericRepo<Event>,IEventsRepo
{
    private readonly HrSystemDbContext _context;

    public EventsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Event>> GetByMonth(int month, int year)
    {
        if (_context.Events != null)
            return await _context.Events
                .Where(x => x.StarDate.Month == month && x.StarDate.Year == year)
                .AsQueryable()
                .ToListAsync();
        return new List<Event>();
    }


    public async Task<Event> AddEvent(Event @event)
    {
        if (_context.Events != null)
        {
            var add =await _context.Events.AddAsync(@event);
            // return the current object 
            await _context.SaveChangesAsync();
            return add.Entity;
        }
        
        return new Event();
    }
}
