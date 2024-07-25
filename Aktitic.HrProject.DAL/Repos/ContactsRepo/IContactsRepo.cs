using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IContactsRepo :IGenericRepo<Contact>
{
    Task<IEnumerable<Contact>> GetByType(string type);
    IQueryable<Contact> GlobalSearch(string? searchKey);

    
}