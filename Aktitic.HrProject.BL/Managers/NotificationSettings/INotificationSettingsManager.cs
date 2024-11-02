using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrTaskList.BL;

public interface INotificationSettingsManager
{
    public Task<int> Add(NotificationSettingsAddDto notificationSettings);
    // public Task<int> Update(PaymentUpdateDto paymentUpdateDto, int id);
    // public Task<int> Delete(int id);
    // public PaymentReadDto? Get(int id);
    public Task<List<NotificationSettingsReadDto>> GetAll();

    Task<int> Update(NotificationSettingsAddDto dto,int notificationSettingsId);
    
}