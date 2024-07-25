using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class ResignationRepo :GenericRepo<Resignation>,IResignationRepo
{
    private readonly HrSystemDbContext _context;

    public ResignationRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Resignation> GlobalSearch(string? searchKey)
    {
        if (_context.Resignations != null)
        {
            var query = _context.Resignations
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
                            x.ResignationDate == searchDate ||
                            x.NoticeDate == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.Reason!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Resignations!.AsQueryable();
    }

    public IQueryable<Resignation> GetAllWithEmployees()
    {
        
        return _context.Resignations!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department);
    }
    
    public IQueryable<Resignation> GetWithEmployees(int id)
    {
        
        return _context.Resignations!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department)
            .Where(x=>x.Id==id);
    }
}
