
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class AttendanceManager:IAttendanceManager
{
    private readonly IAttendanceRepo _attendanceRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AttendanceManager(IAttendanceRepo attendanceRepo, IMapper mapper, IEmployeeRepo employeeRepo, IUnitOfWork unitOfWork)
    {
        _attendanceRepo = attendanceRepo;
        _mapper = mapper;
        _employeeRepo = employeeRepo;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(AttendanceAddDto attendanceAddDto)
    {
        var attendance = new Attendance()
        {
           EmployeeId = attendanceAddDto.EmployeeId,
           Break = attendanceAddDto.Break,
           Date = attendanceAddDto.Date,
           Overtime = attendanceAddDto.Overtime,
           Production = attendanceAddDto.Production,
           PunchIn = attendanceAddDto.PunchIn,
           PunchOut = attendanceAddDto.PunchOut,
        };
         _attendanceRepo.Add(attendance);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(AttendanceUpdateDto attendanceUpdateDto, int id)
    {
        var attendance = _attendanceRepo.GetById(id);
        
        if (attendance == null) return Task.FromResult(0);
        if(attendanceUpdateDto.EmployeeId!=null) attendance.EmployeeId = attendanceUpdateDto.EmployeeId;
        if(attendanceUpdateDto.Break!=null) attendance.Break = attendanceUpdateDto.Break;
        if(attendanceUpdateDto.Date!=null) attendance.Date = attendanceUpdateDto.Date;
        if(attendanceUpdateDto.Overtime!=null) attendance.Overtime = attendanceUpdateDto.Overtime;
        if(attendanceUpdateDto.Production!=null) attendance.Production = attendanceUpdateDto.Production;
        if(attendanceUpdateDto.PunchIn!=null) attendance.PunchIn = attendanceUpdateDto.PunchIn;
        if(attendanceUpdateDto.PunchOut!=null) attendance.PunchOut = attendanceUpdateDto.PunchOut;
        

        _attendanceRepo.Update(attendance);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
         _attendanceRepo.GetById(id);
         return _unitOfWork.SaveChangesAsync();
    }

    public AttendanceReadDto? Get(int id)
    {
        var attendance = _attendanceRepo.GetById(id);
        if (attendance == null) return null;
        return new AttendanceReadDto()
        {
            Id = attendance.Id,
            EmployeeId = attendance.EmployeeId,
            Break = attendance.Break,
            Date = attendance.Date,
            Overtime = attendance.Overtime,
            Production = attendance.Production,
            PunchIn = attendance.PunchIn,
            PunchOut = attendance.PunchOut,
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
            Overtime = attendance.Overtime,
            Production = attendance.Production,
            PunchIn = attendance.PunchIn,
            PunchOut = attendance.PunchOut,
            
        }).ToList();
    }
    
      public async Task<FilteredAttendanceDto> GetFilteredAttendancesAsync
          (string? column, string? value1, string? operator1, string? value2,
              string? operator2, int page, int pageSize)
    {
        var users = _attendanceRepo.GetAttendanceWithEmployee();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            List<AttendanceDto> attendanceDto = new();
            foreach (var attendance in paginatedResults)
            {
                attendanceDto.Add(new AttendanceDto()
                {
                    Id = attendance.Id,
                    Date = attendance.Date,
                    PunchIn = attendance.PunchIn,
                    PunchOut = attendance.PunchOut,
                    Production = attendance.Production,
                    Break = attendance.Break,
                    Overtime = attendance.Overtime,
                    Employee = _mapper.Map<Employee,EmployeeDto>(attendance.Employee),
                });
            }
            
            FilteredAttendanceDto result = new()
            {
                AttendanceDto = attendanceDto,
                TotalCount = count,
                TotalPages = pages
            };
            // foreach (var attendance in result.AttendanceDto)
            // {
            //     attendance.Department = _mapper.Map<Department, DepartmentDto>(attendance.Department);
            // }
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

            List<AttendanceDto> attendanceDto = new();
            foreach (var attendance in paginatedResults)
            {
                attendanceDto.Add(new AttendanceDto()
                {
                        Id = attendance.Id,
                        Date = attendance.Date,
                        PunchIn = attendance.PunchIn,
                        PunchOut = attendance.PunchOut,
                        Production = attendance.Production,
                        Break = attendance.Break,
                        Overtime = attendance.Overtime,
                        Employee = _mapper.Map<Employee,EmployeeDto>(attendance.Employee),
                        
                });
            }
            FilteredAttendanceDto filteredAttendanceDto = new()
            {
                AttendanceDto = attendanceDto,
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
            _ when decimal.TryParse(value, out var attendanceValue) => ApplyNumericFilter(users, column, attendanceValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Attendance> ApplyNumericFilter(IEnumerable<Attendance> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue < value),
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
            var employee =  _employeeRepo.GetById(element.EmployeeId);
            if (employee != null)
            {
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
        }

        return result;
    }

    public Task<List<AttendanceDto>> GetByEmployeeId(int employeeId)
    {
        var attendance = _attendanceRepo.GetByEmployeeId(employeeId);
        var mappedAttendance = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(attendance);
        return Task.FromResult(mappedAttendance.ToList());
    }
}
