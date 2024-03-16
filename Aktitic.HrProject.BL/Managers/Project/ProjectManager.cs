
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ProjectManager:IProjectManager
{
    private readonly IProjectRepo _projectRepo;
    private readonly IMapper _mapper;
    public ProjectManager(IProjectRepo projectRepo, IMapper mapper)
    {
        _projectRepo = projectRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(ProjectAddDto projectAddDto)
    {
        var project = new Project()
        {
            Name = projectAddDto.Name,
            Description = projectAddDto.Description,
            StartDate = projectAddDto.StartDate,
            EndDate = projectAddDto.EndDate,
            ClientId = projectAddDto.ClientId,
            Priority = projectAddDto.Priority,
            RateSelect = projectAddDto.RateSelect,
            Rate = projectAddDto.Rate,
            Status = projectAddDto.Status,
            Checked = projectAddDto.Checked
            
        };
        return _projectRepo.Add(project);
    }

    public Task<int> Update(ProjectUpdateDto projectUpdateDto, int id)
    {
        var project = _projectRepo.GetById(id);
        
        if (project.Result == null) return Task.FromResult(0);
        
        if(projectUpdateDto.Name != null) project.Result.Name = projectUpdateDto.Name;
        if(projectUpdateDto.Description != null) project.Result.Description = projectUpdateDto.Description;
        if(projectUpdateDto.StartDate != null) project.Result.StartDate = projectUpdateDto.StartDate;
        if(projectUpdateDto.EndDate != null) project.Result.EndDate = projectUpdateDto.EndDate;
        if(projectUpdateDto.ClientId != null) project.Result.ClientId = projectUpdateDto.ClientId;
        if(projectUpdateDto.Priority != null) project.Result.Priority = projectUpdateDto.Priority;
        if(projectUpdateDto.RateSelect != null) project.Result.RateSelect = projectUpdateDto.RateSelect;
        if(projectUpdateDto.Rate != null) project.Result.Rate = projectUpdateDto.Rate;
        if(projectUpdateDto.Status != null) project.Result.Status = projectUpdateDto.Status;
        if(projectUpdateDto.Checked != null) project.Result.Checked = projectUpdateDto.Checked;

        return _projectRepo.Update(project.Result);
    }

    public Task<int> Delete(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project.Result != null) return _projectRepo.Delete(project.Result);
        return Task.FromResult(0);
    }

    public Task<ProjectReadDto> Get(int id)
    {
        var project = _projectRepo.GetById(id);
        if (project.Result == null) return Task.FromResult(new ProjectReadDto());
        return Task.FromResult(new ProjectReadDto()
        {
            Id = project.Result.Id,
            Name = project.Result.Name,
            Description = project.Result.Description,
            StartDate = project.Result.StartDate,
            EndDate = project.Result.EndDate,
            ClientId = project.Result.ClientId,
            Priority = project.Result.Priority,
            RateSelect = project.Result.RateSelect,
            Rate = project.Result.Rate,
            Status = project.Result.Status,
            Checked = project.Result.Checked
            
        });
    }

    public Task<List<ProjectReadDto>> GetAll()
    {
        var project = _projectRepo.GetAll();
        return Task.FromResult(project.Result.Select(note => new ProjectReadDto()
        {
            Id = note.Id,
            Name = note.Name,
            Description = note.Description,
            StartDate = note.StartDate,
            EndDate = note.EndDate,
            ClientId = note.ClientId,
            Priority = note.Priority,
            RateSelect = note.RateSelect,
            Rate = note.Rate,
            Status = note.Status,
            Checked = note.Checked
            
        }).ToList());
    }
    
    
     public async Task<FilteredProjectDto> GetFilteredProjectsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _projectRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(paginatedResults);
            FilteredProjectDto result = new()
            {
                ProjectDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Project> filteredResults;
        
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

            var mappedProject = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(paginatedResults);

            FilteredProjectDto filteredProjectDto = new()
            {
                ProjectDto = mappedProject,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredProjectDto;
        }

        return new FilteredProjectDto();
    }
    private IEnumerable<Project> ApplyFilter(IEnumerable<Project> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var projectValue) => ApplyNumericFilter(users, column, projectValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Project> ApplyNumericFilter(IEnumerable<Project> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue < value),
        _ => users
    };
}


    public Task<List<ProjectDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Project> user;
            user = _projectRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var project = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(user);
            return Task.FromResult(project.ToList());
        }

        var  users = _projectRepo.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(users);
        return Task.FromResult(projects.ToList());
    }


}
