using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class DocumentRepo(HrSystemDbContext context) : GenericRepo<Document>(context), IDocumentRepo
{
    private readonly HrSystemDbContext _context = context;

    public async Task<IEnumerable<Document>> GlobalSearch(string? searchKey)
    {
        if (_context.Documents != null)
        {
            var query = _context.Documents
                .Include(x => x.User)
                .Include(x => x.Revisors)
                .Include(x=>x.FileUsers)
                .AsSplitQuery()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();

                if (DateTime.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.CreatedAt == searchDate);
                    return await query.ToListAsync();
                }


                if (query.Any(x => x.User != null
                                   && (x.User.FirstName.ToLower().Contains(searchKey)
                                       || x.User.LastName.ToLower().Contains(searchKey))))
                {
                    return query.Where(x => x.User != null
                                            && (x.User.FirstName.ToLower().Contains(searchKey)
                                                || x.User.LastName.ToLower().Contains(searchKey)))
                        .ToList();

                }

                // if (query.Any(x => x.Revisors != null
                //                    && (x.Revisors..ToLower().Contains(searchKey)
                //                        || x.User.LastName.ToLower().Contains(searchKey))))
                // {
                //     return query.Where(x=>x.User != null 
                //                           && (x.User.FirstName.ToLower().Contains(searchKey) 
                //                               || x.User.LastName.ToLower().Contains(searchKey)))
                //         .ToList();
                //
                // }


                query = query
                    .Where(x =>
                        x.Version!.ToString().Contains(searchKey) ||
                        x.Type!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey) ||
                        x.PrintSize!.ToLower().Contains(searchKey) ||
                        x.Revision!.ToString().Contains(searchKey) ||
                        x.Title!.ToLower().Contains(searchKey) ||
                        x.Status!.ToString().Contains(searchKey));


                return await query.ToListAsync();
            }

        }

        return await _context.Documents!.AsQueryable().ToListAsync();
    }

    public async Task<IEnumerable<Document>> GetByProjectId(int projectId)
    {
        if (_context.Documents != null)
        {
            return await _context.Documents!.Where(x => x.ProjectId == projectId).ToListAsync();
        }

        return new List<Document>();
    }

    public IEnumerable<Document> GetDocuments(string? type, string? status, int page, int pageSize)
    {
        if (_context.Documents != null)
        {
            var query = _context.Documents.AsQueryable();

            // Filter by status if provided
            if (!string.IsNullOrEmpty(status))
            {
                status = status.ToLower(); // Normalize for case-insensitive comparison
                query = query.Where(x => x.Status.ToLower().Contains(status));
            }

            // Filter by type if provided
            if (!string.IsNullOrEmpty(type))
            {
                type = type.ToLower(); // Normalize for case-insensitive comparison
                query = query.Where(x => x.Type.ToLower().Contains(type));
            }

            // Apply pagination
            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.CreatedAt)
                .ToList(); // Execute the query
        }

        return new List<Document>().AsEnumerable();
    }

    public async Task<Document> GetFileById(int id)
    {
        if (_context.Documents != null)
        {
            return await _context.Documents!
                .Include(x => x.DocumentFiles)
                .Include(x=>x.FileUsers)
                .Include(x=>x.Revisors)
                .ThenInclude(x=>x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        return (new Document());
    }

    public async Task<IEnumerable<Document>> GetDocumentWithRevision()
    {
        if (_context.Documents != null)
            return await _context.Documents
                .Include(x => x.Revisors)
                .Include(x=>x.FileUsers)
                .ToListAsync();
        return new List<Document>();
    }

    public void ComputeHash(int documentId)
    {
        var document = _context.Documents!
            .Include(document => document.DocumentFiles)
            .FirstOrDefault(x => x.Id == documentId);
        if (document != null)
            return;
        if (document?.DocumentFiles != null)
        {
            var hash = string.Empty;
            foreach (var file in document.DocumentFiles)
            {
                hash += file.FileHash + ",";
            }

            document.FilesHash = hash;
        }
    }

    public bool IsUniqueCode(string documentCode)
    {
        var result =  context.Documents != null && context.Documents
            .Any(x => x.DocumentCode.Equals(documentCode));
        return result;
    }

    public Task<IEnumerable<Document>> GetDocumentChain(string documentCode)
    {
        if (_context.Documents != null)
        {
            return Task.FromResult(_context.Documents
                .Where(x => x.DocumentCode == documentCode)
                .AsEnumerable());
        }

        return Task.FromResult(new List<Document>().AsEnumerable());
    }
}