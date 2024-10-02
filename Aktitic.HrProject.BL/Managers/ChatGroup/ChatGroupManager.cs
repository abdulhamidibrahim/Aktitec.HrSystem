
using System.Text.Json.Serialization;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using File = System.IO.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ChatGroupManager(
    IUnitOfWork unitOfWork,
    UserUtility userUtility,
    IWebHostEnvironment webHostEnvironment,
    ChatHub hubContext) : IChatGroupManager
{
    public async Task<int> Add(ChatGroupAddDto chatGroupAddDto)
    {
        var chatGroup = new ChatGroup
        {
            Name = chatGroupAddDto.Description,
            Description = chatGroupAddDto.Description,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
            
        };
        
        var chatUsers = JsonConvert.DeserializeObject<List<ChatGroupUserDto>>(chatGroupAddDto.GroupUsers!);
        chatGroup.ChatGroupUsers = chatUsers?.Select(x => new ChatGroupUser()
        {
            UserId = x.UserId,
            ChatGroupId = chatGroup.Id,
            IsAdmin = x.IsAdmin,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
        }).ToList();



        if (chatGroupAddDto.Image != null)
        {
            var unique = Guid.NewGuid();

            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/chats", unique.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await using var fileStream = new FileStream(Path.Combine(path, chatGroupAddDto.Image.FileName), FileMode.Create);
            await chatGroupAddDto.Image.CopyToAsync(fileStream);

            chatGroup.Image = "uploads/chats"+ unique + "/" + chatGroupAddDto.Image?.FileName;


        }
      
        // send the SignalR Request 
        
        var admin = unitOfWork.ApplicationUser.GetUserAdmin(chatGroup.TenantId).Id;
        if (chatGroup.TenantId != null)
            await hubContext.AddUsersToGroup(admin, chatGroup.ChatGroupUsers
                    .Select(x => x.UserId).ToList(),
                chatGroup.TenantId.Value);

        unitOfWork.ChatGroup.Add(chatGroup);
        return await unitOfWork.SaveChangesAsync();
        
    }

    public async Task<int> AddGroupUsers(List<ChatGroupUserDto> chatGroupAddDto, int chatGroupId)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(chatGroupId);
        if (chatGroup == null) return (0);
        chatGroup.ChatGroupUsers = chatGroup.ChatGroupUsers?
            .Union(chatGroupAddDto
                .Select(x => new ChatGroupUser()
                {
                    UserId = x.UserId,
                    ChatGroupId = x.GroupId,
                    IsAdmin = x.IsAdmin,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userUtility.GetUserId(),
                })).ToList();
        
        unitOfWork.ChatGroup.Update(chatGroup);

        var admin = unitOfWork.ApplicationUser.GetUserAdmin(chatGroup.TenantId).Id;
        if (chatGroup.TenantId != null)
            await hubContext.AddUsersToGroup(admin, chatGroupAddDto.Select(x => x.UserId).ToList(),
                chatGroup.TenantId.Value);
        await hubContext.Clients.Group(chatGroup.TenantId.Value.ToString())
            .SendAsync("ReceiveMessage", "New User Added", "New User Added to the group");

        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> SendGroupMessage(MessageAddDto messageAddDto, int chatGroupId)
    {
        var chatGroup =
            unitOfWork.ChatGroup.GetById(chatGroupId);
    
        if (chatGroup == null) return (0);

            var message = new Message()
            {
                GroupId = chatGroupId,
                SenderId = messageAddDto.SenderId,
                Text = messageAddDto.Text,
                CreatedAt = DateTime.Now,
                CreatedBy = userUtility.GetUserId(),
            };

            unitOfWork.Message.Add(message);
          
            await hubContext.Clients.Group(chatGroup.TenantId.Value.ToString())
                .SendAsync("ReceiveMessage", message.Text, message.CreatedBy);
            
            
            return await unitOfWork.SaveChangesAsync(); ;
    }

    public Task<object?> GetUserGroups(int userId, int page, int pageSize)
    {
        var groups = unitOfWork.ChatGroup.GetUserGroups(userId,page,pageSize);
        return Task.FromResult(groups);
    }

    public async Task<List<MessageReadDto>> GetGroupMessages(int chatGroupId, int page, int pageSize)
    {
        var messages = unitOfWork.ChatGroup.GetMessages(chatGroupId,page,pageSize);
        
        var messagesDto = messages.Select(x => new MessageReadDto()
        {
            Id = x.Id,
            Text = x.Text,
            SenderId = x.SenderId,
            // Documents = x.Documents,
            Date = x.Date,
            SenderName = x.Sender?.FullName,
            SenderPhotoUrl = x.Sender?.Image, 
            Group    = new ChatGroupReadDto()
            {
                Id = chatGroupId,
                Name = x.Group?.Name,
                Description = x.Group?.Description,
            }
        }).ToList();
        return messagesDto;
    }
            
    

    public Task<int> Update(ChatGroupUpdateDto chatGroupUpdateDto, int id)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(id);
        
        
        if (chatGroup == null) return Task.FromResult(0);
        
        if(!chatGroupUpdateDto.Name.IsNullOrEmpty()) chatGroup.Name = chatGroupUpdateDto.Name;
        if(!chatGroupUpdateDto.Description.IsNullOrEmpty()) chatGroup.Description = chatGroupUpdateDto.Description;
        
        // update chat group users 
        var chatGroupUsers = JsonConvert.DeserializeObject<List<ChatGroupUserDto>>(chatGroupUpdateDto.GroupUsers!);
        
        var groupUsers = chatGroupUsers?.Select(x => new ChatGroupUser()
        {
            UserId = x.UserId,
            ChatGroupId = chatGroup.Id,
            IsAdmin = x.IsAdmin,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
        }).ToList();


        if (groupUsers != null) chatGroup.ChatGroupUsers = chatGroup.ChatGroupUsers?.Intersect(groupUsers).ToList();
        
        if (chatGroupUpdateDto?.Image != null)
        {
            // Construct the path for the current image
            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, chatGroup.Image);

            // Delete the old image file if it exists
            if (File.Exists(oldImagePath))
            {
                try
                {
                    File.Delete(oldImagePath);
                }
                catch (Exception ex)
                {
                    // Log the exception (you might want to use a logging framework)
                    Console.WriteLine($"Failed to delete old image: {ex.Message}");
                }
            }

            // Use the same path for the new image
            var unique = Path.GetDirectoryName(chatGroup.Image);
            var path = Path.Combine(webHostEnvironment.WebRootPath, unique);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, chatGroupUpdateDto.Image.FileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            chatGroupUpdateDto.Image.CopyTo(fileStream);

            chatGroup.Image = Path.Combine(unique, chatGroupUpdateDto.Image.FileName);
        }

        
        chatGroup.UpdatedAt = DateTime.Now;
        chatGroup.UpdatedBy = userUtility.GetUserId();
        unitOfWork.ChatGroup.Update(chatGroup);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(id);
        if (chatGroup==null) return Task.FromResult(0);
        chatGroup.IsDeleted = true;
        chatGroup.DeletedAt = DateTime.Now;
        chatGroup.DeletedBy = userUtility.GetUserId();
        unitOfWork.ChatGroup.Update(chatGroup);
        return unitOfWork.SaveChangesAsync();
    }

    public ChatGroupReadDto? Get(int id)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(id);
        if (chatGroup == null) return null;
        var chatG = new ChatGroupReadDto()
        {
            Id = chatGroup.Id,
            Description = chatGroup.Description,
            CreatedBy =chatGroup.CreatedBy,
            CreatedAt = chatGroup.CreatedAt,
        };
        chatG.ChatGroupUsers = chatGroup.ChatGroupUsers.Select(x => new ChatGroupUserDto()
        {
            GroupId = x.ChatGroupId,
            UserId = x.UserId,
            IsAdmin = x.IsAdmin,
        }).ToList();
        return chatG;
    }

    public async Task<List<ChatGroupReadDto>> GetAll(int page, int pageSize)
    {
        var chatGroups = await unitOfWork.ChatGroup.GetGroups(page,pageSize);
        var chatGroupsDto = chatGroups.Select(x => new ChatGroupReadDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CreatedBy = x.CreatedBy,
            CreatedAt = x.CreatedAt,
            // GroupUsers = x.GroupUsers?.Select(y => new ChatGroupUserDto()
            // {
            //     GroupId = y.GroupId,
            //     UserId = y.UserId,
            //     IsAdmin = y.IsAdmin,
            // }).ToList()
        }).ToList();
        return (chatGroupsDto);        
    }

    public async Task<List<MessageDto>> GetMessagesInPrivateChat(int userId1, int userId2, int page, int pageSize)
    {
          var messages = await unitOfWork.Message.GetMessagesInPrivateChat(userId1, userId2, page, pageSize);
          var messagesDto = messages.Select(x => new MessageDto()
          {
              Id = x.Id,
              Text = x.Text,
              SenderId = x.SenderId,
              ReceiverId = x.ReceiverId,
              FilePath = x.FilePath,
              Date = x.Date,
              SenderName = x.Sender?.FullName,
              SenderPhotoUrl = x.Sender?.Image,
          }).ToList();
          return messagesDto;
    }
}
