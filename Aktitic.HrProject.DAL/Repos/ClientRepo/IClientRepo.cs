using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public interface IClientRepo :IGenericRepo<Client>
{
    Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit);
    Task<Client?> GetByEmail(string email);
    
    IQueryable<Client> GlobalSearch(string? column);
   
}