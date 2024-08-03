
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class MessageManager:IMessageManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessageManager(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hubContext = hubContext;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task SendPrivateMessage(int senderId, int receiverId, string message,IFormFile? Attatchment = null)
    {
        var fileName = Attatchment?.FileName;
        var filePath = SaveFile(Attatchment);
            
        
          var newMessage = new Message
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Text = message,
            Date = DateTime.UtcNow,
            FilePath = filePath
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

    public async Task SendGroupMessage(int senderId, string groupName, string message, IFormFile? Attatchment=null)
    {
        var fileName = Attatchment?.FileName;

        var filePath = SaveFile(Attatchment);
        
        var newMessage = new Message
        {
            SenderId = senderId,
            GroupName = groupName,
            Text = message,
            Date = DateTime.UtcNow,
            FilePath = filePath
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
    
    private string? SaveFile(IFormFile? file)
    {
        var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/MessageFiles");
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }
        if (file == null) return null;
        var filePath = Path.Combine(uploads, file.FileName);
        using var fileStream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(fileStream);
        return filePath;
    }
    
    // create group 
    // public async Task<ChatGroup> CreateGroupAsync(string groupName, string createdBy)
    // {
    //     // Create group in database
    //     var group = new ChatGroup
    //     {
    //         Name = groupName,
    //         CreatedAt = DateTime.UtcNow,
    //         CreatedBy = createdBy
    //     };
    //     _context.ChatGroups.Add(group);
    //     await _context.SaveChangesAsync();
    //
    //     // Add admin to the Hubs group (if necessary)
    //     // await _hubContext.Groups.AddToGroupAsync(connectionId, groupName);
    //
    //     return group;
    // }

    // public async Task AddUserToGroupAsync(int userId, int groupId)
    // {
    //     var user =  _unitOfWork.ApplicationUser.GetById(userId);
    //     if (user != null)
    //     {
    //         var group =  _unitOfWork.ChatGroup.GetById(groupId);
    //         if (group != null)
    //         {
    //             var chatGroupUser = new ChatGroupUser
    //             {
    //                 ChatGroupId = group.Id,
    //                 UserId = user.Id
    //             };
    //             _unitOfWork.ChatGroupUsers.Add(chatGroupUser);
    //             await _unitOfWork.SaveChangesAsync();
    //
    //             // Add user to Hubs group
    //             await _hubContext.Groups.AddToGroupAsync(_, groupId);
    //         }
    //     }
    // }
    
}
