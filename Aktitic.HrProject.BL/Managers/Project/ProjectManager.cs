
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ProjectManager:IProjectManager
{
    private readonly IProjectRepo _projectRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeProjectsRepo _employeeProjectsRepo;
    private readonly IMapper _mapper;
    public ProjectManager(IProjectRepo projectRepo, IMapper mapper, IEmployeeRepo employeeRepo, IEmployeeProjectsRepo employeeProjectsRepo, IUnitOfWork unitOfWork)
    {
        _projectRepo = projectRepo;
        _mapper = mapper;
        _employeeRepo = employeeRepo;
        _employeeProjectsRepo = employeeProjectsRepo;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(ProjectAddDto projectAddDto)
    {
        // foreach (var project2 in _projectRepo.GetAll().Result)
        // {
        //     if (project2.Name == projectAddDto.Name)
        //     {
        //         return Task.FromResult(0);
        //     }
        // }
        var leader = _employeeRepo.GetById(projectAddDto.LeaderId);
        if (leader != null) leader.TeamLeader = true;
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
            ProjectId = projectAddDto.ProjectId,
            // Checked = projectAddDto.Checked,
            LeaderId = projectAddDto.LeaderId,
            Leader = leader,
            Team = projectAddDto.Team
        };
        var employees = new List<Employee>();
        for (int i = 0; i < projectAddDto.Team?.Length; i++)
        {
            employees.Add(_employeeRepo.GetById(projectAddDto.Team[i])!);
        }

        var employeeProjects = new List<EmployeeProjects>();
        foreach (var employee in employees)
        {
            employeeProjects.Add( new EmployeeProjects()
            {
                Project = project,
                Employee = employee
            });
        }
        
        project.EmployeesProject = employeeProjects;
        _projectRepo.Add(project);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ProjectUpdateDto projectUpdateDto, int id)
    {
        var projectTask = _projectRepo.GetById(id);

        if (projectTask == null) return Task.FromResult(0);

        var project = projectTask;

        // Update project details
        
        if (projectUpdateDto?.Name != null) project.Name = projectUpdateDto.Name;
        if (projectUpdateDto?.Description != null) project.Description = projectUpdateDto.Description;
        if (projectUpdateDto?.StartDate != null) project.StartDate = projectUpdateDto.StartDate;
        if (projectUpdateDto?.EndDate != null) project.EndDate = projectUpdateDto.EndDate;
        if (projectUpdateDto?.ClientId != null) project.ClientId = projectUpdateDto.ClientId;
        if (projectUpdateDto?.Priority != null) project.Priority = projectUpdateDto.Priority;
        if (projectUpdateDto?.RateSelect != null) project.RateSelect = projectUpdateDto.RateSelect;
        if (projectUpdateDto?.Rate != null) project.Rate = projectUpdateDto.Rate;
        if (projectUpdateDto?.Status != null) project.Status = projectUpdateDto.Status;
        if (projectUpdateDto?.ProjectId != null) project.ProjectId = projectUpdateDto.ProjectId;
        // if (projectUpdateDto?.Checked != null) project.Checked = projectUpdateDto.Checked;
        if (projectUpdateDto?.LeaderId != null) project.LeaderId = projectUpdateDto.LeaderId;
        if (projectUpdateDto?.Team != null) project.Team = projectUpdateDto.Team;
        
        if (projectUpdateDto?.LeaderId != null)
        {
            var leader = _employeeRepo.GetById(projectUpdateDto.LeaderId);
            if (leader != null) leader.TeamLeader = true;
            project.Leader = leader;
        }
        // Update team members
        if (projectUpdateDto?.Team != null)
        {
            // Clear existing team members
            project.EmployeesProject?.Clear();

            // Add updated team members
            for (int i = 0; i < projectUpdateDto.Team.Length; i++)
            {
                var employee = _employeeRepo.GetById(projectUpdateDto.Team[i]);
                if (employee != null)
                {
                    project.EmployeesProject?.Add(new EmployeeProjects()
                    {
                        Project = project,
                        Employee = employee
                    });
                }
            }
        }
        _projectRepo.Update(project);
        return _unitOfWork.SaveChangesAsync();
    }


    public Task<int> Delete(int id)
    {

        _projectRepo.GetById(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public async Task<ProjectReadDto?> Get(int id)
    {
        var project = await _projectRepo.GetProjectWithEmployees(id);

        if (project == null) 
            throw new InvalidOperationException("Project not found.");

        var projectEntity = project.FirstOrDefault(); // Get the single project

        // Fetch team members
        var teamMembers = projectEntity?.EmployeesProject?.Select(ep => ep.Employee).ToList();

        // Fetch team leader
        var teamLeader = _employeeRepo.GetById(projectEntity?.LeaderId);

        // ProjectReadDto with project details, team members, and team leader
        if (projectEntity != null)
        {
            var projectDto = new ProjectReadDto()
            {
                Id = projectEntity.Id,
                Name = projectEntity.Name,
                Description = projectEntity.Description,
                StartDate = projectEntity.StartDate,
                EndDate = projectEntity.EndDate,
                ClientId = projectEntity.ClientId,
                Priority = projectEntity.Priority,
                RateSelect = projectEntity.RateSelect,
                Rate = projectEntity.Rate,
                Status = projectEntity.Status,
                ProjectId = projectEntity.ProjectId,
                // Checked = projectEntity.Checked,
                LeaderId = teamLeader?.Id,
                TeamLeader = _mapper.Map<Employee, EmployeeDto>(teamLeader ?? throw new InvalidOperationException("Leader doesn't exist")), // Include the team leader object
                Team = teamMembers?.Select(e => _mapper.Map<Employee, EmployeeDto>(e)).ToList() // Include the list of team members
            };

            return projectDto;
        }
            
        return new ProjectReadDto();
    }



    public Task<List<ProjectReadDto>> GetAll()
    {
        var project = _projectRepo.GetAll();
        return Task.FromResult(project.Result.Select(p => new ProjectReadDto()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            ClientId = p.ClientId,
            Priority = p.Priority,
            RateSelect = p.RateSelect,
            Rate = p.Rate,
            Status = p.Status,
            // Checked = p.Checked,
            LeaderId = p.LeaderId,
            ProjectId = p.ProjectId,
            // Team = p.Team,
            // Team = p.Employees.Select(e => e.Id).ToArray(),
            
        }).ToList());
    }
    
    
     public async Task<FilteredProjectDto> GetFilteredProjectsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _projectRepo.GetProjectWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedProjects = new List<ProjectDto>();
            foreach (var project in paginatedResults)
            {
                // Fetch team leader
                var leader =  _employeeRepo.GetById(project.LeaderId);
                var leaderDto = _mapper.Map<Employee?, EmployeeDto>(leader);

                // Fetch team members
                var teamMembers = project.EmployeesProject?.Select(ep => ep.Employee).ToList();
                var teamDto = teamMembers?.Select(e => _mapper.Map<Employee, EmployeeDto>(e)).ToList(); // Include the list of team members

                var projectDto = _mapper.Map<Project, ProjectDto>(project);
                projectDto.Leader = leaderDto;
                projectDto.Team = teamDto;

                mappedProjects.Add(projectDto);
            }
            FilteredProjectDto filteredProjectDto = new()
            {
                ProjectDto = mappedProjects,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredProjectDto;
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

            // var projects = paginatedResults.ToList();
            // var mappedProject = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(projects);
            
            var mappedProjects = new List<ProjectDto>();

            foreach (var project in paginatedResults)
            {
                // Fetch team leader
                var leader =  _employeeRepo.GetById(project.LeaderId);
                var leaderDto = _mapper.Map<Employee?, EmployeeDto>(leader);

                // Fetch team members
                var teamMembers = project.EmployeesProject?.Select(ep => ep.Employee).ToList();
                var teamDto = _mapper.Map<List<Employee>, List<EmployeeDto>>(teamMembers);

                var projectDto = _mapper.Map<Project, ProjectDto>(project);
                projectDto.Leader = leaderDto;
                projectDto.Team = teamDto;

                mappedProjects.Add(projectDto);
            }
            FilteredProjectDto filteredProjectDto = new()
            {
                ProjectDto = mappedProjects,
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
