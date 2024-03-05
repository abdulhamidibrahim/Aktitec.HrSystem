
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

    public void Update(TimesheetUpdateDto timesheetUpdateDto)
    {
        var timesheet = _timesheetRepo.GetById(timesheetUpdateDto.Id);
        
        if (timesheet.Result == null) return;
        timesheet.Result.Date = timesheetUpdateDto.Date;
        timesheet.Result.EmployeeId = timesheetUpdateDto.EmployeeId;
        timesheet.Result.ProjectId = timesheetUpdateDto.ProjectId;
        timesheet.Result.Deadline = timesheetUpdateDto.Deadline;
        timesheet.Result.AssignedHours = timesheetUpdateDto.AssignedHours;
        timesheet.Result.Hours = timesheetUpdateDto.Hours;
        timesheet.Result.Description = timesheetUpdateDto.Description;
        
        _timesheetRepo.Update(timesheet.Result);
    }

    public void Delete(TimesheetDeleteDto timesheetDeleteDto)
    {
        var timesheet = _timesheetRepo.GetById(timesheetDeleteDto.Id);
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
