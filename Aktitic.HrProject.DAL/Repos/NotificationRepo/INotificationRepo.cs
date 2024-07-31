using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface INotificationRepo :IGenericRepo<Notification>
{
    Task<IEnumerable<Notification>> GetAllReceivedNotifications(int userId);

}