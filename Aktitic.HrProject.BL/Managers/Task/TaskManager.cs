
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrTask.BL;
using AutoMapper;
using Task = Aktitic.HrProject.DAL.Models.Task;

namespace Aktitic.HrProject.BL;

public class TaskManager:ITaskManager
{
    private readonly ITaskRepo _taskRepo;
    private readonly IMapper _mapper;

    public TaskManager(ITaskRepo taskRepo, IMapper mapper)
    {
        _taskRepo = taskRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(TaskAddDto taskAddDto)
    {
        var task = new Task
        {
            
            Date = DateTime.Now
        };
        return _taskRepo.Add(task);
    }

    public Task<int> Update(TaskUpdateDto taskUpdateDto, int id)
    {
        var task = _taskRepo.GetById(id);
        
        if (task.Result == null) return System.Threading.Tasks.Task.FromResult(0);
        
        if(taskUpdateDto.Title != null) task.Result.Title = taskUpdateDto.Title;
        if(taskUpdateDto.Description != null) task.Result.Description = taskUpdateDto.Description;
        if(taskUpdateDto.Priority != null) task.Result.Priority = taskUpdateDto.Priority;
        if(taskUpdateDto.Completed != null) task.Result.Completed = taskUpdateDto.Completed;
        if(taskUpdateDto.AssignedTo != null) task.Result.AssignedTo = taskUpdateDto.AssignedTo;
        task.Result.Date = DateTime.Now;
        if(taskUpdateDto.ProjectId != null) task.Result.ProjectId = taskUpdateDto.ProjectId;
        

        return _taskRepo.Update(task.Result);
    }

    public Task<int> Delete(int id)
    {
        var task = _taskRepo.GetById(id);
        if (task.Result != null) return _taskRepo.Delete(task.Result);
        return System.Threading.Tasks.Task.FromResult(0);
    }

    public Task<TaskReadDto>? Get(int id)
    {
        var task = _taskRepo.GetById(id);
        if (task.Result == null) return null;
        return System.Threading.Tasks.Task.FromResult(new TaskReadDto()
        {
            Id = task.Result.Id,
            Title = task.Result.Title,
            Description = task.Result.Description,
            Completed = task.Result.Completed,
            AssignedTo = task.Result.AssignedTo,
            Priority = task.Result.Priority,
            ProjectId = task.Result.ProjectId,
            Date = task.Result.Date
        });
    }

    public Task<List<TaskReadDto>> GetAll()
    {
        var task = _taskRepo.GetAll();
        return System.Threading.Tasks.Task.FromResult(task.Result.Select(note => new TaskReadDto()
        {
            Id = note.Id,
            Description = note.Description,
            Completed = note.Completed,
            AssignedTo = note.AssignedTo,
            ProjectId = note.ProjectId,
            Priority = note.Priority,
            Title = note.Title,
            Date = note.Date
        }).ToList());
    }
    
     
     public async Task<FilteredTaskDto> GetFilteredTasksAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _taskRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(paginatedResults);
            FilteredTaskDto result = new()
            {
                TaskDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Task> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(users, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedTask = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(paginatedResults);

            FilteredTaskDto filteredTaskDto = new()
            {
                TaskDto = mappedTask,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTaskDto;
        }

        return new FilteredTaskDto();
    }
    private IEnumerable<Task> ApplyFilter(IEnumerable<Task> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var taskValue) => ApplyNumericFilter(users, column, taskValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Task> ApplyNumericFilter(IEnumerable<Task> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var taskValue) && taskValue < value),
        _ => users
    };
}


    public Task<List<TaskDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Task> user;
            user = _taskRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var task = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(user);
            return System.Threading.Tasks.Task.FromResult(task.ToList());
        }

        var  users = _taskRepo.GlobalSearch(searchKey);
        var tasks = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(users);
        return System.Threading.Tasks.Task.FromResult(tasks.ToList());
    }

    public Task<List<TaskDto>> GetTaskWithProjectId(int projectId)
    {
        var users = _taskRepo.GetTaskWithProjectId(projectId);
        var tasks = _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDto>>(users);
        return System.Threading.Tasks.Task.FromResult(tasks.ToList());
    }
}
