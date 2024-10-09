using Microsoft.AspNetCore.Http;

namespace Aktitic.HrTaskList.BL;

public interface IMessageManager
{
    Task SendPrivateMessage(int receiverId, string message, IFormFile? attachment = null);
    // Task SendGroupMessage(int groupId, string message, IFormFile? attachment = null);
    
    
}