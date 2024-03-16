
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

    public void Update(SchedulingUpdateDto schedulingUpdateDto,int id)
    {
        var scheduling = _schedulingRepo.GetById(id);
        
        if (scheduling.Result == null) return;
        if(schedulingUpdateDto.DepartmentId != null) scheduling.Result.DepartmentId = schedulingUpdateDto.DepartmentId;
        if(schedulingUpdateDto.EmployeeId != null) scheduling.Result.EmployeeId = schedulingUpdateDto.EmployeeId;
        if(schedulingUpdateDto.Date != null) scheduling.Result.Date = schedulingUpdateDto.Date;
        if(schedulingUpdateDto.ShiftId != null) scheduling.Result.ShiftId = schedulingUpdateDto.ShiftId;
        if(schedulingUpdateDto.MinStartTime != null) scheduling.Result.MinStartTime = schedulingUpdateDto.MinStartTime;
        if(schedulingUpdateDto.MaxStartTime != null) scheduling.Result.MaxStartTime = schedulingUpdateDto.MaxStartTime;
        if(schedulingUpdateDto.StartTime != null) scheduling.Result.StartTime = schedulingUpdateDto.StartTime;
        if(schedulingUpdateDto.MaxEndTime != null) scheduling.Result.MinEndTime = schedulingUpdateDto.MinEndTime;
        if(schedulingUpdateDto.EndTime != null) scheduling.Result.EndTime = schedulingUpdateDto.EndTime;
        if(schedulingUpdateDto.MaxEndTime != null) scheduling.Result.MaxEndTime = schedulingUpdateDto.MaxEndTime;
        if(schedulingUpdateDto.BreakTime != null) scheduling.Result.BreakTime = schedulingUpdateDto.BreakTime;
        if(schedulingUpdateDto.RepeatEvery != null) scheduling.Result.RepeatEvery = schedulingUpdateDto.RepeatEvery;
        if(schedulingUpdateDto.Note != null) scheduling.Result.Note = schedulingUpdateDto.Note;
        if(schedulingUpdateDto.Status != null) scheduling.Result.Status = schedulingUpdateDto.Status;
        if(schedulingUpdateDto.ApprovedBy != null) scheduling.Result.ApprovedBy = schedulingUpdateDto.ApprovedBy;

        _schedulingRepo.Update(scheduling.Result);
    }

    public void Delete(int id)
    {
        var scheduling = _schedulingRepo.GetById(id);
        if (scheduling.Result != null) _schedulingRepo.Delete(scheduling.Result);
    }

    public SchedulingReadDto? Get(int id)
    {
        var scheduling = _schedulingRepo.GetById(id);
        if (scheduling.Result == null) return null;
        return new SchedulingReadDto()
        {
            Id = scheduling.Result.Id,
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
            Id = scheduling.Id,
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
