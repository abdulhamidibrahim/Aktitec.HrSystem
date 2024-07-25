using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IEventsRepo :IGenericRepo<Event>
{
    Task<IEnumerable<Event>> GetByMonth(int month, int year);
    public Task<Event> AddEvent(Event @event);
}