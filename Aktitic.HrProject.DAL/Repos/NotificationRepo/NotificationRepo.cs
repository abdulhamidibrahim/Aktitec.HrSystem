using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class NotificationRepo :GenericRepo<Notification>,INotificationRepo
{
    private readonly HrSystemDbContext _context;

    public NotificationRepo(HrSystemDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Notification>> GetAllReceivedNotifications(int userId)
    {
        if (_context.Notifications != null)
            return await _context.Notifications
                .Include(x => x.Receivers)
                .Where(x => x.Receivers != null && x.Receivers.Any(x => x.UserId == userId))
                .AsQueryable()
                .ToListAsync();
        return new Notification[] {};
    }
}