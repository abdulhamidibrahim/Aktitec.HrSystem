using Aktitic.HrProject.BL;

namespace Aktitic.HrTaskList.BL;

public interface ITaskListManager
{
    public Task<int> Add(TaskListAddDto projectAddDto);
    public Task<int> Update(TaskListUpdateDto projectUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<TaskListReadDto>? Get(int id);
    public Task<List<TaskListReadDto>> GetAll();
    
    public Task<List<TaskListReadDto>> GetAllByTaskBoardId(int taskBoardId);
}