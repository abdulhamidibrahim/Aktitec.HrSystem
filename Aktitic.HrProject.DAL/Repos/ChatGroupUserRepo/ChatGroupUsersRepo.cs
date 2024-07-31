using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class ChatGroupUsersRepo(HrSystemDbContext context) : GenericRepo<ChatGroupUser>(context), IChatGroupUsersRepo
{
    private readonly HrSystemDbContext _context = context;
}
