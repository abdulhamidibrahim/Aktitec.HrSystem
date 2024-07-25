using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class PerformanceIndicatorRepo :GenericRepo<PerformanceIndicator>,IPerformanceIndicatorRepo
{
    private readonly HrSystemDbContext _context;

    public PerformanceIndicatorRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<PerformanceIndicator> GlobalSearch(string? searchKey)
    {
        if (_context.PerformanceIndicators != null)
        {
            var query = _context.PerformanceIndicators
                .Include(x=>x.AddedBy)
                .Include(x=>x.Department)
                .Include(x=>x.Designation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }

                query = query
                    .Where(x =>
                        x.AddedBy.FullName!.ToLower().Contains(searchKey) ||
                        x.Designation.Name!.ToLower().Contains(searchKey) ||
                        x.Administration!.ToLower().Contains(searchKey) ||
                        x.Attendance!.ToLower().Contains(searchKey) ||
                        x.Efficiency!.ToLower().Contains(searchKey) ||
                        x.Integrity!.ToLower().Contains(searchKey) ||
                        x.Management!.ToLower().Contains(searchKey) ||
                        x.Marketing!.ToLower().Contains(searchKey) ||
                        x.Professionalism!.ToLower().Contains(searchKey) ||
                        x.PresentationSkill!.ToLower().Contains(searchKey) ||
                        x.ConflictManagement!.ToLower().Contains(searchKey) ||
                        x.CriticalThinking!.ToLower().Contains(searchKey) ||
                        x.CustomerExperience!.ToLower().Contains(searchKey) ||
                        x.MeetDeadline!.ToLower().Contains(searchKey) ||
                        x.TeamWork!.ToLower().Contains(searchKey) ||
                        x.QualityOfWork!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) ||
                        x.Department.Name!.ToLower().Contains(searchKey) 
                        
                        );
                        // x.Employee!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
           
        }

        return _context.PerformanceIndicators!.AsQueryable();
    }
    
    public IQueryable<PerformanceIndicator> GetAllWithEmployees()
    {
        
        return _context.PerformanceIndicators!
            .Include(x=>x.AddedBy)
            .Include(x=>x.Department)
            .Include(x=>x.Designation);
    }
    
    public IQueryable<PerformanceIndicator> GetWithEmployees(int id)
    {
        
        return _context.PerformanceIndicators!
            .Include(x=>x.AddedBy)
            .Include(x=>x.Department)
            .Include(x=>x.Designation);
    }
}
