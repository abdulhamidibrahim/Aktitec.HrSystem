
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

public class AttendanceManager:IAttendanceManager
{
    private readonly IAttendanceRepo _attendanceRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IMapper _mapper;

    public AttendanceManager(IAttendanceRepo attendanceRepo, IMapper mapper, IEmployeeRepo employeeRepo)
    {
        _attendanceRepo = attendanceRepo;
        _mapper = mapper;
        _employeeRepo = employeeRepo;
    }
    
    public Task<int> Add(AttendanceAddDto attendanceAddDto)
    {
        var attendance = new Attendance()
        {
           EmployeeId = attendanceAddDto.EmployeeId,
           Break = attendanceAddDto.Break,
           Date = attendanceAddDto.Date,
           OvertimeId = attendanceAddDto.Overtime,
           Production = attendanceAddDto.Production,
           PunchIn = attendanceAddDto.PunchIn,
           PunchOut = attendanceAddDto.PunchOut,
        };
        return _attendanceRepo.Add(attendance);
    }

    public Task<int> Update(AttendanceUpdateDto attendanceUpdateDto,int id)
    {
        var attendance = _attendanceRepo.GetById(id);
        
        if (attendance.Result == null) return Task.FromResult(0);
        if(attendanceUpdateDto.EmployeeId!=null) attendance.Result.EmployeeId = attendanceUpdateDto.EmployeeId;
        if(attendanceUpdateDto.Break!=null) attendance.Result.Break = attendanceUpdateDto.Break;
        if(attendanceUpdateDto.Date!=null) attendance.Result.Date = attendanceUpdateDto.Date;
        if(attendanceUpdateDto.Overtime!=null) attendance.Result.OvertimeId = attendanceUpdateDto.Overtime;
        if(attendanceUpdateDto.Production!=null) attendance.Result.Production = attendanceUpdateDto.Production;
        if(attendanceUpdateDto.PunchIn!=null) attendance.Result.PunchIn = attendanceUpdateDto.PunchIn;
        if(attendanceUpdateDto.PunchOut!=null) attendance.Result.PunchOut = attendanceUpdateDto.PunchOut;
        

       return _attendanceRepo.Update(attendance.Result);
    }

    public Task<int> Delete(int id)
    {
        var attendance = _attendanceRepo.GetById(id);
        if (attendance.Result != null) return _attendanceRepo.Delete(attendance.Result.Id);
        return Task.FromResult(0);
    }

    public AttendanceReadDto? Get(int id)
    {
        var attendance = _attendanceRepo.GetById(id);
        if (attendance.Result == null) return null;
        return new AttendanceReadDto()
        {
            Id = attendance.Result.Id,
            EmployeeId = attendance.Result.EmployeeId,
            Break = attendance.Result.Break,
            Date = attendance.Result.Date,
            Overtime = attendance.Result.OvertimeId,
            Production = attendance.Result.Production,
            PunchIn = attendance.Result.PunchIn,
            PunchOut = attendance.Result.PunchOut,
        };
    }

    public List<AttendanceReadDto> GetAll()
    {
        var attendances = _attendanceRepo.GetAll();
        return attendances.Result.Select(attendance => new AttendanceReadDto()
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            Break = attendance.Break,
            Date = attendance.Date,
            Overtime = attendance.OvertimeId,
            Production = attendance.Production,
            PunchIn = attendance.PunchIn,
            PunchOut = attendance.PunchOut,
            
        }).ToList();
    }
    
     public async Task<FilteredAttendanceDto> GetFilteredAttendancesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _attendanceRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(paginatedResults);
            FilteredAttendanceDto result = new()
            {
                AttendanceDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Attendance> filteredResults;
        
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

            var mappedAttendance = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(paginatedResults);

            FilteredAttendanceDto filteredAttendanceDto = new()
            {
                AttendanceDto = mappedAttendance,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredAttendanceDto;
        }

        return new FilteredAttendanceDto();
    }
    private IEnumerable<Attendance> ApplyFilter(IEnumerable<Attendance> users, string? column, string? value, string? operatorType)
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

    private IEnumerable<Attendance> ApplyNumericFilter(IEnumerable<Attendance> users, string? column, decimal? value, string? operatorType)
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


    public Task<List<AttendanceDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Attendance> user;
            user = _attendanceRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var project = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(user);
            return Task.FromResult(project.ToList());
        }

        var  users = _attendanceRepo.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(users);
        return Task.FromResult(projects.ToList());
    }

    public async Task<List<EmployeeAttendanceDto>> GetAllEmployeeAttendanceInCurrentMonth(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        // Call the method to get attendance records for the current month
        var paginatedAttendance = await GetFilteredAttendancesAsync(column, value1, operator1, value2, operator2, page, pageSize);

        var attendanceDtoList = paginatedAttendance.AttendanceDto.ToList();

        // Get employee attendance information for the filtered attendance records
        var employeeAttendance = _attendanceRepo.GetEmployeeAttendanceInCurrentMonth(attendanceDtoList);

        // Create DTOs for each employee's attendance
        var result = new List<EmployeeAttendanceDto>();

        foreach (var element in employeeAttendance)
        {
            var employee = await _employeeRepo.GetById(element.EmployeeId);
            var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);

            // Map attendance records to AttendanceDto
            var attendanceDto = _mapper.Map<List<AttendanceDto>>(element.Attendance);

            result.Add(new EmployeeAttendanceDto()
            {
                EmployeeDto = employeeDto,
                AttendanceDto = attendanceDto,
                Date = element.Date,
                Attended = element.Attended
            });
        }

        return result;
    }
}
