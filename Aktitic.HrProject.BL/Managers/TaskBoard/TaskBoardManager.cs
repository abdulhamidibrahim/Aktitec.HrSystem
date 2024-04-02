
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Aktitic.HrTaskBoard.BL;

using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskBoard.BL;

public class TaskBoardManager:ITaskBoardManager
{
    private readonly ITaskBoardRepo _taskBoardRepo;
    private readonly IUnitOfWork _unitOfWork;

    public TaskBoardManager(ITaskBoardRepo taskBoardRepo, IUnitOfWork unitOfWork)
    {
        _taskBoardRepo = taskBoardRepo;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(TaskBoardAddDto taskBoardAddDto)
    {
        var taskBoard = new TaskBoard()
        {
            ProjectId = taskBoardAddDto.ProjectId,
        };
         _taskBoardRepo.Add(taskBoard);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TaskBoardUpdateDto taskBoardUpdateDto, int id)
    {
        var taskBoard = _taskBoardRepo.GetById(id);

        if (taskBoard == null) return Task.FromResult(0);
        
        if(taskBoardUpdateDto.ProjectId != null) taskBoard.ProjectId = taskBoardUpdateDto.ProjectId;
        
        _taskBoardRepo.Update(taskBoard);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {

        _taskBoardRepo.GetById(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<TaskBoardReadDto>? Get(int id)
    {
        var taskBoard = _taskBoardRepo.GetById(id);
        if (taskBoard == null) return null;
        return Task.FromResult(new TaskBoardReadDto()
        {
            Id = taskBoard.Id,
            ProjectId = taskBoard.ProjectId,
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
