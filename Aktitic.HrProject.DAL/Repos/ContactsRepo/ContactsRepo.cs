using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ContactsRepo :GenericRepo<Contact>,IContactsRepo
{
    private readonly HrSystemDbContext _context;

    public ContactsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Contact>> GetByType(string type)
    {
        if (_context.Contacts != null) return await _context.Contacts
                                                                    .Where(x=>x.Type ==type)
                                                                    .AsQueryable()
                                                                    .ToListAsync();
        return new List<Contact>();
    }
    
    public IQueryable<Contact> GlobalSearch(string? searchKey)
    {
        
        if (_context.Contacts != null)
        {
            var query = _context.Contacts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                //
                // if( DateOnly.TryParse(searchKey, out var searchDate))
                // {
                //     query = query
                //         .Where(x =>
                //             x.Date == searchDate );
                //     return query;
                // }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Name.ToLower().Contains(searchKey) ||
                        x.Email.ToLower().Contains(searchKey) ||
                        x.Number.ToLower().Contains(searchKey) ||
                        x.Role.ToLower().Contains(searchKey) ||
                        x.Type.ToLower().Contains(searchKey) ||
                        x.Status.ToString().Contains(searchKey) );
                
                return query;
            }
               
        }

        return _context.Contacts.AsQueryable();
    }
}
