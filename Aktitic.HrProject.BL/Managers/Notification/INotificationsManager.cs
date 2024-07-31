using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface INotificationManager
{
    public Task<int> AddGeneral(NotificationAddDto notificationAddDto);
    public Task<int> AddForCompany(NotificationAddDto notificationAddDto);
    public Task<int> Delete(int id);
    public NotificationReadDto? GetReceivedNotifications(int id);
    Task SendNotification(string message);
    Task SendNotificationToSpecificCompany(int companyId, string message);

    public Task SendNotificationToSpecificUsers(List<int> users, string message);
}