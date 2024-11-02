using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class NotificationSettingsManager(IUnitOfWork unitOfWork, IMapper mapper) : INotificationSettingsManager
{
    private readonly IMapper _mapper = mapper;


    public Task<int> Add(NotificationSettingsAddDto notificationSettings)
    {
        var notification = new NotificationSettings()
        {
            Active = notificationSettings.Active,
            PageCode = notificationSettings.PageCode,
            CompanyId =  notificationSettings.CompanyId,
        };
        unitOfWork.NotificationSettings.Add(notification);
        return unitOfWork.SaveChangesAsync();
    }

    public async Task<List<NotificationSettingsReadDto>> GetAll()
    {
        var notification = await unitOfWork.NotificationSettings.GetAll();
        return notification.Select(x => new NotificationSettingsReadDto()
        {
            Id =    x.Id,
            PageCode = x.PageCode,
            Active = x.Active,
            CompanyId = x.CompanyId,
        }).ToList();
    }

    public Task<int> Update(NotificationSettingsAddDto dto, int notificationSettingsId)
    {
        var notification = unitOfWork.NotificationSettings.GetById(notificationSettingsId);
        if (notification is null) return Task.FromResult(0);

        notification.Active = dto.Active;
        notification.CompanyId = dto.CompanyId;
        notification.PageCode = dto.PageCode;
        
        unitOfWork.NotificationSettings.Update(notification);
        return unitOfWork.SaveChangesAsync();
    }
}
