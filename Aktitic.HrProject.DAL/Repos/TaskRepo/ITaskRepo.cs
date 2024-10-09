using Aktitic.HrProject.DAL.Models;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.DAL.Repos;

public interface ITaskRepo :IGenericRepo<Task>
{
    IQueryable<Task> GlobalSearch(string? searchKey);
    IEnumerable<Task>? GetTaskByProjectId(int projectId);
    IEnumerable<Task>? GetTaskByCompleted(bool completed);
    IEnumerable<Task>? GetCompletedTasks(int porjectId);
    
    IEnumerable<Task>? GetAllTasksWithEmployeeAndProject();
    Task? GetTaskWithMessages(int id);

    Task? GetTaskWithEmployee(int id);
}