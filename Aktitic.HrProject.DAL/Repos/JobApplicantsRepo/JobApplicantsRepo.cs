using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class JobApplicantsRepo :GenericRepo<JobApplicant>,IJobApplicantsRepo
{
    private readonly HrSystemDbContext _context;

    public JobApplicantsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<JobApplicant> GlobalSearch(string? searchKey)
    {
        if (_context.JobApplicants != null)
        {
            var query = _context.JobApplicants
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate  );
                    return query;
                }
                
                query = query
                    .Where(x =>
                        x.Name!.ToLower().Contains(searchKey) ||
                        x.Email!.ToLower().Contains(searchKey) ||
                        x.Phone!.ToLower().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) );
                
                return query;
            }
           
        }

        return _context.JobApplicants!.AsQueryable();
    }
    
}
