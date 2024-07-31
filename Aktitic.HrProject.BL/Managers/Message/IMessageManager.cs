using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IMessageManager
{
    Task SendPrivateMessage(int senderId, int receiverId, string message, string fileName = null, string filePath = null);
    Task SendGroupMessage(int senderId, string groupName, string message, string fileName = null, string filePath = null);
  
  
}