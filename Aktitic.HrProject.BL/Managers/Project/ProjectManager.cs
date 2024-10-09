using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ProjectManager:IProjectManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProjectManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(ProjectAddDto projectAddDto)
    {
       
        var leader = _unitOfWork.Employee.GetById(projectAddDto.LeaderId);
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
            Team = projectAddDto.Team,
            CreatedAt = DateTime.Now,
        };
        var employees = new List<Employee?>();
        for (int i = 0; i < projectAddDto.Team?.Length; i++)
        {
            employees.Add(_unitOfWork.Employee.GetById(projectAddDto.Team[i])!);
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
        _unitOfWork.Project.Add(project);
        return _unitOfWork.SaveChangesAsync();
    }

public async Task<int> Update(ProjectUpdateDto projectUpdateDto, int id)
{
    var  project = _unitOfWork.Project.GetProjectWithEmployees(id).Result.FirstOrDefault(); // Use async version if available
    
    if (project == null) return 0;

    // LogNote project details
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

    if (projectUpdateDto?.LeaderId != null)
    {
        var leader =  _unitOfWork.Employee.GetById(projectUpdateDto.LeaderId); // Use async version if available
        if (leader != null) leader.TeamLeader = true;
        project.Leader = leader;
    }

    if (projectUpdateDto?.Team != null)
    {
        // Initialize EmployeesProject if null
        if (project.EmployeesProject == null)
        {
            project.EmployeesProject = new List<EmployeeProjects>();
        }
        else
        {
            // Clear existing team members
            project.EmployeesProject.Clear();
        }

        // Add updated team members
        foreach (var teamMemberId in projectUpdateDto.Team)
        {
            var employee =  _unitOfWork.Employee.GetById(teamMemberId); // Use async version if available
            if (employee != null)
            {
                project.EmployeesProject.Add(new EmployeeProjects()
                {
                    Project = project,
                    Employee = employee
                });
                
            }
        }
    }

    project.UpdatedAt = DateTime.Now;
    
    _unitOfWork.Project.Update(project);
    return await _unitOfWork.SaveChangesAsync();
}

    public Task<int> Delete(int id)
    {
        var project = _unitOfWork.Project.GetById(id);
        if (project==null) return Task.FromResult(0);
        project.IsDeleted = true;
        project.DeletedAt = DateTime.Now;
        _unitOfWork.Project.Update(project);
        return _unitOfWork.SaveChangesAsync();
    }

    public async Task<ProjectReadDto?> Get(int id)
    {
        var project = await _unitOfWork.Project.GetProjectWithEmployees(id);

        if (project == null) 
            throw new InvalidOperationException("Project not found.");

        var projectEntity = project.FirstOrDefault(); // Get the single project

        // Fetch team members
        var teamMembers = projectEntity?.EmployeesProject?.Select(ep => ep.Employee).ToList();

        // Fetch team leader
        var teamLeader = _unitOfWork.Employee.GetById(projectEntity?.LeaderId);

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
                LeaderId = teamLeader?.Id,
                TeamLeader = _mapper.Map<Employee, EmployeeDto>(teamLeader ?? throw new InvalidOperationException("Leader doesn't exist")), // Include the team leader object
                Team = teamMembers?.Select(e => _mapper.Map<Employee, EmployeeDto>(e)).ToList() // Include the list of team members
            };

            return projectDto;
        }
            
        return null;
    }



    public Task<List<ProjectReadDto>> GetAll()
    {
        var project = _unitOfWork.Project.GetAll();
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
            LeaderId = p.LeaderId,
            ProjectId = p.ProjectId,
            // Team = p.Team,
            // Team = p.Employees.Select(e => e.Id).ToArray(),
            
        }).ToList());
    }
    
    
     public  FilteredProjectDto GetFilteredProjectsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var projects =  _unitOfWork.Project.GetProjectWithEmployees().Result;
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = projects.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var projectList = projects.ToList();

            var paginatedResults = projectList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedProjects = new List<ProjectDto>();
            foreach (var project in paginatedResults)
            {
               
                // Fetch team leader
                var leader =  _unitOfWork.Employee.GetById(project.LeaderId);
                var leaderDto = _mapper.Map<Employee?, EmployeeDto>(leader);

                // Fetch team members
                var teamMembers = project.EmployeesProject?.Select(ep => ep.Employee).ToList();
                var teamDto = teamMembers?.Select(e => _mapper.Map<Employee, EmployeeDto>(e)).ToList(); // Include the list of team members

                var projectDto = new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ClientId = project.ClientId,
                    Priority = project.Priority,
                    RateSelect = project.RateSelect,
                    Rate = project.Rate,
                    Status = project.Status,
                    LeaderId = project.LeaderId,
                    ProjectId = project.ProjectId,
                    Leader = leaderDto,
                    TeamDto = teamDto
                };

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

        if (projects != null)
        {
            IEnumerable<Project> filteredResults =
                // Apply the first filter
                ApplyFilter(projects, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(projects, column, value2, operator2));
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
                var leader =  _unitOfWork.Employee.GetById(project.LeaderId);
                var leaderDto = _mapper.Map<Employee?, EmployeeDto>(leader);

                // Fetch team members
                var teamMembers = project.EmployeesProject?.Select(ep => ep.Employee).ToList();
                var teamDto = _mapper.Map<List<Employee>, List<EmployeeDto>>(teamMembers);

                var projectDto =  new ProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ClientId = project.ClientId,
                    Priority = project.Priority,
                    RateSelect = project.RateSelect,
                    Rate = project.Rate,
                    Status = project.Status,
                    LeaderId = project.LeaderId,
                    ProjectId = project.ProjectId,
                };
                projectDto.Leader = leaderDto;
                projectDto.TeamDto = teamDto;

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
    private IEnumerable<Project> ApplyFilter(IEnumerable<Project> projects, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => projects.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => projects.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => projects.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => projects.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var projectValue) => ApplyNumericFilter(projects, column, projectValue, operatorType),
            _ => projects
        };
    }

    private IEnumerable<Project> ApplyNumericFilter(IEnumerable<Project> projects, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => projects.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue == value),
        "neq" => projects.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue != value),
        "gte" => projects.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue >= value),
        "gt" => projects.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue > value),
        "lte" => projects.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue <= value),
        "lt" => projects.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue < value),
        _ => projects
    };
}


    public Task<List<ProjectDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Project> projects;
            projects = _unitOfWork.Project.GetProjectWithEmployees().Result
                .Where(e => e.GetPropertyValue(column).ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            
            var mappedProjects = new List<ProjectDto>();
            foreach (var project in projects)
            {
                // Fetch team leader
                var leader =  _unitOfWork.Employee.GetById(project.LeaderId);
                var leaderDto = _mapper.Map<Employee?, EmployeeDto>(leader);

                // Fetch team members
                var teamMembers = project.EmployeesProject?.Select(ep => ep.Employee).ToList();
                var teamDto = _mapper.Map<List<Employee>, List<EmployeeDto>>(teamMembers);

                var projectDto =  new ProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    ClientId = project.ClientId,
                    Priority = project.Priority,
                    RateSelect = project.RateSelect,
                    Rate = project.Rate,
                    Status = project.Status,
                    LeaderId = project.LeaderId,
                    ProjectId = project.ProjectId,
                };
                projectDto.Leader = leaderDto;
                projectDto.TeamDto = teamDto;

                mappedProjects.Add(projectDto);
            }
            return Task.FromResult(mappedProjects.ToList());
        }

        var  dtos = _unitOfWork.Project.GlobalSearch(searchKey);
        var mapped = new List<ProjectDto>();
        foreach (var project in dtos)
        {
            // Fetch team leader
            var leader =  _unitOfWork.Employee.GetById(project.LeaderId);
            var leaderDto = _mapper.Map<Employee?, EmployeeDto>(leader);

            // Fetch team members
            var teamMembers = project.EmployeesProject?.Select(ep => ep.Employee).ToList();
            var teamDto = _mapper.Map<List<Employee>, List<EmployeeDto>>(teamMembers);

            var projectDto =  new ProjectDto()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ClientId = project.ClientId,
                Priority = project.Priority,
                RateSelect = project.RateSelect,
                Rate = project.Rate,
                Status = project.Status,
                LeaderId = project.LeaderId,
                ProjectId = project.ProjectId,
            };
            projectDto.Leader = leaderDto;
            projectDto.TeamDto = teamDto;

            mapped.Add(projectDto);
        }
        return Task.FromResult(mapped.ToList());
    }


}
