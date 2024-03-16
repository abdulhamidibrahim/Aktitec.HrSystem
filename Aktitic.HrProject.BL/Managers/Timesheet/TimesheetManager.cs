
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class TimesheetManager:ITimesheetManager
{
    private readonly ITimesheetRepo _timesheetRepo;

    public TimesheetManager(ITimesheetRepo timesheetRepo)
    {
        _timesheetRepo = timesheetRepo;
    }
    
    public void Add(TimesheetAddDto timesheetAddDto)
    {
        var timesheet = new Timesheet()
        {
            Date = timesheetAddDto.Date,
            EmployeeId = timesheetAddDto.EmployeeId,
            ProjectId = timesheetAddDto.ProjectId,
            Deadline = timesheetAddDto.Deadline,
            AssignedHours = timesheetAddDto.AssignedHours,
            Hours = timesheetAddDto.Hours,
            Description = timesheetAddDto.Description,
            
        };
        _timesheetRepo.Add(timesheet);
    }

    public void Update(TimesheetUpdateDto timesheetUpdateDto,int id)
    {
        var timesheet = _timesheetRepo.GetById(id);
        
        if (timesheet.Result == null) return;
        if(timesheetUpdateDto.Date != null) 
            timesheet.Result.Date = timesheetUpdateDto.Date;
        if(timesheetUpdateDto.EmployeeId != null) 
            timesheet.Result.EmployeeId = timesheetUpdateDto.EmployeeId;
        if(timesheetUpdateDto.ProjectId != null) 
            timesheet.Result.ProjectId = timesheetUpdateDto.ProjectId;
        if(timesheetUpdateDto.Deadline != null) 
            timesheet.Result.Deadline = timesheetUpdateDto.Deadline;
        if(timesheetUpdateDto.AssignedHours != null) 
            timesheet.Result.AssignedHours = timesheetUpdateDto.AssignedHours;
        if(timesheetUpdateDto.Hours != null) 
            timesheet.Result.Hours = timesheetUpdateDto.Hours;
        if(timesheetUpdateDto.Description != null) 
            timesheet.Result.Description = timesheetUpdateDto.Description;
        
        _timesheetRepo.Update(timesheet.Result);
    }

    public void Delete(int id)
    {
        var timesheet = _timesheetRepo.GetById(id);
        if (timesheet.Result != null) _timesheetRepo.Delete(timesheet.Result);
    }

    public TimesheetReadDto? Get(int id)
    {
        var timesheet = _timesheetRepo.GetById(id);
        if (timesheet.Result == null) return null;
        return new TimesheetReadDto()
        {
            Id = timesheet.Result.Id,
            Date = timesheet.Result.Date,
            EmployeeId = timesheet.Result.EmployeeId,
            ProjectId = timesheet.Result.ProjectId,
            Deadline = timesheet.Result.Deadline,
            AssignedHours = timesheet.Result.AssignedHours,
            Hours = timesheet.Result.Hours,
            Description = timesheet.Result.Description,
            
        };
    }

    public List<TimesheetReadDto> GetAll()
    {
        var timesheets = _timesheetRepo.GetAll();
        return timesheets.Result.Select(timesheet => new TimesheetReadDto()
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
}
