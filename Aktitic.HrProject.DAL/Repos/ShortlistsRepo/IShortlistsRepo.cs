using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IShortlistsRepo :IGenericRepo<Shortlist>
{
    IQueryable<Shortlist> GlobalSearch(string? searchKey);
    Task<IEnumerable<Shortlist>> GetAllShortlists();
}