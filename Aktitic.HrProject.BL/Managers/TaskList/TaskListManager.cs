
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Aktitic.HrTaskList.BL;

using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TaskListManager:ITaskListManager
{
    private readonly ITaskListRepo _projectRepo;
    private readonly IUnitOfWork _unitOfWork;

    public TaskListManager(ITaskListRepo projectRepo, IUnitOfWork unitOfWork)
    {
        _projectRepo = projectRepo;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(TaskListAddDto projectAddDto)
    {
        var project = new TaskList()
        {
            ListName = projectAddDto.ListName,
        }; 
        _projectRepo.Add(project);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TaskListUpdateDto projectUpdateDto, int id)
    {
        var project = _projectRepo.GetById(id);

        if (project == null) return Task.FromResult(0);
        
        if(projectUpdateDto.ListName != null) project.ListName = projectUpdateDto.ListName;
        _projectRepo.Update(project);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _projectRepo.GetById(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<TaskListReadDto>? Get(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project == null) return null;
        return Task.FromResult(new TaskListReadDto()
        {
            Id = project.Id,
            ListName = project.ListName,
            
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
