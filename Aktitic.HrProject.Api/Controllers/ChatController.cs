using Aktitic.HrProject.BL.SignalR;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Aktitic.HrProject.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ChatController(IMessageManager messageService, IHubContext<ChatHub> hubContext) : ControllerBase
{
    [HttpPost("sendPrivateMessage")]
    public async Task<IActionResult> SendPrivateMessage
        (int senderId, int receiverId, string message, 
            string fileName = null, string filePath = null)
    {
        await messageService.SendPrivateMessage(senderId, receiverId, message, fileName, filePath);
        return Ok();
    }

    [HttpPost("sendGroupMessage")]
    public async Task<IActionResult> SendGroupMessage(int senderId, string groupName, string message, string fileName = null, string filePath = null)
    {
        await messageService.SendGroupMessage(senderId, groupName, message, fileName, filePath);
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
