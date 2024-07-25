using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class TerminationRepo :GenericRepo<Termination>,ITerminationRepo
{
    private readonly HrSystemDbContext _context;

    public TerminationRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Termination> GlobalSearch(string? searchKey)
    {
        if (_context.Terminations != null)
        {
            var query = _context.Terminations
                .Include(x=>x.Employee)
                .ThenInclude(x=>x.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateOnly.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate ||
                            x.NoticeDate == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Type!.ToLower().Contains(searchKey) ||
                        x.Reason!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Terminations!.AsQueryable();
    }

    public IQueryable<Termination> GetAllWithEmployees()
    {
        
        return _context.Terminations!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department);
    }
    
    public IQueryable<Termination> GetWithEmployees(int id)
    {
        
        return _context.Terminations!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department)
            .Where(x=>x.Id==id);
    }
}
