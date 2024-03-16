using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.DAL.Repos;

public interface INotesRepo :IGenericRepo<Notes>
{

    public Task<List<NoteSender>> GetByReceiver(int receiverId);
    
    public Task<List<Notes>> GetBySender(int senderId);
    public Task<List<Notes>> GetStarred(int userId);
}