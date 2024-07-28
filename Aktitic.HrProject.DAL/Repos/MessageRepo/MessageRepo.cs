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
                        x.SenderName!.ToLower().Contains(searchKey) ||
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

    public void DeleteRange(List<Message> oldMessages)
    {
        _context.Messages.RemoveRange(oldMessages);
    }
}
