using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrTaskList.BL;

public interface IMessageManager
{
    Task SendPrivateMessage(int receiverId, string message, IFormFile? attachment = null);
    // Task SendGroupMessage(int groupId, string message, IFormFile? attachment = null);
    
    
}