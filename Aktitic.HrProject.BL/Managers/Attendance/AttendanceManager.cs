
using System.Collections;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AttendanceManager( IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
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
        
         _unitOfWork.Attendance.Add(attendance);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(AttendanceUpdateDto attendanceUpdateDto, int id)
    {
        var attendance = _unitOfWork.Attendance.GetById(id);
        
        if (attendance == null) return Task.FromResult(0);
        if(attendanceUpdateDto.EmployeeId!=null) attendance.EmployeeId = attendanceUpdateDto.EmployeeId;
        if(attendanceUpdateDto.Break!=null) attendance.Break = attendanceUpdateDto.Break;
        if(attendanceUpdateDto.Date!=null) attendance.Date = attendanceUpdateDto.Date;
        if(attendanceUpdateDto.Overtime!=null) attendance.Overtime = attendanceUpdateDto.Overtime;
        if(attendanceUpdateDto.Production!=null) attendance.Production = attendanceUpdateDto.Production;
        if(attendanceUpdateDto.PunchIn!=null) attendance.PunchIn = attendanceUpdateDto.PunchIn;
        if(attendanceUpdateDto.PunchOut!=null) attendance.PunchOut = attendanceUpdateDto.PunchOut;
        
        _unitOfWork.Attendance.Update(attendance);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        // soft delete
        
        var attendance = _unitOfWork.Attendance.GetById(id);
        
        if (attendance == null) return Task.FromResult(0);
        
        attendance.IsDeleted = true;
        
        _unitOfWork.Attendance.Update(attendance);
        
         return _unitOfWork.SaveChangesAsync();
    }

    public AttendanceReadDto? Get(int id)
    {
        var attendance = _unitOfWork.Attendance.GetById(id);
        if (attendance == null) return null;
        return new AttendanceReadDto()
        {
            Id = attendance.Id,
            // EmployeeId = attendance.EmployeeId,
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
        var attendances = _unitOfWork.Attendance.GetAll();
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
              string? operator2 , int page, int pageSize= 1)
      {
          // var attendances = new List<Attendance>();
        // if(attendancesModel ==null)
           var attendances = _unitOfWork.Employee.GetEmployeesWithAttendancesAsync().Result;
        // else
        // {
             // attendances = attendancesModel;
        // }

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = attendances.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var attendanceList = attendances.ToList();

            var paginatedResults = attendanceList.Skip((page - 1) * pageSize).Take(pageSize);

            var attendanceDto = GetAllEmployeeAttendanceInCurrentMonth(paginatedResults.ToList()).Result;
            FilteredAttendanceDto result = new()
            {
                AttendanceDto = attendanceDto,
                TotalCount = count,
                TotalPages = pages
            };
            // foreach (var attendance in result.AttendanceDto)
            // {
            //     attendance.DepartmentId = _mapper.Map<DepartmentId, DepartmentDto>(attendance.DepartmentId);
            // }
            return result;
        }

        if (attendances != null)
        {
            IEnumerable<Employee> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(attendances, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(attendances, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var attendanceDto = GetAllEmployeeAttendanceInCurrentMonth(paginatedResults.ToList()).Result;
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
    
    public async Task<TodayFilteredAttendanceDto> GetTodayFilteredAttendancesAsync
          (string? column, string? value1, string? operator1, string? value2,
              string? operator2 , int page, int pageSize= 1)
      {
          // var attendances = new List<Attendance>();
        // if(attendancesModel ==null)
           var attendances = _unitOfWork.Employee.GetEmployeesWithAttendancesAsync().Result;
        // else
        // {
             // attendances = attendancesModel;
        // }

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = attendances.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var attendanceList = attendances.ToList();

            var paginatedResults = attendanceList.Skip((page - 1) * pageSize).Take(pageSize);

            var attendanceDto = GetAllEmployeeAttendanceToday(paginatedResults.ToList()).Result;
            int attendedCount = attendanceDto
                .Count(a => a
                                .TryGetValue("Today", out var today) 
                            && today is Dictionary<string, object> todayDict 
                            && todayDict.TryGetValue("Attended", out var attended) 
                            && (bool)attended);
            TodayFilteredAttendanceDto result = new()
            {
                AttendanceDto = attendanceDto,
                TotalCount = count,
                TotalPages = pages,
                TotalAttendance = attendedCount
            };
            
            return result;
        }

        if (attendances != null)
        {
            IEnumerable<Employee> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(attendances, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(attendances, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var attendanceDto = GetAllEmployeeAttendanceToday(paginatedResults.ToList()).Result;
            
            // count all attendance 
            int attendedCount = attendanceDto
                .Count(a => a
                    .TryGetValue("Today", out var today) 
                            && today is Dictionary<string, object> todayDict 
                            && todayDict.TryGetValue("attended", out var attended) 
                            && (bool)attended);
            
            TodayFilteredAttendanceDto filteredAttendanceDto = new()
            {
                AttendanceDto = attendanceDto,
                TotalCount = totalCount,
                TotalPages = totalPages,
                TotalAttendance = attendedCount,
            };
            return filteredAttendanceDto;
        }

        return new TodayFilteredAttendanceDto();
    }
    
    
    private IEnumerable<Employee> ApplyFilter(IEnumerable<Employee> attendances, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => attendances.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => attendances.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => attendances.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => attendances.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var attendanceValue) => ApplyNumericFilter(attendances, column, attendanceValue, operatorType),
            _ => attendances
        };
    }

    private IEnumerable<Employee> ApplyNumericFilter(IEnumerable<Employee> attendances, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => attendances.Where(e => column != null && decimal
            .TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue == value),
        "neq" => attendances.Where(e => column != null && decimal
            .TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue != value),
        "gte" => attendances.Where(e => column != null && decimal
            .TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue >= value),
        "gt" => attendances.Where(e => column != null && decimal
            .TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue > value),
        "lte" => attendances.Where(e => column != null && decimal
            .TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue <= value),
        "lt" => attendances.Where(e => column != null && decimal
            .TryParse(e.GetPropertyValue(column), out var attendanceValue) && attendanceValue < value),
        _ => attendances
    };
}


    public Task<List<AttendanceDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Attendance> attendance;
            attendance = _unitOfWork.Attendance.GetAll().Result
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            
            var project = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(attendance);
            return Task.FromResult(project.ToList());
        }

        var  attendances = _unitOfWork.Attendance.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(attendances);
        return Task.FromResult(projects.ToList());
    }

    public async Task<List<Dictionary<string, object>>> GetAllEmployeeAttendanceInCurrentMonth(List<Employee> employees)
    {
        
        var currentYear = DateTime.Now.Year;
        var currentMonth = DateTime.Now.Month;
        var daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

        // Create the response format
        var employeeAttendance = new List<Dictionary<string, object>>();

        foreach (var employee in employees)
        {
            var attendanceRecord = new Dictionary<string, object>
            {
                { "id", employee.Id },
                { "employee", employee.FullName },
            };

            for (int day = 1; day <= daysInMonth ; day++)
            {
                var attendanceOnDay = employee.Attendances.FirstOrDefault(d => d.Date.Value.Day == day);
                if (attendanceOnDay != null)
                {
                    attendanceRecord.Add(day.ToString(), new { attendanceOnDay.Id, Attended = true});
                }
                else
                {
                    attendanceRecord.Add(day.ToString(), new { Id = (int?)null, Attended = false });
                }
            }

            employeeAttendance.Add(attendanceRecord);
        }

        return employeeAttendance;
    }

    // get all attendance in today 
    
    public async Task<List<Dictionary<string, object>>> GetAllEmployeeAttendanceToday(List<Employee> employees)
    {
        // Get the current date
        // cast to date only 
        
        var today = DateOnly.FromDateTime(DateTime.Today);
        

        // Create the response format
        var employeeAttendance = new List<Dictionary<string, object>>();

        foreach (var employee in employees)
        {
            var attendanceRecord = new Dictionary<string, object>
            {
                { "id", employee.Id },
                { "employee", new {employee.FullName , employee.ImgUrl,department = employee.Department?.Name, employee.JoiningDate}}
            };

            // Check attendance for today
            var attendanceToday = employee.Attendances.FirstOrDefault(d => d.Date == today);
            if (attendanceToday != null)
            {
                attendanceRecord.Add("Today", new { attendanceToday.Id, Attended = true });
            }
            else
            {
                attendanceRecord.Add("Today", new { Id = (int?)null, Attended = false });
            }

            employeeAttendance.Add(attendanceRecord);
        }

        return employeeAttendance;
    }
    
    public async Task<PaginatedAttendanceDto> GetEmployeeAttendance(int employeeId,int page, int pageSize)
    {
   
        var employee = await _unitOfWork.Employee.GetEmployeeWithAttendancesAsync(employeeId);
        var attendanceDtos = employee.Attendances.Select(a => new AttendanceDto()
        {
            Id = a.Id,
            EmployeeId = a.EmployeeId,
            Date = a.Date,
            PunchIn = a.PunchIn,
            PunchOut = a.PunchOut,
            Overtime = a.Overtime,
            Break = a.Break,
            Production = a.Production
        }).ToList();
        
        var orderByDescending = attendanceDtos.OrderByDescending(d => d.Date);
        var attendanceDtosResult = orderByDescending   
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();
        int totalCount = attendanceDtos.Count();
        int totalPages = (int)Math.Ceiling((double) totalCount/pageSize);
        var result = new PaginatedAttendanceDto()
        {
            AttendanceDto = attendanceDtosResult,
            TotalCount = attendanceDtos.Count(),
            TotalPages = totalPages
        };
        return (result);     
    }
    
    // public async Task<List<Dictionary<string, object>>> GetAllEmployeeAttendanceInCurrentMonth(
    //     string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    // {
    //     // Retrieve attendance records for the current month
    //     var attendance = await _unitOfWork.Attendance.GetEmployeeAttendanceInCurrentMonth();
    //     var filteredAttendance = await GetFilteredAttendancesAsync(column, value1, operator1, value2, operator2, attendance, page, pageSize);
    //
    //     var attendanceDto = filteredAttendance.AttendanceDto
    //         .Select(a => new AttendanceInMonthDto
    //         {
    //             Id = a.Id,
    //             EmployeeId = a.EmployeeId,
    //             EmployeeName = a.Employee.FullName,
    //             Date = a.Date,
    //             Attended = true, // This should be determined based on actual attendance data
    //         })
    //         .ToList();
    //
    //     // Group attendance records by employee
    //     var groupedByEmployee = attendanceDto
    //         .GroupBy(a => new { a.EmployeeId, a.EmployeeName })
    //         .Select(g => new
    //         {
    //             EmployeeId = g.Key.EmployeeId,
    //             EmployeeName = g.Key.EmployeeName,
    //             Days = g.ToList()
    //         });
    //
    //     // Create the response format
    //     var employeeAttendance = new List<Dictionary<string, object>>();
    //
    //     foreach (var employee in groupedByEmployee)
    //     {
    //         var attendanceRecord = new Dictionary<string, object>
    //         {
    //             { "id", employee.EmployeeId },
    //             { "employee", employee.EmployeeName },
    //             
    //         };
    //
    //         for (int day = 1; day <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); day++)
    //         {
    //             var attendanceOnDay = employee.Days.FirstOrDefault(d => d.Date.Value.Day == day);
    //             if (attendanceOnDay != null)
    //             {
    //                 attendanceRecord.Add(day.ToString(), new { attendanceOnDay.Id, attendanceOnDay.Attended });
    //             }
    //             else
    //             {
    //                 attendanceRecord.Add(day.ToString(), new { Id = (int?)null, Attended = false });
    //             }
    //         }
    //
    //         employeeAttendance.Add(attendanceRecord);
    //     }
    //
    //     return employeeAttendance;
    // }
    
     public Task<List<AttendanceDto>> GetByEmployeeId(int employeeId)
    {
        var attendance = _unitOfWork.Attendance.GetByEmployeeId(employeeId);
        var mappedAttendance = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceDto>>(attendance);
        return Task.FromResult(mappedAttendance.ToList());
    }
    //  public async Task<IEnumerable<AttendanceRepo.EmployeeAttendanceDalDto>> GetEmployeeAttendanceInCurrentMonth(List<AttendanceDto> attendanceDtoList, List<EmployeeDto> employeeDtoList)
    // {
    //     int currentYear = DateTime.Today.Year;
    //     int currentMonth = DateTime.Today.Month;
    //
    //     DateOnly startDate = new DateOnly(currentYear, currentMonth, 1);
    //     DateOnly endDate = startDate.AddMonths(1).AddDays(-1);
    //
    //     // Filter attendance records within the current month
    //     var filteredAttendance = attendanceDtoList
    //         .Where(attendance => attendance.Date.HasValue &&
    //                              attendance.Date.Value >= startDate &&
    //                              attendance.Date.Value <= endDate).ToList();
    //     
    //    
    //     // Group filtered attendance records by EmployeeId
    //     var groupedByEmployee = filteredAttendance
    //         .GroupBy(attendance => attendance.EmployeeId)
    //         .ToList();
    //
    //     var employeeAttendanceList = new List<AttendanceRepo.EmployeeAttendanceDalDto>();
    //
    //     // Ensure all employees are included in the result
    //     foreach (var employee in employeeDtoList)
    //     {
    //         var employeeId = employee.Id;
    //         var attendanceList = groupedByEmployee
    //             .FirstOrDefault(group => group.Key == employeeId)?
    //             .ToList() ?? new List<AttendanceDto>();
    //
    //         var employeeAttendance = new AttendanceRepo.EmployeeAttendanceDalDto
    //         {
    //             EmployeeId = employeeId,
    //             Date = startDate,
    //             Attended = attendanceList.Count > 0,
    //             Attendance = attendanceList
    //         };
    //
    //         employeeAttendanceList.Add(employeeAttendance);
    //     }
    //
    //     return employeeAttendanceList;
    // }
}
