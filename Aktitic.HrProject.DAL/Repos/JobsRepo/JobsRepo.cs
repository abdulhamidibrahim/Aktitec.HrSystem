using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

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
                        x.Description!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.Jobs!.AsQueryable();
    }

    public IEnumerable<Job> GetAllJobs()
    {
        return _context.Jobs!.Include(x => x.Department).ToList();
    }
}
