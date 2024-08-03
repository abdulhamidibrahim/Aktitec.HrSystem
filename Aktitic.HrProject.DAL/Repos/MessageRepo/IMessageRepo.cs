using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IMessageRepo :IGenericRepo<Message>
{
    IQueryable<Message> GlobalSearch(string? searchKey);
    // Task<Message> GetEstimateWithItems(int id);
    // Task<List<Message>> GetAllMessageWithItems();
    List<Message> GetMessagesByTaskId(int taskId);
    public Task<List<Message>> GetMessagesInPrivateChat(int userId1, int userId2, int page, int pageSize);
    void DeleteRange(List<Message> oldMessages);
}