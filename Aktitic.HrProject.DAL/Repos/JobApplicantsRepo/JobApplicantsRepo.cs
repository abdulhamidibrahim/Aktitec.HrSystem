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
                    .Include(x=>x.Job)
                .AsQueryable();
            
            
            if(query.Any(x => searchKey != null && x.Job.JobTitle.ToLower().Contains(searchKey)))
                return query.Where(x=>searchKey != null && x.Job.JobTitle.ToLower().Contains(searchKey));


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

    public async Task<List<JobApplicant>> GetJobApplicants()
    {
        return await _context.JobApplicants!
            .Include(x=>x.Job)
            .ToListAsync();
    }

    public async Task<object> GetTotalCount()
    {
        var jobApplicants = await  _context.JobApplicants!.CountAsync();
        var jobs = await _context.Jobs!.CountAsync();
        var shortlists = await _context.Shortlists!.CountAsync();
        var employees = await _context.Employees!.CountAsync();
        return new {jobApplicants = jobApplicants , jobs = jobs , shortlists = shortlists , employees = employees};
    }
}
