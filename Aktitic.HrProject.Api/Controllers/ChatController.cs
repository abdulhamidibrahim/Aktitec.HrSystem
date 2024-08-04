using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Aktitic.HrProject.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ChatController(IMessageManager messageService,IChatGroupManager chatGroupManager, IHubContext<ChatHub> hubContext) : ControllerBase
{
    [HttpPost("sendPrivateMessage")]
    public async Task<IActionResult> SendPrivateMessage
        (int senderId, int receiverId, string message, 
            IFormFile? attachment = null)
    {
        await messageService.SendPrivateMessage(senderId, receiverId, message, attachment);
        return Ok();
    }

    [HttpPost("sendGroupMessage")]
    public async Task<IActionResult> SendGroupMessage(int senderId, string groupName, string message,IFormFile? attachment = null)
    {
        await messageService.SendGroupMessage(senderId, groupName, message,attachment);
        return Ok();
    }

    [HttpGet("getGroupMessages/{chatGroupId}")]
    public async Task<IActionResult> GetGroupMessages(int chatGroupId,int page,int pageSize)
    {
        var messages = await chatGroupManager.GetGroupMessages(chatGroupId,page,pageSize);
        return Ok(messages);
    }
    
    [HttpGet("getMessagesInPrivateChat/{userId1}/{userId2}")]
    public async Task<IActionResult> GetMessagesInPrivateChat(int userId1, int userId2, int page, int pageSize)
    {
        var messages = await chatGroupManager.GetMessagesInPrivateChat(userId1, userId2, page, pageSize);
        return Ok(messages);
    }
    
    // [HttpGet("getGroups")]
    // public async Task<IActionResult> GetGroups()
    // {
    //     var groups = await chatGroupManager.GetAll();
    //     return Ok(groups);
    // }
    
    [HttpPost("createGroup")]
    public async Task<IActionResult> CreateGroup(ChatGroupAddDto dto)
    {
        await chatGroupManager.Add(dto);
        return Ok();
    }
    
    [HttpPost("addUsersToGroup/{chatGroupId}")]
    public async Task<IActionResult> JoinGroup(List<ChatGroupUserDto> dto,int chatGroupId)
    {
        await chatGroupManager.AddGroupUsers(dto, chatGroupId);
        return Ok();
    }

    [HttpPut("updateGroup/{chatGroupId}")]
    public async Task<IActionResult> UpdateGroup(ChatGroupUpdateDto dto,int chatGroupId)
    {
        await chatGroupManager.Update(dto, chatGroupId);
        return Ok();
    }
    
    [HttpGet("GetAllGroups/{page}/{pageSize}")]
    public async Task<IActionResult> GetAllGroups(int page, int pageSize)
    {
        var groups = await chatGroupManager.GetAll(page, pageSize);
        return Ok(groups);
    }
    
    [HttpDelete("deleteGroup/{chatGroupId}")]
    public async Task<IActionResult> DeleteGroup(int chatGroupId)
    {
        await chatGroupManager.Delete(chatGroupId);
        return Ok();
    }

   // join company group
   [HttpPost("joinCompanyGroup/{companyId}")]
   public async Task<IActionResult> JoinCompanyGroup(int companyId)
   {
       var connectionId = HttpContext.Connection.Id; // You might need a better way to track connection IDs
       await hubContext.Groups.AddToGroupAsync(connectionId, companyId.ToString());
       return Ok();
   }
   [HttpPost("leaveGroup")]
   public async Task<IActionResult> LeaveGroup(int companyId)
   {
       var connectionId = HttpContext.Connection.Id; // You might need a better way to track connection IDs
       await hubContext.Groups.RemoveFromGroupAsync(connectionId, companyId.ToString());
       return Ok();
   }
   
}
