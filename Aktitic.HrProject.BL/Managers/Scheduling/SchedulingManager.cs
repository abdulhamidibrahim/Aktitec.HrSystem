
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class SchedulingManager:ISchedulingManager
{
    private readonly ISchedulingRepo _schedulingRepo;
    private readonly IMapper _mapper;
    public SchedulingManager(ISchedulingRepo schedulingRepo, IMapper mapper)
    {
        _schedulingRepo = schedulingRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(SchedulingAddDto schedulingAddDto)
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
        return _schedulingRepo.Add(scheduling);
    }

    public Task<int> Update(SchedulingUpdateDto schedulingUpdateDto,int id)
    {
        var scheduling = _schedulingRepo.GetById(id);
        
        if (scheduling.Result == null) return Task.FromResult(0);
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

        return _schedulingRepo.Update(scheduling.Result);
    }

    public Task<int> Delete(int id)
    {
        var scheduling = _schedulingRepo.GetById(id);
        if (scheduling.Result != null) return _schedulingRepo.Delete(scheduling.Result);
        return Task.FromResult(0);
    }

    public async Task<SchedulingReadDto>? Get(int id)
    {
        var scheduling =await _schedulingRepo.GetById(id);
        if (scheduling == null) return null;
        return new SchedulingReadDto()
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
            
        };
    }

    public async Task<List<SchedulingReadDto>> GetAll()
    {
        var schedulings = await _schedulingRepo.GetAll();
        return schedulings.Select(scheduling => new SchedulingReadDto()
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

    public Task<List<FilteredSchedulingDto>> GetAllEmployeesScheduling(int page, int pageSize)
    {
       var employee  =  _schedulingRepo.GetSchedulingWithEmployees();
        var employeesMap = _mapper.Map<List<Scheduling>, List<ScheduleDto>>(employee);
        var totalCount = employeesMap.Count;
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var employees = employeesMap.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var readDto = Task.FromResult(employees.Select(x => new SchedulingReadDto
        {
            Id = x.Id,
            DepartmentId = x.DepartmentId,
            EmployeeId = x.EmployeeId,
            Date = x.Date,
            ShiftId = x.ShiftId,
            MinStartTime = x.MinStartTime,
            MaxStartTime = x.MaxStartTime,
            StartTime = x.StartTime,
            MinEndTime = x.MinEndTime,
            EndTime = x.EndTime,
            MaxEndTime = x.MaxEndTime,
            BreakTime = x.BreakTime,
            RepeatEvery = x.RepeatEvery,
            Note = x.Note,
            Status = x.Status,
            ApprovedBy = x.ApprovedBy,
            Employee = x.Employee
        }).ToList());
        return new Task<List<FilteredSchedulingDto>>(() => new List<FilteredSchedulingDto>
        {
            new FilteredSchedulingDto
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                SchedulingReadDto = readDto.Result,
            }
        });
        {
            
        }
    }
    
    public Task<List<ScheduleDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Scheduling> user;
            user = _schedulingRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var Schedule = _mapper.Map<IEnumerable<Scheduling>, IEnumerable<ScheduleDto>>(user);
            return Task.FromResult(Schedule.ToList());
        }

        var  users = _schedulingRepo.GlobalSearch(searchKey);
        var shifts = _mapper.Map<IEnumerable<Scheduling>, IEnumerable<ScheduleDto>>(users);
        return Task.FromResult(shifts.ToList());
    }

}
