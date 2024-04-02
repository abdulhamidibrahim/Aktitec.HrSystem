
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
    private readonly ILeavesRepo _leavesRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LeavesManager(ILeavesRepo leavesRepo, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _leavesRepo = leavesRepo;
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
            Status = leavesAddDto.Status
            
        }; 
        _leavesRepo.Add(leaves);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(LeavesUpdateDto leavesUpdateDto, int id)
    {
        var leaves = _leavesRepo.GetById(id);

        if (leaves == null) return Task.FromResult(0);
        if(leavesUpdateDto.EmployeeId != null) leaves.EmployeeId = leavesUpdateDto.EmployeeId;
        if(leavesUpdateDto.Type != null) leaves.Type = leavesUpdateDto.Type;
        if(leavesUpdateDto.FromDate != null) leaves.FromDate = leavesUpdateDto.FromDate;
        if(leavesUpdateDto.ToDate != null) leaves.ToDate = leavesUpdateDto.ToDate;
        if(leavesUpdateDto.Reason != null) leaves.Reason = leavesUpdateDto.Reason;
        if(leavesUpdateDto.ApprovedBy!=null) leaves.ApprovedBy = leavesUpdateDto.ApprovedBy;
        if(leavesUpdateDto.Days != null) leaves.Days = leavesUpdateDto.Days;
        if(leavesUpdateDto.Approved != null) leaves.Approved = leavesUpdateDto.Approved;
        
        _leavesRepo.Update(leaves);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _leavesRepo.GetById(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public LeavesReadDto? Get(int id)
    {
        var leaves = _leavesRepo.GetById(id);
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
        var leaves = _leavesRepo.GetAll();
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

    public async Task<FilteredLeavesDto> GetFilteredLeavesAsync(string? column, string? value1, string? operator1,
        string? value2, string? operator2, int page, int pageSize)
    {
        var users =  _leavesRepo.GetLeavesWithEmployee();

        // Check if users is null
        if (users == null)
        {
            return new FilteredLeavesDto();
        }

        // Check if column, value1, and operator1 are all null or empty
        IEnumerable<Leaves>? paginatedResults;
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            List<LeavesDto> leavesDto = new();
            foreach (var leaves in paginatedResults)
            {
                leavesDto.Add(new LeavesDto()
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
                    ApprovedBy = _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation)
                });
            }

            var result = new FilteredLeavesDto()
            {
                LeavesDto = leavesDto,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        IEnumerable<Leaves> filteredResults;

        // Apply the first filter
        filteredResults = ApplyFilter(users, column, value1, operator1);

        // Apply the second filter only if both value2 and operator2 are provided
        if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
        {
            filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
        }

        var enumerable = filteredResults.Distinct().ToList(); // Use Distinct to eliminate duplicates
        var totalCount = enumerable.Count();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

        List<LeavesDto> leavesDtos = new();
        foreach (var leaves in paginatedResults)
        {
            leavesDtos.Add(new LeavesDto()
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
                ApprovedBy = _mapper.Map<Employee, EmployeeDto>(leaves.ApprovedByNavigation)
            });
        }

        var filteredLeaves = new FilteredLeavesDto()
        {
            LeavesDto = leavesDtos,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
        return filteredLeaves;
    }

    private IEnumerable<Leaves> ApplyFilter(IEnumerable<Leaves> users, string? column, string? value, string? operatorType)
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

    private IEnumerable<Leaves> ApplyNumericFilter(IEnumerable<Leaves> users, string? column, decimal? value, string? operatorType)
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


    public Task<List<LeavesDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Leaves> user;
            user = _leavesRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var project = _mapper.Map<IEnumerable<Leaves>, IEnumerable<LeavesDto>>(user);
            return Task.FromResult(project.ToList());
        }

        var  users = _leavesRepo.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Leaves>, IEnumerable<LeavesDto>>(users);
        return Task.FromResult(projects.ToList());
    }

}
