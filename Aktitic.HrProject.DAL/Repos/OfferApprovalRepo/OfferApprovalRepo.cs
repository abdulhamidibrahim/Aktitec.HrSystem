using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class OfferApprovalsRepo :GenericRepo<OfferApproval>,IOfferApprovalsRepo
{
    private readonly HrSystemDbContext _context;

    public OfferApprovalsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<OfferApproval> GlobalSearch(string? searchKey)
    {
        if (_context.OfferApprovals != null)
        {
            var query = _context.OfferApprovals
                .Include(x=>x.Employee)
                .Include(x=>x.Job)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
               
                
                if(query.Any(x => x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Employee.FullName != null && x.Employee.FullName.ToLower().Contains(searchKey));
             
                if(query.Any(x => x.Job.JobTitle.ToLower().Contains(searchKey)))
                                return query.Where(x=>x.Job.JobTitle.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.Pay!.ToLower().Contains(searchKey) ||
                        x.AnnualIp!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.OfferApprovals!.AsQueryable();
    }

    public async Task<IEnumerable<OfferApproval>> GetAllOfferApprovals()
    {
        return await _context.OfferApprovals!
            .Include(x=>x.Employee)
            .Include(x=>x.Job)
            .ToListAsync();
    }
}
