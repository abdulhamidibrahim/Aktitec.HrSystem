using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL.SignalR;

public class ChatHub : Hub
{
    private readonly UnitOfWork _unitOfWork;

    public ChatHub(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // public async Task SendMessage(string user, string message) 
    //     => await Clients.All.SendAsync("ReceiveMessage", user, message);
    // public async Task SendPrivateMessage(string user, string message) 
    //     => await Clients.User(user).SendAsync("ReceiveMessage", user, message);
    //
    // public async Task SendGroupMessage(string groupName, string message) 
    //     => await Clients.Group(groupName).SendAsync("ReceiveMessage", message);


    public async Task SendNotification(string user, string message) 
        => await Clients.All.SendAsync("ReceiveMessage", user, message);

    public async Task SendMessage(int senderId, string message, string fileName = null, string filePath = null)
    {
        var newMessage = new Message
        {
            SenderId = senderId,
            Text = message,
            Date = DateTime.UtcNow,
            FileName = fileName,
            FilePath = filePath
        };
        _unitOfWork.Message.Add(newMessage);
        await _unitOfWork.SaveChangesAsync();
    
        await Clients.All.SendAsync("ReceiveMessage", new
        {
            SenderId = senderId,
            Message = message,
            FileName = fileName,
            FilePath = filePath
        });
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
            FilePath = filePath
        };
        _unitOfWork.Message.Add(newMessage);
        await _unitOfWork.SaveChangesAsync();
    
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", new
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
            Text = message,
            Date = DateTime.UtcNow,
            GroupName = groupName,
            FileName = fileName,
            FilePath = filePath
        };
        _unitOfWork.Message.Add(newMessage);
        await _unitOfWork.SaveChangesAsync();
    
        await Clients.Group(groupName).SendAsync("ReceiveMessage", new
        {
            SenderId = senderId,
            GroupName = groupName,
            Message = message,
            FileName = fileName,
            FilePath = filePath
        });
    }
}