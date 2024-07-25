using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class PromotionRepo :GenericRepo<Promotion>,IPromotionRepo
{
    private readonly HrSystemDbContext _context;

    public PromotionRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Promotion> GlobalSearch(string? searchKey)
    {
        if (_context.Promotions != null)
        {
            var query = _context.Promotions
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
                            x.Date == searchDate );
                    return query;
                }
                query = query
                    .Where(x =>
                        x.PromotionFrom!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Promotions!.AsQueryable();
    }

    public IQueryable<Promotion> GetAllWithEmployees()
    {
        
        return _context.Promotions!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department)
            .Include(x=>x.PromotionTo);
    }
    
    public IQueryable<Promotion> GetWithEmployees(int id)
    {
        
        return _context.Promotions!
            .Include(x=>x.Employee)
            .ThenInclude(x=>x.Department)
            .Include(x=>x.PromotionTo)
            .Where(x=>x.Id==id);
    }
}
