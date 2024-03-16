using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public class LeavesRepo :GenericRepo<Leaves>,ILeavesRepo
{
    private readonly HrManagementDbContext _context;

    public LeavesRepo(HrManagementDbContext context) : base(context)
    {
        _context = context;
    }
    
    
    public IQueryable<Leaves> GlobalSearch(string? searchKey)
    {
        if (_context.Leaves != null)
        {
            var query = _context.Leaves.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Type!.ToLower().Contains(searchKey) ||
                        x.Reason!.ToLower().Contains(searchKey) ||
                        x.FromDate!.ToString().ToLower().Contains(searchKey) ||
                        x.ToDate!.ToString().ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Leaves!.AsQueryable();
    }
    
}
