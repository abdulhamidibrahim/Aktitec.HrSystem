using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTask.BL;

public interface ITaskManager
{
    public Task<int> Add(TaskAddDto taskAddDto);
    public Task<int> Update(TaskUpdateDto taskUpdateDto, int id);
    public Task<int> Delete(int id);
    public TaskReadSingleDto Get(int id);
    public Task<List<TaskReadDto>> GetAll();
    
    public Task<FilteredTaskDto> GetFilteredTasksAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TaskDto>> GlobalSearch(string searchKey,string? column);
    public Task<List<TaskDto>> GetTaskWithProjectId(int projectId);
        
    public Task<List<TaskDto>> GetTaskByCompleted(bool completed);
    public Task<List<TaskDto>> GetCompletedTasks(int projectId);
}