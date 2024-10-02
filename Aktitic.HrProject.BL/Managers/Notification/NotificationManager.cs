using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using File = System.IO.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class NotificationManager(
    IUnitOfWork unitOfWork,
    UserUtility userUtility,
    ChatHub chatHub) : INotificationManager
{
    public Task<int> AddGeneral(NotificationAddDto notificationAddDto)
    {
        var notification = new Notification()
        {
            Title = notificationAddDto.Title,
            Content = notificationAddDto.Content,
            IsAll = notificationAddDto.IsAll,
            Priority = notificationAddDto.Priority,
            CreatedBy = userUtility.GetUserName(),
            CreatedAt = DateTime.Now,
        };

        if (!notification.IsAll)
        {
            notification.Receivers = notificationAddDto.Receivers?.Select(receiver => new ReceivedNotification()
            {
                IsRead = false,
                UserId = receiver,
                CreatedAt = DateTime.Now,
                CreatedBy = userUtility.GetUserId(),
            }).ToList();
        }
        
        unitOfWork.Notification.Add(notification);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> AddForCompany(NotificationAddDto notificationAddDto)
    {
        var notification = new Notification()
        {
            Title = notificationAddDto.Title,
            Content = notificationAddDto.Content,
            IsAll = notificationAddDto.IsAll,
            Priority = notificationAddDto.Priority,
            CreatedBy = userUtility.GetUserId(),
            CreatedAt = DateTime.Now,
            CompanyId = int.Parse(userUtility.GetCurrentCompany()),
        };

        if (!notification.IsAll)
        {
            notification.Receivers = notificationAddDto.Receivers?.Select(receiver => new ReceivedNotification()
            {
                IsRead = false,
                UserId = receiver,
                CreatedAt = DateTime.Now,
                CreatedBy = userUtility.GetUserId(),
            }).ToList();
        }
        unitOfWork.Notification.Add(notification);
        return unitOfWork.SaveChangesAsync();
    }


    public Task<int> Delete(int id)
    {
        unitOfWork.Notification.Delete(id);
       
        var notification = unitOfWork.Notification.GetById(id);

        if (notification == null) return Task.FromResult(0);

        notification.IsDeleted = true;
        notification.DeletedAt = DateTime.Now;
        notification.DeletedBy = userUtility.GetUserId();
        
        unitOfWork.Notification.Update(notification);
            
        
        return unitOfWork.SaveChangesAsync();
    }

    public NotificationReadDto? GetReceivedNotifications(int id)
    {
        var notification = unitOfWork.Notification.GetById(id);
        if (notification == null) return null;
        var result = new NotificationReadDto()
        {
            Id = notification.Id,
            Title = notification.Title,
            Content = notification.Content,
            IsAll = notification.IsAll,
            Priority = notification.Priority,
            CreatedBy = notification.CreatedBy,
            CreatedAt = notification.CreatedAt,
        };
        if (!result.IsAll)
        {
             result.Receivers = notification.Receivers?.Select(receiver => new ReceivedNotificationDto()
            {
                Id = receiver.Id,
                UserId = receiver.UserId,
                CreatedBy = receiver.CreatedBy,
                CreatedAt = receiver.CreatedAt,
            }).ToList();
        }
        return result;          
        }
    
    
    public async Task SendNotification(string message)
    {
        await chatHub.Clients.All.SendAsync("ReceiveNotification", message);
    }

    public async Task SendNotificationToSpecificCompany(int companyId, string message)
    {
        var users =await unitOfWork.ApplicationUser.GetUserIdsByCompanyId(companyId);
        if (users != null && users.Any())
        {
            var userIdsAsStrings = users.Select(id => id.ToString()).ToList();

            await chatHub.Clients.Users(userIdsAsStrings).SendAsync("ReceiveNotification", message);
        }
    }
    
    public async Task SendNotificationToSpecificUsers(List<int> users, string message)
    {
       
            var userIdsAsStrings = users.Select(id => id.ToString()).ToList();

            await chatHub.Clients.Users(userIdsAsStrings).SendAsync("ReceiveNotification", message);
        
    }
}
