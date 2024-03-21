
using System.Diagnostics;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class TimesheetManager:ITimesheetManager
{
    private readonly ITimesheetRepo _timesheetRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IProjectRepo _projectRepo;
    private readonly IMapper _mapper;
    public TimesheetManager(ITimesheetRepo timesheetRepo, IMapper mapper, IEmployeeRepo employeeRepo, IProjectRepo projectRepo)
    {
        _timesheetRepo = timesheetRepo;
        _mapper = mapper;
        _employeeRepo = employeeRepo;
        _projectRepo = projectRepo;
    }
    
    public async Task<int> Add(TimesheetAddDto timesheetAddDto)
    {
        var timesheet = new TimeSheet()
        {
            Date = timesheetAddDto.Date,
            EmployeeId = timesheetAddDto.EmployeeId,
            ProjectId = timesheetAddDto.ProjectId,
            Deadline = timesheetAddDto.Deadline,
            AssignedHours = timesheetAddDto.AssignedHours,
            Hours = timesheetAddDto.Hours,
            Description = timesheetAddDto.Description,
            
        };
       return await _timesheetRepo.Add(timesheet);
    }

    public async Task<int> Update(TimesheetUpdateDto timesheetUpdateDto,int id)
    {
        var timesheet =await _timesheetRepo.GetById(id);

        if (timesheet == null) return 0;
        if(timesheetUpdateDto.Date != null) 
            timesheet.Date = timesheetUpdateDto.Date;
        if(timesheetUpdateDto.EmployeeId != null) 
            timesheet.EmployeeId = timesheetUpdateDto.EmployeeId;
        if(timesheetUpdateDto.ProjectId != null) 
            timesheet.ProjectId = timesheetUpdateDto.ProjectId;
        if(timesheetUpdateDto.Deadline != null) 
            timesheet.Deadline = timesheetUpdateDto.Deadline;
        if(timesheetUpdateDto.AssignedHours != null) 
            timesheet.AssignedHours = timesheetUpdateDto.AssignedHours;
        if(timesheetUpdateDto.Hours != null) 
            timesheet.Hours = timesheetUpdateDto.Hours;
        if(timesheetUpdateDto.Description != null) 
            timesheet.Description = timesheetUpdateDto.Description;
        
        return await _timesheetRepo.Update(timesheet);
    }

    public Task<int> Delete(int id)
    {
        var timesheet = _timesheetRepo.GetById(id);
        if (timesheet.Result != null) return _timesheetRepo.Delete(timesheet.Result);
        return Task.FromResult(0);
    }

    public async Task<TimesheetReadDto?> Get(int id)
    {
        var timesheet = await _timesheetRepo.GetById(id);
        if (timesheet == null) return new TimesheetReadDto();

        var employee = await _employeeRepo.GetById(timesheet.EmployeeId);
        var project = await _projectRepo.GetById(timesheet.ProjectId);

        // Check if employee or project is null
        // if (employee == null || project == null)
        // {
        //     // Handle the case where either employee or project is null
        //     // For example, you could return null or throw an exception
        //     return null; // or throw new Exception("Employee or Project not found");
        // }

        // Construct the TimesheetReadDto object
        var result = new TimesheetReadDto()
        {
            Id = timesheet.Id,
            Date = timesheet.Date,
            EmployeeId = timesheet.EmployeeId,
            ProjectId = timesheet.ProjectId,
            Deadline = timesheet.Deadline,
            AssignedHours = timesheet.AssignedHours,
            Hours = timesheet.Hours,
            Description = timesheet.Description,
            
        };
        if(result == null) return new TimesheetReadDto();
        return result;
    }


    public async Task<List<TimesheetReadDto>> GetAll()
    {
        var timesheets =await _timesheetRepo.GetAll();
        return timesheets.Select(timesheet => new TimesheetReadDto()
        {
            Id = timesheet.Id,
            Date = timesheet.Date,
            EmployeeId = timesheet.EmployeeId,
            ProjectId = timesheet.ProjectId,
            Deadline = timesheet.Deadline,
            AssignedHours = timesheet.AssignedHours,
            Hours = timesheet.Hours,
            Description = timesheet.Description,
            
            
        }).ToList();
    }
    
      public async Task<FilteredTimeSheetDto> GetFilteredTimeSheetsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _timesheetRepo.GetTimeSheetWithEmployeeAndProject();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            List<TimeSheetDto> timeSheet = new();
            foreach (var designation in paginatedResults)
            {
                timeSheet.Add(new TimeSheetDto()
                { 
                    Description= designation.Description,
                    Id = designation.Id,
                     IdNavigation = _mapper.Map<Employee, EmployeeDto>(designation.Employee)
                });
            }

            FilteredTimeSheetDto filteredTimeSheetDto = new()
            {
                TimeSheetDto = timeSheet,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredTimeSheetDto;
        }

        if (users != null)
        {
            IEnumerable<TimeSheet> filteredResults;
        
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

            List<TimeSheetDto> timeSheet = new();
            foreach (var designation in paginatedResults)
            {
                timeSheet.Add(new TimeSheetDto()
                { 
                    Description= designation.Description,
                    Id = designation.Id,
                    IdNavigation = _mapper.Map<Employee, EmployeeDto>(designation.Employee)
                });
            }

            FilteredTimeSheetDto filteredTimeSheetDto = new()
            {
                TimeSheetDto = timeSheet,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTimeSheetDto;
        }

        return new FilteredTimeSheetDto();
    }
    private IEnumerable<TimeSheet> ApplyFilter(IEnumerable<TimeSheet> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var timesheetValue) => ApplyNumericFilter(users, column, timesheetValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<TimeSheet> ApplyNumericFilter(IEnumerable<TimeSheet> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue < value),
        _ => users
    };
}


    public Task<List<TimeSheetDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<TimeSheet> user;
            user = _timesheetRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var timesheet = _mapper.Map<IEnumerable<TimeSheet>, IEnumerable<TimeSheetDto>>(user);
            return Task.FromResult(timesheet.ToList());
        }

        var  users = _timesheetRepo.GlobalSearch(searchKey);
        var timesheets = _mapper.Map<IEnumerable<TimeSheet>, IEnumerable<TimeSheetDto>>(users);
        return Task.FromResult(timesheets.ToList());
    }

}
