
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class DepartmentManager:IDepartmentManager
{
    private readonly IDepartmentRepo _departmentRepo;
    private readonly IMapper _mapper;
    public DepartmentManager(IDepartmentRepo departmentRepo, IMapper mapper)
    {
        _departmentRepo = departmentRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(DepartmentAddDto departmentAddDto)
    {
        var department = new Department()
        {
            Name = departmentAddDto.Name,
        };
        return _departmentRepo.Add(department);
    }

    public Task<int> Update(DepartmentUpdateDto departmentUpdateDto,int id)
    {
        var department = _departmentRepo.GetById(id);
        
        if (department.Result == null) return Task.FromResult(0);
        if(departmentUpdateDto.Name!=null) department.Result.Name = departmentUpdateDto.Name;


        return _departmentRepo.Update(department.Result);
    }

    public Task<int> Delete(int id)
    {
        var department = _departmentRepo.GetById(id);
        if (department.Result != null) return _departmentRepo.Delete(department.Result);
        return Task.FromResult(0);
    }

    public DepartmentReadDto? Get(int id)
    {
        var department = _departmentRepo.GetById(id);
        if (department.Result == null) return null;
        return new DepartmentReadDto()
        {
            Id = department.Result.Id,
            Name = department.Result.Name,
        };
    }

    public List<DepartmentReadDto> GetAll()
    {
        var departments = _departmentRepo.GetAll();
        return departments.Result.Select(department => new DepartmentReadDto()
        {
            Id = department.Id,
            Name = department.Name,
            
        }).ToList();
    }
    
     public async Task<FilteredDepartmentDto> GetFilteredDepartmentsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _departmentRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(paginatedResults);
            FilteredDepartmentDto result = new()
            {
                DepartmentDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Department> filteredResults;
        
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

            var mappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(paginatedResults);

            FilteredDepartmentDto filteredDepartmentDto = new()
            {
                DepartmentDto = mappedDepartment,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredDepartmentDto;
        }

        return new FilteredDepartmentDto();
    }
    private IEnumerable<Department> ApplyFilter(IEnumerable<Department> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var departmentValue) => ApplyNumericFilter(users, column, departmentValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Department> ApplyNumericFilter(IEnumerable<Department> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue < value),
        _ => users
    };
}


    public Task<List<DepartmentDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Department> user;
            user = _departmentRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var department = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(user);
            return Task.FromResult(department.ToList());
        }

        var  users = _departmentRepo.GlobalSearch(searchKey);
        var departments = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(users);
        return Task.FromResult(departments.ToList());
    }
}
