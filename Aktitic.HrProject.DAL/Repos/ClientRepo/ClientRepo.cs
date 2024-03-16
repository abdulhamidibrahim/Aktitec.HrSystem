using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.ClientRepo;

public class ClientRepo : GenericRepo<Client>, IClientRepo
{
    private readonly HrManagementDbContext _context;

    public ClientRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit)
    {
        if (_context.Clients != null)
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.FirstName != null &&
                        x.LastName != null &&
                        x.Phone != null &&
                        x.Email != null &&
                        x.FullName != null &&
                        x.CompanyName != null &&
                        (x.FullName.ToLower().Contains(term) ||
                         x.Email.ToLower().Contains(term) ||
                         x.Phone.ToLower().Contains(term) ||
                         x.FirstName.ToLower().Contains(term) ||
                         x.LastName.ToLower().Contains(term) )||
                         x.CompanyName.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(sort))
            {
                // var sortOrder = sort.StartsWith("desc") ? "desc" : "asc";
                var sortFields = sort.Split(",");
                StringBuilder orderQueryBuilder = new();
                PropertyInfo[] prop = typeof(Client).GetProperties();
                foreach (var sortField in sortFields)
                {
                    var sortOrder = sortField.StartsWith("-") ? "descending" : "ascending";
                    var sortFieldWithoutOrder = sortField.TrimStart('-');
                    var property = prop.FirstOrDefault(p =>
                        p.Name.Equals(sortFieldWithoutOrder, StringComparison.OrdinalIgnoreCase));
                    if (property == null) continue;
                    orderQueryBuilder.Append($"{property.Name} {sortOrder}, ");
                }

                var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
                query = query.OrderBy(orderQuery);
                
            }

            // pagination
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);
            var employees = query.Skip((page - 1) * limit).Take(limit).ToList();

            return Task.FromResult(new PagedClientResult
            {
                Clients = employees.Select(x => new ClientDto()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName, 
                    Email = x.Email,
                    Phone = x.Phone,
                    CompanyName = x.CompanyName
                }),
                TotalCount = totalCount,
                TotalPages = totalPages
            });
        }

        return null;
    }

    public async Task<Client?> GetByEmail(string email)
    {
        if (_context.Clients != null) return await _context.Clients.FirstOrDefaultAsync(x => x.Email == email);
        return null;
    }
    

    public IQueryable<Client> GlobalSearch(string? searchKey)
    {
        if (_context.Clients != null)
        {
            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x => 
                                x.FullName!.ToLower().Contains(searchKey) ||
                                x.Email!.ToLower().Contains(searchKey) ||
                                x.Phone!.ToLower().Contains(searchKey) ||
                                x.FirstName!.ToLower().Contains(searchKey) ||
                                x.LastName!.ToLower().Contains(searchKey) ||
                                x.CompanyName!.ToLower().Contains(searchKey));
                return query;
            }
           
        }

        return _context.Clients!.AsQueryable();
    }

}

