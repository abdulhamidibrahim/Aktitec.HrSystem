
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class SchedulingManager:ISchedulingManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public SchedulingManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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
            Publish = schedulingAddDto.Publish,
            ExtraHours = schedulingAddDto.ExtraHours,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Scheduling.Add(scheduling);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(SchedulingUpdateDto schedulingUpdateDto, int id)
    {
        var scheduling = _unitOfWork.Scheduling.GetById(id);

        if (scheduling == null) return Task.FromResult(0);
        if(schedulingUpdateDto.DepartmentId != null) scheduling.DepartmentId = schedulingUpdateDto.DepartmentId;
        if(schedulingUpdateDto.EmployeeId != null) scheduling.EmployeeId = schedulingUpdateDto.EmployeeId;
        if(schedulingUpdateDto.Date != null) scheduling.Date = schedulingUpdateDto.Date;
        if(schedulingUpdateDto.ShiftId != null) scheduling.ShiftId = schedulingUpdateDto.ShiftId;
        if(schedulingUpdateDto.MinStartTime != null) scheduling.MinStartTime = schedulingUpdateDto.MinStartTime;
        if(schedulingUpdateDto.MaxStartTime != null) scheduling.MaxStartTime = schedulingUpdateDto.MaxStartTime;
        if(schedulingUpdateDto.StartTime != null) scheduling.StartTime = schedulingUpdateDto.StartTime;
        if(schedulingUpdateDto.MaxEndTime != null) scheduling.MinEndTime = schedulingUpdateDto.MinEndTime;
        if(schedulingUpdateDto.EndTime != null) scheduling.EndTime = schedulingUpdateDto.EndTime;
        if(schedulingUpdateDto.MaxEndTime != null) scheduling.MaxEndTime = schedulingUpdateDto.MaxEndTime;
        if(schedulingUpdateDto.BreakTime != null) scheduling.BreakTime = schedulingUpdateDto.BreakTime;
        if(schedulingUpdateDto.RepeatEvery != null) scheduling.RepeatEvery = schedulingUpdateDto.RepeatEvery;
        if(schedulingUpdateDto.ExtraHours != null) scheduling.ExtraHours = schedulingUpdateDto.ExtraHours;
        if(schedulingUpdateDto.Publish != null) scheduling.Publish = schedulingUpdateDto.Publish;
        if(schedulingUpdateDto.RepeatEvery != null) scheduling.RepeatEvery = schedulingUpdateDto.RepeatEvery;

         _unitOfWork.Scheduling.Update(scheduling);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var scheduling = _unitOfWork.Scheduling.GetById(id);
        if (scheduling==null) return Task.FromResult(0);
        scheduling.IsDeleted = true;
        scheduling.DeletedAt = DateTime.Now;
        _unitOfWork.Scheduling.Update(scheduling);
        return _unitOfWork.SaveChangesAsync();
    }

    public  SchedulingReadDto? Get(int id)
    {
        var scheduling = _unitOfWork.Scheduling.GetById(id);
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
            ExtraHours = scheduling.ExtraHours,
            Publish = scheduling.Publish,
            
        };
    }

    public async Task<List<SchedulingReadDto>> GetAll()
    {
        var schedulings = await _unitOfWork.Scheduling.GetAll();
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
            ExtraHours = scheduling.ExtraHours,
            Publish = scheduling.Publish,
            
        }).ToList();
    }

    public List<FilteredSchedulingDto> GetAllEmployeesScheduling(int page, int pageSize)
    {
       var employee  =  _unitOfWork.Scheduling.GetSchedulingWithEmployees();
       if (employee.Count == 0) return new List<FilteredSchedulingDto>(); 
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
            ExtraHours = x.ExtraHours,
            Publish = x.Publish,
            Employee = x.Employee,
            // ApprovedByNavigation = x.ApprovedByNavigation,
        }).ToList());
        return
        [
            new FilteredSchedulingDto
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                ScheduleDto = readDto.Result,
            }
        ];
        
    } 
    
    public List<FilteredSchedulingDto> GetAllEmployeesScheduling(int page, int pageSize,DateOnly? startDate)
    {
       var employee  =  _unitOfWork.Scheduling.GetSchedulingWithEmployees(startDate);
       if (employee.Count == 0) return new List<FilteredSchedulingDto>(); 
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
            Publish = x.Publish,
            ExtraHours = x.ExtraHours,
            Employee = x.Employee
        }).ToList());
        return
        [
            new FilteredSchedulingDto
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                ScheduleDto = readDto.Result,
            }
        ];
        
    } 
    
    // public Task<List<FilteredSchedulingDto>> GetAllEmployeesScheduling(int page, int pageSize,DateTime startDate)
    // {
    //    var employee  =  _schedulingRepo.GetSchedulingWithEmployees(startDate);
    //     var employeesMap = _mapper.Map<List<Scheduling>, List<ScheduleDto>>(employee);
    //     var totalCount = employeesMap.Count;
    //     var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    //     var employees = employeesMap.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    //     var readDto = Task.FromResult(employees.Select(x => new SchedulingReadDto
    //     {
    //         Id = x.Id,
    //         DepartmentId = x.DepartmentId,
    //         EmployeeId = x.EmployeeId,
    //         Date = x.Date,
    //         ShiftId = x.ShiftId,
    //         MinStartTime = x.MinStartTime,
    //         MaxStartTime = x.MaxStartTime,
    //         StartTime = x.StartTime,
    //         MinEndTime = x.MinEndTime,
    //         EndTime = x.EndTime,
    //         MaxEndTime = x.MaxEndTime,
    //         BreakTime = x.BreakTime,
    //         RepeatEvery = x.RepeatEvery,
    //         Note = x.Note,
    //         Confidential = x.Confidential,
    //         ApprovedBy = x.ApprovedBy,
    //         Employee = x.Employee
    //     }).ToList());
    //     return new Task<List<FilteredSchedulingDto>>(() => new List<FilteredSchedulingDto>
    //     {
    //         new FilteredSchedulingDto
    //         {
    //             TotalCount = totalCount,
    //             TotalPages = totalPages,
    //             SchedulingReadDto = readDto.Result,
    //         }
    //     });
    //     {
    //         
    //     }
    // }
    //
    public Task<List<ScheduleDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Scheduling> scheduling;
            scheduling = _unitOfWork.Scheduling.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var Schedule = _mapper.Map<IEnumerable<Scheduling>, IEnumerable<ScheduleDto>>(scheduling);
            return Task.FromResult(Schedule.ToList());
        }

        var  schedulings = _unitOfWork.Scheduling.GlobalSearch(searchKey);
        var shifts = _mapper.Map<IEnumerable<Scheduling>, IEnumerable<ScheduleDto>>(schedulings);
        // var emp = _mapper
        //     .Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>
        //     (schedulings.Select(s => s.Employee).ToList());
        return Task.FromResult(shifts.ToList());
    }

}
