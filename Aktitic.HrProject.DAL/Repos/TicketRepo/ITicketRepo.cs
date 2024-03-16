using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITicketRepo :IGenericRepo<Ticket>
{
    IQueryable<Ticket> GlobalSearch(string? searchKey);
}