using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrTaskList.BL;

public interface IMessageManager
{
    Task SendPrivateMessage(int senderId, int receiverId, string message, IFormFile? attachment = null);
    Task SendGroupMessage(int senderId, int groupId, string message, IFormFile? attachment = null);
    
    
}