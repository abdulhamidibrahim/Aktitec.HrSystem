using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class MessageManager(
    IUnitOfWork unitOfWork,
    IHubContext<ChatHub> hubContext,
    UserUtility userUtility,
    IWebHostEnvironment webHostEnvironment)
    : IMessageManager
{
    public async Task SendPrivateMessage( int receiverId, string message,IFormFile? attachment = null)
    {
        var fileName = attachment?.FileName;
        var filePath = SaveFile(attachment);
            
        
          var newMessage = new Message
        {
            SenderId = int.Parse(userUtility.GetUserId()),
            ReceiverId = receiverId,
            Text = message,
            Date = DateTime.UtcNow,
            FilePath = filePath
        };
        unitOfWork.Message.Add(newMessage);
        await unitOfWork.SaveChangesAsync();

        await hubContext.Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", new
        {
            SenderId = int.Parse(userUtility.GetUserId()),
            ReceiverId = receiverId,
            Message = message,
            FileName = fileName,
            FilePath = filePath
        });
    }

    public async Task SendGroupMessage(int groupId, string message, IFormFile? attachment=null)
    {
        var fileName = attachment?.FileName;

        var filePath = SaveFile(attachment);
        
        var newMessage = new Message
        {
            SenderId = int.Parse(userUtility.GetUserId()),
            GroupId = groupId,
            Text = message,
            Date = DateTime.UtcNow,
            FilePath = filePath
        };
        unitOfWork.Message.Add(newMessage);
        await unitOfWork.SaveChangesAsync();

        // await _hubContext.Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", new
        // {
        //     SenderId = senderId,
        //     GroupName = groupId,
        //     Message = message,
        //     FileName = fileName,
        //     FilePath = filePath
        // });
    }
    
    private string? SaveFile(IFormFile? file)
    {
        var uploads = Path.Combine(webHostEnvironment.WebRootPath, "uploads/MessageFiles");
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
    // public async Task<ChatGroup> CreateGroupAsync(string groupId, string createdBy)
    // {
    //     // Create group in database
    //     var group = new ChatGroup
    //     {
    //         Name = groupId,
    //         CreatedAt = DateTime.UtcNow,
    //         CreatedBy = createdBy
    //     };
    //     _context.ChatGroups.Add(group);
    //     await _context.SaveChangesAsync();
    //
    //     // Add admin to the Hubs group (if necessary)
    //     // await _hubContext.Groups.AddToGroupAsync(connectionId, groupId);
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
    //                 GroupId = group.Id,
    //                 UserId = user.Id
    //             };
    //             _unitOfWork.GroupUsers.Add(chatGroupUser);
    //             await _unitOfWork.SaveChangesAsync();
    //
    //             // Add user to Hubs group
    //             await _hubContext.Groups.AddToGroupAsync(_, groupId);
    //         }
    //     }
    // }
    
}
