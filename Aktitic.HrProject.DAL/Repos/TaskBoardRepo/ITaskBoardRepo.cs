using Aktitic.HrProject.DAL.Models;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITaskBoardRepo :IGenericRepo<TaskBoard>
{
    //get by project id
    Task<List<TaskBoard>> GetByProjectId(int projectId);
    Task<List<TaskBoard>> GetAllWithTaskLists();
    Task<TaskBoard?> GetWithTaskLists(int id);
}