
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

public class LeavesManager:ILeavesManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LeavesManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(LeavesAddDto leavesAddDto)
    {
        var leaves = new Leaves()
        {
            EmployeeId = leavesAddDto.EmployeeId,
            Type = leavesAddDto.Type,
            FromDate = leavesAddDto.FromDate,
            ToDate = leavesAddDto.ToDate,
            Reason = leavesAddDto.Reason,
            ApprovedBy = leavesAddDto.ApprovedBy,
            Days = leavesAddDto.Days,
            Approved = leavesAddDto.Approved,
            Status = leavesAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Leaves.Add(leaves);
        return _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> Update(LeavesUpdateDto leavesUpdateDto, int id)
    {
        var leaves = _unitOfWork.Leaves.GetById(id); // Use async method for fetching data

        if (leaves == null) return 0;

        if (leavesUpdateDto.EmployeeId != null) leaves.EmployeeId = leavesUpdateDto.EmployeeId;
        if (leavesUpdateDto.Type != null) leaves.Type = leavesUpdateDto.Type;
        if (leavesUpdateDto.FromDate != null) leaves.FromDate = leavesUpdateDto.FromDate;
        if (leavesUpdateDto.ToDate != null) leaves.ToDate = leavesUpdateDto.ToDate;
        if (leavesUpdateDto.Reason != null) leaves.Reason = leavesUpdateDto.Reason;
        if (leavesUpdateDto.ApprovedBy != null) leaves.ApprovedBy = leavesUpdateDto.ApprovedBy;
        if (leavesUpdateDto.Status != null) leaves.Status = leavesUpdateDto.Status;
        if (leavesUpdateDto.Days != null) leaves.Days = leavesUpdateDto.Days;
        if (leavesUpdateDto.Approved != null) leaves.Approved = leavesUpdateDto.Approved;

        leaves.UpdatedAt = DateTime.Now;
        _unitOfWork.Leaves.Update(leaves);
        return await _unitOfWork.SaveChangesAsync(); // Use async method for saving changes
    }

    public Task<int> Delete(int id)
    {
        var leaves = _unitOfWork.Leaves.GetById(id);
        if (leaves==null) return Task.FromResult(0);
        leaves.IsDeleted = true;
        leaves.DeletedAt = DateTime.Now;
        _unitOfWork.Leaves.Update(leaves);
        return _unitOfWork.SaveChangesAsync();
    }

    public LeavesReadDto? Get(int id)
    {
        var leaves = _unitOfWork.Leaves.GetById(id);
        if (leaves == null) return null;
        return new LeavesReadDto()
        {
            Id = leaves.Id,
            EmployeeId = leaves.EmployeeId,
            Type = leaves.Type,
            FromDate = leaves.FromDate,
            ToDate = leaves.ToDate,
            Reason = leaves.Reason,
            ApprovedBy = leaves.ApprovedBy,
            Days = leaves.Days,
            Approved = leaves.Approved,
            Status = leaves.Status,
        };
    }

    public List<LeavesReadDto> GetAll()
    {
        var leaves = _unitOfWork.Leaves.GetAll();
        return leaves.Result.Select(leave => new LeavesReadDto()
        {
            Id = leave.Id,
            EmployeeId = leave.EmployeeId,
            Type = leave.Type,
            FromDate = leave.FromDate,
            ToDate = leave.ToDate,
            Reason = leave.Reason,
            ApprovedBy = leave.ApprovedBy,
            Days = leave.Days,
            Approved = leave.Approved,
            Status = leaves.Status.ToString()

        }).ToList();
    }

    public Task<FilteredLeavesDto> GetFilteredLeavesAsync(string? column, string? value1, string? operator1,
        string? value2, string? operator2, int page, int pageSize)
    {
        var leavesList =  _unitOfWork.Leaves.GetLeavesWithEmployee();

        // Check if leaves is null
        if (leavesList == null)
        {
            return  Task.FromResult(new FilteredLeavesDto());
        }

        // Check if column, value1, and operator1 are all null or empty
        // IEnumerable<Leaves>? paginatedResults;
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = leavesList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var leaveList = leavesList.ToList();

            var paginatedResults = leaveList.Skip((page - 1) * pageSize).Take(pageSize);

            List<LeavesGetFilteredDto> leavesDto = new();
            foreach (var leaves in paginatedResults)
            {
                leavesDto.Add(new LeavesGetFilteredDto()
                {
                    Id = leaves.Id,
                    EmployeeId = leaves.EmployeeId,
                    Type = leaves.Type,
                    FromDate = leaves.FromDate,
                    ToDate = leaves.ToDate,
                    Reason = leaves.Reason,
                    Days = leaves.Days,
                    Approved = leaves.Approved,
                    Status = leaves.Status,
                    Employee = _mapper.Map<Employee, EmployeeDto>(leaves.Employee),
                    ApprovedBy = _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation)?.FullName,
                });
            }

            var result = new FilteredLeavesDto()
            {
                LeavesDto = leavesDto,
                TotalCount = count,
                TotalPages = pages
            };
            return Task.FromResult(result);
        }

        IEnumerable<Leaves> filteredResults;

        // Apply the first filter
        filteredResults = ApplyFilter(leavesList, column, value1, operator1);

        // Apply the second filter only if both value2 and operator2 are provided
        if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
        {
            filteredResults = filteredResults.Concat(ApplyFilter(leavesList, column, value2, operator2));
        }

        var enumerable = filteredResults.Distinct().ToList(); // Use Distinct to eliminate duplicates
        var totalCount = enumerable.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        var results = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

        List<LeavesGetFilteredDto> leavesDtos = new();
        foreach (var leaves in results)
        {
            leavesDtos.Add(new LeavesGetFilteredDto()
            {
                Id = leaves.Id,
                EmployeeId = leaves.EmployeeId,
                Type = leaves.Type,
                FromDate = leaves.FromDate,
                ToDate = leaves.ToDate,
                Reason = leaves.Reason,
                Days = leaves.Days,
                Approved = leaves.Approved,
                ApprovedById = leaves.ApprovedBy,
                Status = leaves.Status,
                Employee = _mapper.Map<Employee, EmployeeDto>(leaves.Employee),
                ApprovedBy = _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation)?.FullName,
            });
        }

        var filteredLeaves = new FilteredLeavesDto()
        {
            LeavesDto = leavesDtos,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        return Task.FromResult(filteredLeaves);
    }

    private IEnumerable<Leaves> ApplyFilter(IEnumerable<Leaves> leaves, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => leaves.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => leaves.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => leaves.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => leaves.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var projectValue) => ApplyNumericFilter(leaves, column, projectValue, operatorType),
            _ => leaves
        };
    }

    private IEnumerable<Leaves> ApplyNumericFilter(IEnumerable<Leaves> leaves, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => leaves.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue == value),
        "neq" => leaves.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue != value),
        "gte" => leaves.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue >= value),
        "gt" => leaves.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue > value),
        "lte" => leaves.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue <= value),
        "lt" => leaves.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue < value),
        _ => leaves
    };
}


    public Task<List<LeavesGetFilteredDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Leaves> leave;
            leave = _unitOfWork.Leaves.GetLeavesWithEmployee().AsQueryable().Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var mappedLeave = leave.Select(leaves=>new  LeavesGetFilteredDto()
            {
                Id = leaves.Id,
                EmployeeId = leaves.EmployeeId,
                Type = leaves.Type,
                FromDate = leaves.FromDate,
                ToDate = leaves.ToDate,
                Reason = leaves.Reason,
                Days = leaves.Days,
                Approved = leaves.Approved,
                Status = leaves.Status,
                Employee = _mapper.Map<Employee, EmployeeDto>(leaves.Employee),
                ApprovedBy = _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation) != null ? 
                    _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation).FullName : null,
            });
            return Task.FromResult(mappedLeave.ToList());
        }

        var  leaves = _unitOfWork.Leaves.GlobalSearch(searchKey);
        var mappedLeaves = leaves.Select(leaves => new LeavesGetFilteredDto()
        {
            Id = leaves.Id,
            EmployeeId = leaves.EmployeeId,
            Type = leaves.Type,
            FromDate = leaves.FromDate,
            ToDate = leaves.ToDate,
            Reason = leaves.Reason,
            Days = leaves.Days,
            Approved = leaves.Approved,
            Status = leaves.Status,
            Employee = _mapper.Map<Employee, EmployeeDto>(leaves.Employee),
            ApprovedBy = _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation) != null ? 
                _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation).FullName : null,
        });
        return Task.FromResult(mappedLeaves.ToList());
    }

}
