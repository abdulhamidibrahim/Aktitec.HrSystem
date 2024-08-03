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
        return _context.Messages
            .Include(x=>x.Sender)
            .Where(m => m.GroupId == chatGroupId)
            .OrderByDescending(m => m.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
