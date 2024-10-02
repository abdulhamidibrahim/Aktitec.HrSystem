using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos;

public class LogsRepo :GenericRepo<AuditLog>,ILogsRepo
{
    private readonly HrSystemDbContext _context;

    public LogsRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<AuditLog> GlobalSearch(string? searchKey)
    {
        if (_context.AuditLogs != null)
        {
            var query = _context.AuditLogs
                .Include(x=>x.ModifiedRecords)
                .Include(x=>x.AppPages)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.TimeStamp == searchDate  );
                    return query;
                }
                
                  
                if(query.Any(x => x.AppPages != null && x.AppPages.Name.ToLower().Contains(searchKey) 
                                  || x.AppPages != null && x.AppPages.ArabicName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.AppPages != null && x.AppPages.Name.ToLower().Contains(searchKey) 
                                          || x.AppPages != null && x.AppPages.ArabicName.ToLower().Contains(searchKey))
                        .OrderByDescending(x=>x.TimeStamp); 
                
                
                if(query.Any(x => x.Action != null && x.Action.Name != null && x.Action.ArabicName != null && (x.Action != null && x.Action.Name.ToLower().Contains(searchKey) 
                       || x.Action != null && x.Action.ArabicName.ToLower().Contains(searchKey))))
                    return query.Where(x=>x.Action != null && x.Action.Name != null && x.Action.ArabicName != null && (x.Action != null && x.Action.Name.ToLower().Contains(searchKey) 
                            || x.Action != null && x.Action.ArabicName.ToLower().Contains(searchKey)))
                        .OrderByDescending(x=>x.TimeStamp);

                query = query
                    .Where(x =>
                        x.UserId!.ToLower().Contains(searchKey) ||
                        x.EntityName!.ToLower().Contains(searchKey) ||
                        x.Changes!.ToLower().Contains(searchKey) ||
                        x.IpAddress!.ToLower().Contains(searchKey) ||
                        x.IpAddress!.ToLower().Contains(searchKey) ||
                        x.IpAddress!.ToLower().Contains(searchKey) ||
                        x.TenantId!.ToString().Contains(searchKey) );
                       
                        
                return query.OrderByDescending(x=>x.TimeStamp);
            }
           
        }

        return _context.AuditLogs!.AsQueryable().OrderByDescending(x=>x.TimeStamp);
    }

    public async Task<IEnumerable<AuditLog>> GetAllLogs()
    {
        return await _context.AuditLogs!
            .Include(x=>x.ModifiedRecords)
            .Include(x=>x.AppPages)
            .Include(x=>x.Action)
            .OrderByDescending(x=>x.TimeStamp)
            .ToListAsync();
    }
}
