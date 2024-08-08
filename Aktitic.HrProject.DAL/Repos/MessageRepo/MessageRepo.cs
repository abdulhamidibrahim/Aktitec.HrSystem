using System.Linq.Dynamic.Core;
using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class MessageRepo :GenericRepo<Message>,IMessageRepo
{
    private readonly HrSystemDbContext _context;

    public MessageRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Message> GlobalSearch(string? searchKey)
    {
        if (_context.Messages != null)
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                searchKey = searchKey.Trim().ToLower();
                
                if( DateTime.TryParse(searchKey, out var searchDate))
                {
                    query = query
                        .Where(x =>
                            x.Date == searchDate );
                    return query;
                }
                searchKey = searchKey.Trim().ToLower();
                query = query
                    .Where(x =>
                        x.Text!.ToLower().Contains(searchKey) ||
                        x.SenderId!.ToString().Contains(searchKey) );
                       
                        
                return query;
            }
           
        }

        return _context.Messages!.AsQueryable();
    }

    public List<Message> GetMessagesByTaskId(int taskId)
    {
        return _context.Messages
            .Where(x => x.TaskId == taskId)
            .ToList();
    }

    public async Task<List<Message>> GetMessagesInPrivateChat(int userId1, int userId2,int page,int pageSize)
    {
        if (_context.Messages != null)
            return await _context.Messages
                .Include(x=>x.Sender)
                .Include(x=>x.Receiver)
                .Where(x => x.SenderId == userId1 && x.ReceiverId == userId2
                            || x.SenderId == userId2 && x.ReceiverId == userId1)
                .OrderByDescending(x=>x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        return await Task.FromResult(new List<Message>());
    }

    public void DeleteRange(List<Message> oldMessages)
    {
        _context.Messages.RemoveRange(oldMessages);
    }
}
