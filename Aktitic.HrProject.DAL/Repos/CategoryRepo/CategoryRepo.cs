
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class CategoryRepo :GenericRepo<Category>,ICategoryRepo
{
    private readonly HrSystemDbContext _context;

    public CategoryRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Category> GlobalSearch(string? searchKey)
    {
        if (_context.Categories!= null)
        {
            var query = 
                _context.Categories
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.CategoryName!.ToLower().Contains(searchKey) ||
                        x.SubcategoryName!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Categories!.AsQueryable();
    }

}
