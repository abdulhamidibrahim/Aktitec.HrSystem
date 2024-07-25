using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class PolicyRepo :GenericRepo<Policies>,IPolicyRepo
{
    private readonly HrSystemDbContext _context;

    public PolicyRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Policies> GlobalSearch(string? searchKey)
    {
        if (_context.Polices != null)
        {
            var query = _context.Polices
                .Include(x=>x.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Polices!.AsQueryable();
    }

    public IQueryable<Policies> GetAllWithDepartments()
    {
        
        return _context.Polices!
            .Include(x=>x.Department);
    }
    
    public IQueryable<Policies> GetWithDepartments(int id)
    {
        
        return _context.Polices!.Include(x=>x.Department)
            .Where(x=>x.Id==id);
    }
}
