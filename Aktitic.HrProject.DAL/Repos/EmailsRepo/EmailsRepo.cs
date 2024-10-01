using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class EmailsRepo(HrSystemDbContext context) : GenericRepo<Email>(context), IEmailsRepo
{
    private readonly HrSystemDbContext _context = context;

    public IQueryable<Email> GlobalSearch(string? searchKey,string email)
    {
        if (_context.Emails != null)
        {
            var query = _context.Emails
                .Include(x=>x.Sender)
                .Include(x=>x.Receiver)
                .Where(x=>x.Receiver.Email == email)
                .AsSplitQuery()
                .OrderByDescending(x=>x.Date)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                if(DateTime.TryParse(searchKey,out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate ||
                            x.CreatedAt == searchDate );
                    return query;
                }
                
                
                if(query.Any(x => x.Sender.FirstName.ToLower().Contains(searchKey) || x.Sender.LastName.ToLower().Contains(searchKey)))
                    return query.Where(x=>x.Sender.FirstName.ToLower().Contains(searchKey) || x.Sender.LastName.ToLower().Contains(searchKey));
            
                if(query.Any(x => x.Receiver.FirstName.ToLower().Contains(searchKey) || x.Receiver.LastName.ToLower().Contains(searchKey)))
                                return query.Where(x=>x.Receiver.FirstName.ToLower().Contains(searchKey) || x.Receiver.LastName.ToLower().Contains(searchKey));

                
                query = query
                    .Where(x =>
                        x.Cc!.ToLower().Contains(searchKey) ||
                        x.Bcc!.ToLower().Contains(searchKey) ||
                        x.Subject!.ToLower().Contains(searchKey) ||
                        x.Label!.ToLower().Contains(searchKey) ||
                        x.Description!.ToLower().Contains(searchKey));
                       
                        
                return query;
            }
            
            // get query for booleans
            if (searchKey == "true" || searchKey == "false")
            {
                query = query
                    .Where(x =>
                        x.Read.ToString().ToLower().Contains(searchKey) ||
                        x.Archive.ToString().ToLower().Contains(searchKey) ||
                        x.Draft.ToString().ToLower().Contains(searchKey) ||
                        x.Trash.ToString().ToLower().Contains(searchKey) ||
                        x.Selected.ToString().ToLower().Contains(searchKey) ||
                        x.Spam.ToString().ToLower().Contains(searchKey) ||
                        x.Starred.ToString().ToLower().Contains(searchKey));
                return query;
            }
           
        }

        return _context.Emails!.AsQueryable();
    }

    public Email GetEmail(int id)
    {
        return _context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Include(x=>x.Attachments)
            .FirstOrDefault(x => x.Id == id);
    }

   
    public async Task<IEnumerable<Email>> GetReceivedEmails(string email)
    {
        return await _context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.Receiver.Email == email)
            .AsSplitQuery()
            .OrderByDescending(x=>x.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Email>> GetSentEmails(int id)
    {
        return await (_context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.Sender.Id == id)
            .AsSplitQuery()
            .OrderByDescending(x=>x.Date)
            .ToListAsync());
    }

    public async Task<IEnumerable<Email>> GetStarredEmails(int? page, int? pageSize,string email)
    {
        return await context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.Receiver.Email == email)
            .AsSplitQuery()
            .Where(x => x.Starred)
            .Skip((page!.Value - 1) * pageSize!.Value)
            .Take(pageSize!.Value)
            .OrderByDescending(x=>x.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Email>> GetArchivedEmails(int? page, int? pageSize,string email)
    {
        return await context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.Receiver.Email == email)
            .AsSplitQuery()
            .Where(x => x.Archive)
            .Skip((page!.Value - 1) * pageSize!.Value)
            .Take(pageSize!.Value)
            .OrderByDescending(x=>x.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Email>> GetDraftedEmails(int? page, int? pageSize,string email)
    {
        return await context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.Receiver.Email == email)
            .AsSplitQuery()
            .Where(x => x.Draft)
            .Skip((page!.Value - 1) * pageSize!.Value)
            .Take(pageSize!.Value)
            .OrderByDescending(x=>x.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Email>> GetTrashedEmails(int? page, int? pageSize,string email)
    {
        return await context.Emails!
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Where(x => x.Receiver.Email == email)
            .AsSplitQuery()
            .Where(x => x.Trash)
            .Skip((page!.Value - 1) * pageSize!.Value)
            .Take(pageSize!.Value)
            .OrderByDescending(x=>x.Date)
            .ToListAsync();
    }

    public Task<MailAttachment> GetAttachments(int id)
    {
        return _context.Attachments!
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
