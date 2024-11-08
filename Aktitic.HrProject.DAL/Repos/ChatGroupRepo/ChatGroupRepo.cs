using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ChatGroupRepo(HrSystemDbContext context) : GenericRepo<ChatGroup>(context), IChatGroupRepo
{
    private readonly HrSystemDbContext _context = context;
    public List<Message> GetMessages(int chatGroupId, int page, int pageSize)
    {
        if (page < 1)
        {
            page = 1;
        }
        return _context.Messages
            .Include(x=>x.Sender)
            .Where(m => m.GroupId == chatGroupId)
            .OrderByDescending(m => m.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<List<ChatGroup>> GetGroups(int page, int pageSize)
    {
        if (page < 1)
        {
            page = 1;
        }
        return await _context.ChatGroups
            .Include(x => x.ChatGroupUsers)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public object? GetUserGroups(int userId, int page, int pageSize)
    {
        if (page < 1)
        {
            page = 1;
        }
        return _context.ChatGroups?
            .Include(x => x.ChatGroupUsers)
            .Where(x => x.ChatGroupUsers != null && x.ChatGroupUsers.Any(x => x.UserId == userId))
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
