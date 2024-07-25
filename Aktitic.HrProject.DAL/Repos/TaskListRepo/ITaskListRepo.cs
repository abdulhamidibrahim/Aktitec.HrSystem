using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITaskListRepo :IGenericRepo<TaskList>
{
    
    Task<IEnumerable<TaskList>> GetAllByTaskBoardId(int id);
    Task<IEnumerable<TaskList>> GetAllWithTask();
}