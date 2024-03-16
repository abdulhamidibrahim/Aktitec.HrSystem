
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrTaskBoard.BL;

using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskBoard.BL;

public class TaskBoardManager:ITaskBoardManager
{
    private readonly ITaskBoardRepo _taskBoardRepo;

    public TaskBoardManager(ITaskBoardRepo taskBoardRepo)
    {
        _taskBoardRepo = taskBoardRepo;
    }
    
    public Task<int> Add(TaskBoardAddDto taskBoardAddDto)
    {
        var taskBoard = new TaskBoard()
        {
            ProjectId = taskBoardAddDto.ProjectId,
        };
        return _taskBoardRepo.Add(taskBoard);
    }

    public Task<int> Update(TaskBoardUpdateDto taskBoardUpdateDto, int id)
    {
        var taskBoard = _taskBoardRepo.GetById(id);
        
        if (taskBoard.Result == null) return Task.FromResult(0);
        
        if(taskBoardUpdateDto.ProjectId != null) taskBoard.Result.ProjectId = taskBoardUpdateDto.ProjectId;

        return _taskBoardRepo.Update(taskBoard.Result);
    }

    public Task<int> Delete(int id)
    {
        var taskBoard = _taskBoardRepo.GetById(id);
        if (taskBoard.Result != null) return _taskBoardRepo.Delete(taskBoard.Result);
        return Task.FromResult(0);
    }

    public Task<TaskBoardReadDto>? Get(int id)
    {
        var taskBoard = _taskBoardRepo.GetById(id);
        if (taskBoard.Result == null) return null;
        return Task.FromResult(new TaskBoardReadDto()
        {
            Id = taskBoard.Result.Id,
            ProjectId = taskBoard.Result.ProjectId,
        });
    }

    public Task<List<TaskBoardReadDto>> GetAll()
    {
        var taskBoard = _taskBoardRepo.GetAll();
        return Task.FromResult(taskBoard.Result.Select(note => new TaskBoardReadDto()
        {
            Id = note.Id,
            ProjectId = note.ProjectId,
            
        }).ToList());
    }
}
