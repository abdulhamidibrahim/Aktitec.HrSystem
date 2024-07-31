
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class MessageManager:IMessageManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessageManager(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _hubContext = hubContext;
    }
    
    public async Task SendPrivateMessage(int senderId, int receiverId, string message, string fileName = null, string filePath = null)
    {
        var newMessage = new Message
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Text = message,
            Date = DateTime.UtcNow,
            FileName = fileName,
            // FilePath = filePath
        };
        _unitOfWork.Message.Add(newMessage);
        await _unitOfWork.SaveChangesAsync();

        await _hubContext.Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", new
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Message = message,
            FileName = fileName,
            FilePath = filePath
        });
    }

    public async Task SendGroupMessage(int senderId, string groupName, string message, string fileName = null, string filePath = null)
    {
        var newMessage = new Message
        {
            SenderId = senderId,
            GroupName = groupName,
            Text = message,
            Date = DateTime.UtcNow,
            FileName = fileName,
            // FilePath = filePath
        };
        _unitOfWork.Message.Add(newMessage);
        await _unitOfWork.SaveChangesAsync();

        await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", new
        {
            SenderId = senderId,
            GroupName = groupName,
            Message = message,
            FileName = fileName,
            FilePath = filePath
        });
    }

    
    
}
