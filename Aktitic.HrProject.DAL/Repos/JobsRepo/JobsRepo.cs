using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class JobsRepo :GenericRepo<Job>,IJobsRepo
{
    private readonly HrSystemDbContext _context;

    public JobsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Job> GlobalSearch(string? searchKey)
    {
        if (_context.Jobs != null)
        {
            var query = _context.Jobs
                .Include(x=>x.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.StartDate == searchDate ||
                            x.ExpiredDate == searchDate );
                    return query;
                }
                
                
                if(query.Any(x => x.Department.Name != null && x.Department.Name.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Department.Name.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.JobLocation!.ToLower().Contains(searchKey) ||
                        x.NoOfVacancies!.ToLower().Contains(searchKey) ||
                        x.Experience!.ToLower().Contains(searchKey) ||
                        x.JobType!.ToLower().Contains(searchKey) ||
                        x.JobTitle!.ToLower().Contains(searchKey) ||
                        x.Age!.ToString().Contains(searchKey) ||
                        x.SalaryFrom!.ToString().Contains(searchKey) ||
                        x.SalaryTo!.ToString().Contains(searchKey) ||
                        x.Status!.ToLower().Contains(searchKey) ||
                        x.Category!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Jobs!.AsQueryable();
    }

    public async Task<IEnumerable<Job>> GetAllJobs()
    {
        return await _context.Jobs!
            .Include(x => x.Department)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetByCategory(string? category)
    {
        return await _context.Jobs!
            .Where(x => x.Category.Equals(category))
            .Include(x => x.Department)
            .ToListAsync();
    }

    public async Task<object> GetTotalCount()
    {
        var jobs = _context.Jobs!.AsQueryable();
        var offered = jobs.Count(x => x.Category == "Offered"||x.Category == "offered");
        var applied = jobs.Count(x => x.Category == "Applied"||x.Category == "applied");
        var saved = jobs.Count(x => x.Category == "Saved"||x.Category == "saved");
        var visited = jobs.Count(x => x.Category == "Visited"||x.Category == "visited");
        return await Task.FromResult(new { offered, applied, saved, visited });
    }
}
