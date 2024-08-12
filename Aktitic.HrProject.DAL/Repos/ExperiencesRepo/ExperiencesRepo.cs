using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class ExperiencesRepo :GenericRepo<Experience>,IExperiencesRepo
{
    private readonly HrSystemDbContext _context;

    public ExperiencesRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Experience> GlobalSearch(string? searchKey)
    {
        if (_context.Experiences != null)
        {
            var query = _context.Experiences
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
               
                query = query
                    .Where(x =>
                        x.ExperienceLevel!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Experiences!.AsQueryable();
    }
    
}
