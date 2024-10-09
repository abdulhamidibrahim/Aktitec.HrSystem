using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class OvertimeManager:IOvertimeManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OvertimeManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
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
            CreatedAt = DateTime.Now,
        };
          _unitOfWork.Overtime.Add(overtime);
          return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(OvertimeUpdateDto overtimeUpdateDto, int id)
    {
        var overtime = _unitOfWork.Overtime.GetOvertimesWithEmployeeAndApprovedBy(id);
        
        if (overtime.Result == null) return Task.FromResult(0);
        if(overtimeUpdateDto.OtHours != null) overtime.Result.OtHours = overtimeUpdateDto.OtHours;
        if(overtimeUpdateDto.OtDate != null) overtime.Result.OtDate = overtimeUpdateDto.OtDate;
        if(overtimeUpdateDto.OtType != null) overtime.Result.OtType = overtimeUpdateDto.OtType;
        if(overtimeUpdateDto.Description != null) overtime.Result.Description = overtimeUpdateDto.Description;
        if(overtimeUpdateDto.Status != null) overtime.Result.Status = overtimeUpdateDto.Status;
        if(overtimeUpdateDto.ApprovedBy != null) overtime.Result.ApprovedBy = overtimeUpdateDto.ApprovedBy;
        if(overtimeUpdateDto.EmployeeId != null) overtime.Result.EmployeeId = overtimeUpdateDto.EmployeeId;

        overtime.Result.UpdatedAt = DateTime.Now;
         _unitOfWork.Overtime.Update(overtime.Result);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var overtime = _unitOfWork.Overtime.GetById(id);
        if (overtime==null) return Task.FromResult(0);
        overtime.IsDeleted = true;
        overtime.DeletedAt = DateTime.Now;
        _unitOfWork.Overtime.Update(overtime);
        return _unitOfWork.SaveChangesAsync();
    }


    public async Task<OvertimeReadDto>? Get(int id)
    {
        var overtime = await _unitOfWork.Overtime.GetOvertimesWithEmployeeAndApprovedBy(id);
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
        var overtimes = await _unitOfWork.Overtime.GetOvertimesWithEmployeeAndApprovedBy();
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
        var overtimes = await _unitOfWork.Overtime.GetOvertimesWithEmployeeAndApprovedBy();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = overtimes.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var overtimeList = overtimes.ToList();

            var paginatedResults = overtimeList.Skip((page - 1) * pageSize).Take(pageSize);

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

        if (overtimes != null)
        {
            IEnumerable<Overtime> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(overtimes, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(overtimes, column, value2, operator2));
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
    private IEnumerable<Overtime> ApplyFilter(IEnumerable<Overtime> overtimes, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => overtimes.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => overtimes.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => overtimes.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => overtimes.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var projectValue) => ApplyNumericFilter(overtimes, column, projectValue, operatorType),
            _ => overtimes
        };
    }

    private IEnumerable<Overtime> ApplyNumericFilter(IEnumerable<Overtime> overtimes, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => overtimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue == value),
        "neq" => overtimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue != value),
        "gte" => overtimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue >= value),
        "gt" => overtimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue > value),
        "lte" => overtimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue <= value),
        "lt" => overtimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue < value),
        _ => overtimes
    };
}


    public Task<List<OvertimeDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Overtime> overtime;
            overtime = _unitOfWork.Overtime.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var project = _mapper.Map<IEnumerable<Overtime>, IEnumerable<OvertimeDto>>(overtime);
            return Task.FromResult(project.ToList());
        }

        var  overtimes = _unitOfWork.Overtime.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Overtime>, IEnumerable<OvertimeDto>>(overtimes);
        return Task.FromResult(projects.ToList());
    }

}
