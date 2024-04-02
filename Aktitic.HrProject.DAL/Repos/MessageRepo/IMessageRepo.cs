using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface IMessageRepo :IGenericRepo<Message>
{
    IQueryable<Message> GlobalSearch(string? searchKey);
    // Task<Message> GetEstimateWithItems(int id);
    // Task<List<Message>> GetAllMessageWithItems();
    List<Message> GetMessagesByTaskId(int taskId);
    void DeleteRange(List<Message> oldMessages);
}