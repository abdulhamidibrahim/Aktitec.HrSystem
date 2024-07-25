
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Aktitic.HrTask.BL;
using AutoMapper;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.BL;

public class TaskManager:ITaskManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Add(TaskAddDto taskAddDto)
    {
        var task = new Task
        {
            Text = taskAddDto.Text,
            Description = taskAddDto.Description,
            Priority = taskAddDto.Priority,
            Completed = taskAddDto.Completed,
            AssignedTo = taskAddDto.AssignedTo,
            ProjectId = taskAddDto.ProjectId,
            Date = DateTime.Now,
            CreatedAt = DateTime.Now,
        };
        
        _unitOfWork.Task.Add(task);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> Update(TaskUpdateDto taskUpdateDto, int id)
    {
        var task = _unitOfWork.Task.GetById(id);
        
        // await _unitOfWork.SaveChangesAsync();
        if (task == null) return await System.Threading.Tasks.Task.FromResult(0);
        
        if(taskUpdateDto.Text != null) task.Text = taskUpdateDto.Text;
        if(taskUpdateDto.Description != null) task.Description = taskUpdateDto.Description;
        if(taskUpdateDto.Priority != null) task.Priority = taskUpdateDto.Priority;
        if(taskUpdateDto.Completed != null) task.Completed = taskUpdateDto.Completed;
        if(taskUpdateDto.AssignedTo != null) task.AssignedTo = taskUpdateDto.AssignedTo;
        task.Date = DateTime.Now;
        if(taskUpdateDto.ProjectId != null) task.ProjectId = taskUpdateDto.ProjectId;

        task.UpdatedAt = DateTime.Now;
        _unitOfWork.Task.Update(task);
        return await _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var task = _unitOfWork.Task.GetById(id);
        if (task==null) return System.Threading.Tasks.Task.FromResult(0);
        task.IsDeleted = true;
        task.DeletedAt = DateTime.Now;
        _unitOfWork.Task.Update(task);
        return _unitOfWork.SaveChangesAsync();
    }

    public TaskReadSingleDto Get(int id)
    {
        var task = _unitOfWork.Task.GetTaskWithEmployee(id);
        if (task == null) return new TaskReadSingleDto();
        return new TaskReadSingleDto()
        {
            Id = task.Id,
            Text = task.Text,
            Description = task.Description,
            Completed = task.Completed,
            AssignedTo = _mapper.Map<Employee,EmployeeDto>(task.AssignEmployee),
            Priority = task.Priority,
            ProjectId = task.ProjectId,
            Date = task.Date,
            Messages = _mapper.Map<IEnumerable<Message>,IEnumerable<MessageDto>>(task.Messages).ToList(),
        };
    }

    public Task<List<TaskReadDto>> GetAll()
    {
        var task = _unitOfWork.Task.GetAll();
        return System.Threading.Tasks.Task.FromResult(task.Result.Select(t => new TaskReadDto()
        {
            Id = t.Id,
            Description = t.Description,
            Completed = t.Completed,
            AssignedTo = t.AssignedTo,
            ProjectId = t.ProjectId,
            Priority = t.Priority,
            Text = t.Text,
            Date = t.Date,
            Messages = _mapper.Map<IEnumerable<Message>,IEnumerable<MessageDto>>(t.Messages).ToList(),
        }).ToList());
    }
    
     
     public Task<FilteredTaskDto> GetFilteredTasksAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var Tasks =  _unitOfWork.Task.GetAllTasksWithEmployeeAndProject();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = Tasks.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var TaskList = Tasks.ToList();

            var paginatedResults = TaskList.Skip((page - 1) * pageSize).Take(pageSize);

            List<TaskDto> taskDto = new();
            foreach (var task in paginatedResults)
            {
                taskDto.Add(new TaskDto()
                {
                    Id = task.Id,
                    Text = task.Text,
                    Description = task.Description,
                    Completed = task.Completed,
                    Date = task.Date,
                    Priority = task.Priority,
                    Project = _mapper.Map<Project,ProjectDto>(task.Project),
                    AssignEmployee = _mapper.Map<Employee,EmployeeDto>(task.AssignEmployee),
                    Message = _mapper.Map<IEnumerable<Message>,IEnumerable<MessageDto>>(task.Messages).ToList(),
                });
            }
            
            FilteredTaskDto result = new()
            {
                TaskDto = taskDto,
                TotalCount = count,
                TotalPages = pages
            };
            return System.Threading.Tasks.Task.FromResult(result);
        }

        if (Tasks != null)
        {
            IEnumerable<Task> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(Tasks, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(Tasks, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);


            List<TaskDto> taskDto = new();
            foreach (var task in paginatedResults)
            {
                taskDto.Add(new TaskDto()
                {
                    Id = task.Id,
                    Text = task.Text,
                    Description = task.Description,
                    Completed = task.Completed,
                    Date = task.Date,
                    Priority = task.Priority,
                    Project = _mapper.Map<Project,ProjectDto>(task.Project),
                    AssignEmployee = _mapper.Map<Employee,EmployeeDto>(task.AssignEmployee),
                    Message = _mapper.Map<IEnumerable<Message>,IEnumerable<MessageDto>>(task.Messages).ToList(),
                });
            }
            
            FilteredTaskDto result = new()
            {
                TaskDto = taskDto,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return System.Threading.Tasks.Task.FromResult(result);
        }

        return System.Threading.Tasks.Task.FromResult(new FilteredTaskDto());
    }
    private IEnumerable<Task> ApplyFilter(IEnumerable<Task> Tasks, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => Tasks.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => Tasks.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => Tasks.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => Tasks.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var taskValue) => ApplyNumericFilter(Tasks, column, taskValue, operatorType),
            _ => Tasks
        };
    }

    private IEnumerable<Task> ApplyNumericFilter(IEnumerable<Task> Tasks, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => Tasks.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue == value),
        "neq" => Tasks.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue != value),
        "gte" => Tasks.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue >= value),
        "gt" => Tasks.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue > value),
        "lte" => Tasks.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue <= value),
        "lt" => Tasks.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue < value),
        _ => Tasks
    };
}


    public Task<List<TaskDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Task> Task;
            Task = _unitOfWork.Task.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var task = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(Task);
            return System.Threading.Tasks.Task.FromResult(task.ToList());
        }

        var  Tasks = _unitOfWork.Task.GlobalSearch(searchKey);
        var tasks = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(Tasks);
        return System.Threading.Tasks.Task.FromResult(tasks.ToList());
    }

    public Task<List<TaskDto>> GetTaskWithProjectId(int projectId)
    {
        var Tasks = _unitOfWork.Task.GetTaskByProjectId(projectId);
        var tasks = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(Tasks);
        return System.Threading.Tasks.Task.FromResult(tasks.ToList());
    }

    public Task<List<TaskDto>> GetTaskByCompleted(bool completed)
    {
        var Tasks = _unitOfWork.Task.GetTaskByCompleted(completed);
        var tasks = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(Tasks);
        return System.Threading.Tasks.Task.FromResult(tasks.ToList());
    }
}
