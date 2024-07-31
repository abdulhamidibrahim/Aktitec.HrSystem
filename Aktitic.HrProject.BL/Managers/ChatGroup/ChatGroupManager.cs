
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrProject.BL.Utilities;

using Aktitic.HrProject.DAL.Models;

using Aktitic.HrProject.DAL.UnitOfWork;

using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ChatGroupManager(
    IUnitOfWork unitOfWork,
    ChatHub hubContext) : IChatGroupManager
{
    public async Task<int> Add(ChatGroupAddDto chatGroupAddDto)
    {
        var chatGroup = new ChatGroup()
        {
            Name = chatGroupAddDto.Description,
            Description = chatGroupAddDto.Description,
            CreatedAt = DateTime.Now,
            CreatedBy = UserUtility.GetUserId(),
        };
        chatGroup.ChatGroupUsers = chatGroupAddDto.ChatGroupUsers.Select(x => new ChatGroupUser()
        {
            UserId = x.UserId,
            ChatGroupId = x.ChatGroupId,
            IsAdmin = x.IsAdmin,
            CreatedAt = DateTime.Now,
            CreatedBy = UserUtility.GetUserId(),
        }).ToList();
        
        var admin =  unitOfWork.ApplicationUser.GetUserAdmin(chatGroup.TenantId).Id;
        if (chatGroup.TenantId != null)
            await hubContext.AddUserToGroup(admin, chatGroupAddDto.ChatGroupUsers
                    .Select(x => x.UserId).ToList(),
                chatGroup.TenantId.Value);

        unitOfWork.ChatGroup.Add(chatGroup);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> AddGroupUsers(ChatGroupAddDto chatGroupAddDto,int chatGroupId)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(chatGroupId);
        if (chatGroup == null) return (0);
        chatGroup.ChatGroupUsers = chatGroup.ChatGroupUsers
            .Union(chatGroupAddDto.ChatGroupUsers
                .Select(x => new ChatGroupUser()
        {
            UserId = x.UserId,
            ChatGroupId = x.ChatGroupId,
            IsAdmin = x.IsAdmin,
            CreatedAt = DateTime.Now,
            CreatedBy = UserUtility.GetUserId(),
        })).ToList();
        unitOfWork.ChatGroup.Update(chatGroup);

        var admin =  unitOfWork.ApplicationUser.GetUserAdmin(chatGroup.TenantId).Id;
        if (chatGroup.TenantId != null)
            await hubContext.AddUserToGroup(admin, chatGroupAddDto.ChatGroupUsers.
                    Select(x => x.UserId).ToList(),
                chatGroup.TenantId.Value);
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ChatGroupUpdateDto chatGroupUpdateDto, int id)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(id);
        
        
        if (chatGroup == null) return Task.FromResult(0);
        
        if(chatGroupUpdateDto.Name != null) chatGroup.Name = chatGroupUpdateDto.Name;
        if(chatGroupUpdateDto.Description != null) chatGroup.Description = chatGroupUpdateDto.Description;
        
        // update chat group users 
        var chatGroupUsers = chatGroupUpdateDto.ChatGroupUsers
            .Select(x=> new ChatGroupUser()
        {
            UserId = x.UserId,
            ChatGroupId = x.ChatGroupId,
            UpdatedBy = UserUtility.GetUserId(),
            UpdatedAt = DateTime.Now,
        }).ToList();

        chatGroup.ChatGroupUsers = chatGroup.ChatGroupUsers.Intersect(chatGroupUsers).ToList();
        
        chatGroup.UpdatedAt = DateTime.Now;
        chatGroup.UpdatedBy = UserUtility.GetUserId();
        unitOfWork.ChatGroup.Update(chatGroup);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var chatGroup = unitOfWork.ChatGroup.GetById(id);
        if (chatGroup==null) return Task.FromResult(0);
        chatGroup.IsDeleted = true;
        chatGroup.DeletedAt = DateTime.Now;
        chatGroup.DeletedBy = UserUtility.GetUserId();
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
            ChatGroupId = x.ChatGroupId,
            UserId = x.UserId,
            IsAdmin = x.IsAdmin,
        }).ToList();
        return chatG;
    }

    public Task<List<ChatGroupReadDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}
