
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

public class OvertimeManager:IOvertimeManager
{
    private readonly IOvertimeRepo _overtimeRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OvertimeManager(IOvertimeRepo overtimeRepo, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _overtimeRepo = overtimeRepo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(OvertimeAddDto overtimeAddDto)
    {
        var overtime = new Overtime()
        {
            OtHours = overtimeAddDto.OtHours,
            OtDate = overtimeAddDto.OtDate,
            OtType = overtimeAddDto.OtType,
            Description = overtimeAddDto.Description,
            Status = overtimeAddDto.Status,
            ApprovedBy = overtimeAddDto.ApprovedBy,
            EmployeeId = overtimeAddDto.EmployeeId,
            
        };
          _overtimeRepo.Add(overtime);
          return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(OvertimeUpdateDto overtimeUpdateDto, int id)
    {
        var overtime = _overtimeRepo.GetOvertimesWithEmployeeAndApprovedBy(id);
        
        if (overtime.Result == null) return Task.FromResult(0);
        if(overtimeUpdateDto.OtHours != null) overtime.Result.OtHours = overtimeUpdateDto.OtHours;
        if(overtimeUpdateDto.OtDate != null) overtime.Result.OtDate = overtimeUpdateDto.OtDate;
        if(overtimeUpdateDto.OtType != null) overtime.Result.OtType = overtimeUpdateDto.OtType;
        if(overtimeUpdateDto.Description != null) overtime.Result.Description = overtimeUpdateDto.Description;
        if(overtimeUpdateDto.Status != null) overtime.Result.Status = overtimeUpdateDto.Status;
        if(overtimeUpdateDto.ApprovedBy != null) overtime.Result.ApprovedBy = overtimeUpdateDto.ApprovedBy;
        if(overtimeUpdateDto.EmployeeId != null) overtime.Result.EmployeeId = overtimeUpdateDto.EmployeeId;

         _overtimeRepo.Update(overtime.Result);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _overtimeRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }


    public async Task<OvertimeReadDto>? Get(int id)
    {
        var overtime = await _overtimeRepo.GetOvertimesWithEmployeeAndApprovedBy(id);
        if (overtime == null) return null;
        return new OvertimeReadDto()
        {
            Id = overtime.Id,
            OtHours = overtime.OtHours,
            OtDate = overtime.OtDate,
            OtType = overtime.OtType,
            Description = overtime.Description,
            Status = overtime.Status,
            ApprovedBy = overtime.ApprovedByNavigation?.FullName,
            EmployeeDto = _mapper.Map<Employee, EmployeeDto>(overtime.Employee)
        };
    }

    public async Task<List<OvertimeReadDto>> GetAll()
    {
        var overtimes = await _overtimeRepo.GetOvertimesWithEmployeeAndApprovedBy();
        return overtimes.Select(overtime => new OvertimeReadDto()
        {
            Id = overtime.Id,
            OtHours = overtime.OtHours,
            OtDate = overtime.OtDate,
            OtType = overtime.OtType,
            Description = overtime.Description,
            Status = overtime.Status,
            ApprovedBy = overtime.ApprovedByNavigation?.FullName,
            Employee = overtime.Employee?.FullName,
            
        }).ToList();
    }
    
     public async Task<FilteredOvertimeDto> GetFilteredOvertimesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _overtimeRepo.GetOvertimesWithEmployeeAndApprovedBy();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            List<OvertimeDto> leavesDto = new();
            foreach (var leaves in paginatedResults)
            {
                leavesDto.Add(new OvertimeDto()
                {
                    Id = leaves.Id,
                    OtHours = leaves.OtHours,
                    OtDate = leaves.OtDate,
                    OtType = leaves.OtType,
                    Description = leaves.Description,
                    Status = leaves.Status,
                    Employee = leaves.Employee?.FullName,
                    ApprovedBy = leaves.ApprovedByNavigation?.FullName,
                    
                });
            }

            var result = new FilteredOvertimeDto()
            {
                OvertimeDto = leavesDto,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Overtime> filteredResults;
        
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

            List<OvertimeDto> leavesDto = new();
            foreach (var leaves in paginatedResults)
            {
                leavesDto.Add(new OvertimeDto()
                {
                    Id = leaves.Id,
                    OtHours = leaves.OtHours,
                    OtDate = leaves.OtDate,
                    OtType = leaves.OtType,
                    Description = leaves.Description,
                    Status = leaves.Status,
                    ApprovedBy = leaves.ApprovedByNavigation.FullName,
                    Employee = leaves.Employee.FullName
                });
            }

            var result = new FilteredOvertimeDto()
            {
                OvertimeDto = leavesDto,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return result;
        }

        return new FilteredOvertimeDto();
    }
    private IEnumerable<Overtime> ApplyFilter(IEnumerable<Overtime> users, string? column, string? value, string? operatorType)
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

    private IEnumerable<Overtime> ApplyNumericFilter(IEnumerable<Overtime> users, string? column, decimal? value, string? operatorType)
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


    public Task<List<OvertimeDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Overtime> user;
            user = _overtimeRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var project = _mapper.Map<IEnumerable<Overtime>, IEnumerable<OvertimeDto>>(user);
            return Task.FromResult(project.ToList());
        }

        var  users = _overtimeRepo.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Overtime>, IEnumerable<OvertimeDto>>(users);
        return Task.FromResult(projects.ToList());
    }

}
