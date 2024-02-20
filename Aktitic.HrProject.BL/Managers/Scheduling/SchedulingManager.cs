
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class SchedulingManager:ISchedulingManager
{
    private readonly ISchedulingRepo _schedulingRepo;

    public SchedulingManager(ISchedulingRepo schedulingRepo)
    {
        _schedulingRepo = schedulingRepo;
    }
    
    public void Add(SchedulingAddDto schedulingAddDto)
    {
        var scheduling = new Scheduling()
        {
            DepartmentId = schedulingAddDto.DepartmentId,
            EmployeeId = schedulingAddDto.EmployeeId,
            Date = schedulingAddDto.Date,
            ShiftId = schedulingAddDto.ShiftId,
            MinStartTime = schedulingAddDto.MinStartTime,
            MaxStartTime = schedulingAddDto.MaxStartTime,
            StartTime = schedulingAddDto.StartTime,
            MinEndTime = schedulingAddDto.MinEndTime,
            EndTime = schedulingAddDto.EndTime,
            MaxEndTime = schedulingAddDto.MaxEndTime,
            BreakTime = schedulingAddDto.BreakTime,
            RepeatEvery = schedulingAddDto.RepeatEvery,
            Note = schedulingAddDto.Note,
            Status = schedulingAddDto.Status,
            ApprovedBy = schedulingAddDto.ApprovedBy,
            
        };
        _schedulingRepo.Add(scheduling);
    }

    public void Update(SchedulingUpdateDto schedulingUpdateDto)
    {
        var scheduling = _schedulingRepo.GetById(schedulingUpdateDto.Id);
        
        if (scheduling.Result == null) return;
        scheduling.Result.DepartmentId = schedulingUpdateDto.DepartmentId;
        scheduling.Result.EmployeeId = schedulingUpdateDto.EmployeeId;
        scheduling.Result.Date = schedulingUpdateDto.Date;
        scheduling.Result.ShiftId = schedulingUpdateDto.ShiftId;
        scheduling.Result.MinStartTime = schedulingUpdateDto.MinStartTime;
        scheduling.Result.MaxStartTime = schedulingUpdateDto.MaxStartTime;
        scheduling.Result.StartTime = schedulingUpdateDto.StartTime;
        scheduling.Result.MinEndTime = schedulingUpdateDto.MinEndTime;
        scheduling.Result.EndTime = schedulingUpdateDto.EndTime;
        scheduling.Result.MaxEndTime = schedulingUpdateDto.MaxEndTime;
        scheduling.Result.BreakTime = schedulingUpdateDto.BreakTime;
        scheduling.Result.RepeatEvery = schedulingUpdateDto.RepeatEvery;
        scheduling.Result.Note = schedulingUpdateDto.Note;
        scheduling.Result.Status = schedulingUpdateDto.Status;
        scheduling.Result.ApprovedBy = schedulingUpdateDto.ApprovedBy;

        _schedulingRepo.Update(scheduling.Result);
    }

    public void Delete(SchedulingDeleteDto schedulingDeleteDto)
    {
        var scheduling = _schedulingRepo.GetById(schedulingDeleteDto.Id);
        if (scheduling.Result != null) _schedulingRepo.Delete(scheduling.Result);
    }

    public SchedulingReadDto? Get(int id)
    {
        var scheduling = _schedulingRepo.GetById(id);
        if (scheduling.Result == null) return null;
        return new SchedulingReadDto()
        {
            DepartmentId = scheduling.Result.DepartmentId,
            EmployeeId = scheduling.Result.EmployeeId,
            Date = scheduling.Result.Date,
            ShiftId = scheduling.Result.ShiftId,
            MinStartTime = scheduling.Result.MinStartTime,
            MaxStartTime = scheduling.Result.MaxStartTime,
            StartTime = scheduling.Result.StartTime,
            MinEndTime = scheduling.Result.MinEndTime,
            EndTime = scheduling.Result.EndTime,
            MaxEndTime = scheduling.Result.MaxEndTime,
            BreakTime = scheduling.Result.BreakTime,
            RepeatEvery = scheduling.Result.RepeatEvery,
            Note = scheduling.Result.Note,
            Status = scheduling.Result.Status,
            ApprovedBy = scheduling.Result.ApprovedBy,
            
        };
    }

    public List<SchedulingReadDto> GetAll()
    {
        var schedulings = _schedulingRepo.GetAll();
        return schedulings.Result.Select(scheduling => new SchedulingReadDto()
        {
            DepartmentId = scheduling.DepartmentId,
            EmployeeId = scheduling.EmployeeId,
            Date = scheduling.Date,
            ShiftId = scheduling.ShiftId,
            MinStartTime = scheduling.MinStartTime,
            MaxStartTime = scheduling.MaxStartTime,
            StartTime = scheduling.StartTime,
            MinEndTime = scheduling.MinEndTime,
            EndTime = scheduling.EndTime,
            MaxEndTime = scheduling.MaxEndTime,
            BreakTime = scheduling.BreakTime,
            RepeatEvery = scheduling.RepeatEvery,
            Note = scheduling.Note,
            Status = scheduling.Status,
            ApprovedBy = scheduling.ApprovedBy,
            
        }).ToList();
    }
}
