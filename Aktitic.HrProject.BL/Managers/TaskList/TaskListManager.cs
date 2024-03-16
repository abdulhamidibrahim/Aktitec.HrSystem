
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrTaskList.BL;

using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TaskListManager:ITaskListManager
{
    private readonly ITaskListRepo _projectRepo;

    public TaskListManager(ITaskListRepo projectRepo)
    {
        _projectRepo = projectRepo;
    }
    
    public Task<int> Add(TaskListAddDto projectAddDto)
    {
        var project = new TaskList()
        {
            ListName = projectAddDto.ListName,
        };
        return _projectRepo.Add(project);
    }

    public Task<int> Update(TaskListUpdateDto projectUpdateDto, int id)
    {
        var project = _projectRepo.GetById(id);
        
        if (project.Result == null) return Task.FromResult(0);
        
        if(projectUpdateDto.ListName != null) project.Result.ListName = projectUpdateDto.ListName;

        return _projectRepo.Update(project.Result);
    }

    public Task<int> Delete(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project.Result != null) return _projectRepo.Delete(project.Result);
        return Task.FromResult(0);
    }

    public Task<TaskListReadDto>? Get(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project.Result == null) return null;
        return Task.FromResult(new TaskListReadDto()
        {
            Id = project.Result.Id,
            ListName = project.Result.ListName,
            
        });
    }

    public Task<List<TaskListReadDto>> GetAll()
    {
        var project = _projectRepo.GetAll();
        return Task.FromResult(project.Result.Select(note => new TaskListReadDto()
        {
            Id = note.Id,
            ListName = note.ListName,
            
        }).ToList());
    }
}
