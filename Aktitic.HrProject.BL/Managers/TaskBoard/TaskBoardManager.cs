
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskBoard.BL;

public class TaskBoardManager:ITaskBoardManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskBoardManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        this._mapper = mapper;
    }
    
    public Task<int> Add(TaskBoardAddDto taskBoardAddDto)
    {
        var taskBoard = new TaskBoard()
        {
            ProjectId = taskBoardAddDto.ProjectId,
            ListName = taskBoardAddDto.ListName,
            Color = taskBoardAddDto.Color,
            CreatedAt = DateTime.Now,
        };
         _unitOfWork.TaskBoard.Add(taskBoard);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TaskBoardUpdateDto taskBoardUpdateDto, int id)
    {
        var taskBoard = _unitOfWork.TaskBoard.GetById(id);

        if (taskBoard == null) return Task.FromResult(0);
        
        if(taskBoardUpdateDto.ProjectId != null) taskBoard.ProjectId = taskBoardUpdateDto.ProjectId;
        if(taskBoardUpdateDto.ListName != null) taskBoard.ListName = taskBoardUpdateDto.ListName;
        if(taskBoardUpdateDto.Color != null) taskBoard.Color = taskBoardUpdateDto.Color;

        taskBoard.UpdatedAt = DateTime.Now;
        _unitOfWork.TaskBoard.Update(taskBoard);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var taskBoard = _unitOfWork.TaskBoard.GetById(id);
        if (taskBoard==null) return Task.FromResult(0);
        taskBoard.IsDeleted = true;
        taskBoard.DeletedAt = DateTime.Now;
        _unitOfWork.TaskBoard.Update(taskBoard);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<TaskBoardReadDto>? Get(int id)
    {
        var taskBoard = _unitOfWork.TaskBoard.GetWithTaskLists(id).Result;
        if (taskBoard == null) return null;
        return Task.FromResult(new TaskBoardReadDto()
        {
            Id = taskBoard.Id,
            ProjectId = taskBoard.ProjectId,
            
            ListName = taskBoard.ListName,
            
            Color = taskBoard.Color,
            TaskLists = taskBoard.TaskLists.Select(tl=> new MappedTaskList()
            {
                Id = tl.Id,
                TaskId = tl.TaskId,
                TaskText = tl.Task?.Text,
                DueDate = tl.DueDate,
                ListName = tl.ListName,
                Priority = tl.Priority,
                Status = tl.Status,
            }).ToList(),
        });
    }

    public Task<List<TaskBoardReadDto>> GetAll()
    {
        var taskBoard = _unitOfWork.TaskBoard.GetAllWithTaskLists();
        return Task.FromResult(taskBoard.Result.Select(board => new TaskBoardReadDto()
        {
            Id = board.Id,
            ProjectId = board.ProjectId,
                
            ListName = board.ListName,
            
            Color = board.Color,
            TaskLists = board.TaskLists.Select(tl=> new MappedTaskList()
            {
                Id = tl.Id,
                TaskId = tl.TaskId,
                TaskText = tl.Task?.Text,
                DueDate = tl.DueDate,
                ListName = tl.ListName,
                Priority = tl.Priority,
                Status = tl.Status,
            }).ToList(),
        }).ToList());
    }

    public Task<List<TaskBoardReadDto>> GetAllByProjectId(int projectId)
    {
        
        var taskBoard = _unitOfWork.TaskBoard.GetByProjectId(projectId);
        return Task.FromResult(taskBoard.Result.Select(board => new TaskBoardReadDto()
        {
            Id = board.Id,
            ProjectId = board.ProjectId,
                    
            ListName = board.ListName,
            
            Color = board.Color,
            TaskLists = board.TaskLists.Select(tl=> new MappedTaskList()
            {
                Id = tl.Id,
                TaskId = tl.TaskId,
                TaskText = tl.Task?.Text,
                DueDate = tl.DueDate,
                ListName = tl.ListName,
                Priority = tl.Priority,
                Status = tl.Status,
            }).ToList(),
        }).ToList());
    }
}
