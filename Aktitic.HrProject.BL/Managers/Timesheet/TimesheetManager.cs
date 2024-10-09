using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class TimesheetManager:ITimesheetManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public TimesheetManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Task<int>> Add(TimesheetAddDto timesheetAddDto)
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
            CreatedAt = DateTime.Now,
            
        };
        _unitOfWork.TimeSheet.Add(timesheet);
        return _unitOfWork.SaveChangesAsync();
    }

    public async Task<Task<int>> Update(TimesheetUpdateDto timesheetUpdateDto, int id)
    {
        var timesheet = _unitOfWork.TimeSheet.GetById(id);

        if (timesheet == null) return Task.FromResult(0);
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
        
        
        timesheet.UpdatedAt = DateTime.Now;
        _unitOfWork.TimeSheet.Update(timesheet);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var timeSheet = _unitOfWork.TimeSheet.GetById(id);
        if (timeSheet==null) return Task.FromResult(0);
        timeSheet.IsDeleted = true;
        timeSheet.DeletedAt = DateTime.Now;
        _unitOfWork.TimeSheet.Update(timeSheet);
        return _unitOfWork.SaveChangesAsync();
    }

    public async Task<TimesheetReadDto?> Get(int id)
    {
        var timesheet = _unitOfWork.TimeSheet.GetById(id);
        if (timesheet == null) return new TimesheetReadDto();

        var employee =  _unitOfWork.Employee.GetById(timesheet.EmployeeId);
        var project =  _unitOfWork.Project.GetById(timesheet.ProjectId);

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
        var timesheets =await _unitOfWork.TimeSheet.GetAll();
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
        var timesheets = await _unitOfWork.TimeSheet.GetTimeSheetWithEmployeeAndProject();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = timesheets.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var timesheetList = timesheets.ToList();

            var paginatedResults = timesheetList.Skip((page - 1) * pageSize).Take(pageSize);

            List<TimeSheetDto> timeSheet = new();
            foreach (var designation in paginatedResults)
            {
                timeSheet.Add(new TimeSheetDto()
                {   
                     Description= designation.Description,
                     Id = designation.Id,
                     Date = designation.Date,
                     ProjectId = designation.ProjectId,
                     AssignedHours = designation.AssignedHours,
                     Deadline = designation.Deadline,
                     Hours = designation.Hours,
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

        if (timesheets != null)
        {
            IEnumerable<TimeSheet> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(timesheets, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(timesheets, column, value2, operator2));
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
                    Date = designation.Date,
                    ProjectId = designation.ProjectId,
                    AssignedHours = designation.AssignedHours,
                    Deadline = designation.Deadline,
                    Hours = designation.Hours,
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
    private IEnumerable<TimeSheet> ApplyFilter(IEnumerable<TimeSheet> timesheets, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => timesheets.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => timesheets.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => timesheets.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => timesheets.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var timesheetValue) => ApplyNumericFilter(timesheets, column, timesheetValue, operatorType),
            _ => timesheets
        };
    }

    private IEnumerable<TimeSheet> ApplyNumericFilter(IEnumerable<TimeSheet> timesheets, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => timesheets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue == value),
        "neq" => timesheets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue != value),
        "gte" => timesheets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue >= value),
        "gt" => timesheets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue > value),
        "lte" => timesheets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue <= value),
        "lt" => timesheets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var timesheetValue) && timesheetValue < value),
        _ => timesheets
    };
}


    public Task<List<TimeSheetDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<TimeSheet> timesheetDto;
            timesheetDto = _unitOfWork.TimeSheet.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var timesheet = _mapper.Map<IEnumerable<TimeSheet>, IEnumerable<TimeSheetDto>>(timesheetDto);
            return Task.FromResult(timesheet.ToList());
        }

        var  timesheetDtos = _unitOfWork.TimeSheet.GlobalSearch(searchKey);
        var timesheets = _mapper.Map<IEnumerable<TimeSheet>, IEnumerable<TimeSheetDto>>(timesheetDtos);
        return Task.FromResult(timesheets.ToList());
    }

}
