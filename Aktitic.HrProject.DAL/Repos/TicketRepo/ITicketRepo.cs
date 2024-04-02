using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITicketRepo :IGenericRepo<Ticket>
{
    IQueryable<Ticket> GlobalSearch(string? searchKey);
    //get ticket with employees 
    public Task<Ticket?> GetTicketsWithEmployeesAsync(int id);
    public List<Ticket> GetTicketsWithEmployeesAsync();
}