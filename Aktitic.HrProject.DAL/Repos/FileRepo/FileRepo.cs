using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.DAL.Repos;

public class FileRepo :GenericRepo<File>,IFileRepo
{
    private readonly HrSystemDbContext _context;

    public FileRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<File>> GlobalSearch(string? searchKey)
    {
        if (_context.Files != null)
        {
            var query = _context.Files
                .Include(x=>x.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.CreatedAt == searchDate );
                    return await query.ToListAsync();
                }
                
                
                if(query.Any(x => x.User.FullName != null && x.User.FullName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.User.FullName.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.FileName!.ToLower().Contains(searchKey) ||
                        x.FileSize!.ToLower().Contains(searchKey) ||
                        x.VersionNumber!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey) );
                       
                        
                return  await query.ToListAsync();
            }
           
        }

        return  await _context.Files!.AsQueryable().ToListAsync();
    }

    public async Task<IEnumerable<File>> GetByProjectId(int projectId)
    {
        if (_context.Files != null)
        {
            return await _context.Files!.Where(x=>x.ProjectId==projectId).ToListAsync();
        }

        return new List<File>();
    }
}
