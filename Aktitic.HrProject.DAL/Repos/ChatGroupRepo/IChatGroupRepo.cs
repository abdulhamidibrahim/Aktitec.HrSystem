using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IChatGroupRepo :IGenericRepo<ChatGroup>
{
      List<Message> GetMessages(int chatGroupId, int page, int pageSize);
      Task<List<ChatGroup>> GetGroups(int page, int pageSize);
}