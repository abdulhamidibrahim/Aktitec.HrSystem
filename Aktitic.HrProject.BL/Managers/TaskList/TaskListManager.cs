
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Aktitic.HrTaskList.BL;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TaskListManager:ITaskListManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    

    public TaskListManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(TaskListAddDto projectAddDto)
    {
        var project = new TaskList()
        {
            ListName = projectAddDto.ListName,
            Priority = projectAddDto.Priority,
            DueDate = projectAddDto.DueDate,
            Status = projectAddDto.Status,
            TaskId = projectAddDto.TaskId,
            TaskBoardId = projectAddDto.TaskBoardId,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.TaskList.Add(project);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TaskListUpdateDto projectUpdateDto, int id)
    {
        var project = _unitOfWork.TaskList.GetById(id);

        if (project == null) return Task.FromResult(0);
        
        if(projectUpdateDto.ListName != null) project.ListName = projectUpdateDto.ListName;
        if(projectUpdateDto.Priority != null) project.Priority = projectUpdateDto.Priority;
        if(projectUpdateDto.DueDate!= null) project.DueDate = projectUpdateDto.DueDate;
        if(projectUpdateDto.Status != null) project.Status = projectUpdateDto.Status;
        if(projectUpdateDto.TaskId != null) project.TaskId = projectUpdateDto.TaskId;
        if(projectUpdateDto.TaskBoardId != null) project.TaskBoardId = projectUpdateDto.TaskBoardId;

        project.UpdatedAt = DateTime.Now;
        _unitOfWork.TaskList.Update(project);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var taskList = _unitOfWork.TaskList.GetById(id);
        if (taskList==null) return Task.FromResult(0);
        taskList.IsDeleted = true;
        taskList.DeletedAt = DateTime.Now;
        _unitOfWork.TaskList.Update(taskList);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<TaskListReadDto>? Get(int id)
    {
        var project = _unitOfWork.TaskList.GetById(id);
        if (project == null) return null;
        return Task.FromResult(new TaskListReadDto()
        {  
            Id = project.Id,
            ListName = project.ListName,
            Priority = project.Priority,
            DueDate = project.DueDate,
            Status = project.Status,
            TaskBoardId = project.TaskBoardId,
            TaskId = new TaskDto()
            {
                    
                Id = project.Task.Id,
                Text = project.Task.Text,
                Description = project.Task.Description,
                Date = project.Task.Date,
                Priority = project.Task.Priority,
                Completed = project.Task.Completed,
                ProjectId = project.Task.ProjectId,
               
            }
        });
    }

    public Task<List<TaskListReadDto>> GetAll()
    {
        var project = _unitOfWork.TaskList.GetAllWithTask();
        return Task.FromResult(project.Result.Select(list => new TaskListReadDto()
        {
            Id = list.Id,
            ListName = list.ListName,
            Priority = list.Priority,
            DueDate = list.DueDate,
            Status = list.Status,
            TaskBoardId = list.TaskBoardId,
            TaskId = new TaskDto()
            {
                    
                Id = list.Task.Id,
                Text = list.Task.Text,
                Description = list.Task.Description,
                Date = list.Task.Date,
                Priority = list.Task.Priority,
                Completed = list.Task.Completed,
                ProjectId = list.Task.ProjectId,
            }
        }).ToList());
    }

    public Task<List<TaskListReadDto>> GetAllByTaskBoardId(int taskBoardId)
    {
        var project = _unitOfWork.TaskList.GetAllByTaskBoardId(taskBoardId);
        return Task.FromResult(project.Result.Select(list => new TaskListReadDto()
        {
            Id = list.Id,
            ListName = list.ListName,
            Priority = list.Priority,
            DueDate = list.DueDate,
            Status = list.Status,
            TaskBoardId = list.TaskBoardId,
            TaskId = new TaskDto()
            {
                    
                Id = list.Task.Id,
                Text = list.Task.Text,
                Description = list.Task.Description,
                Date = list.Task.Date,
                Priority = list.Task.Priority,
                Completed = list.Task.Completed,
                ProjectId = list.Task.ProjectId,
               
            }        }).ToList());
    }
}
