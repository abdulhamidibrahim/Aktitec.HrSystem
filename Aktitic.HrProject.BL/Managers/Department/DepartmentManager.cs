using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class DepartmentManager:IDepartmentManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartmentManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(DepartmentAddDto departmentAddDto)
    {
        var department = new Department()
        {
            Name = departmentAddDto.Name,
            DeletedAt = DateTime.Now,
        };
         _unitOfWork.Department.Add(department);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(DepartmentUpdateDto departmentUpdateDto,int id)
    {
        var department = _unitOfWork.Department.GetById(id);
        
        if (department == null) return Task.FromResult(0);
        if(departmentUpdateDto.Name!=null) department.Name = departmentUpdateDto.Name;

        department.UpdatedAt = DateTime.Now;
         _unitOfWork.Department.Update(department);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var department = _unitOfWork.Department.GetById(id);
        if (department==null) return Task.FromResult(0);
        department.IsDeleted = true;
        department.DeletedAt = DateTime.Now;
        _unitOfWork.Department.Update(department);
        return _unitOfWork.SaveChangesAsync();
    }


    public DepartmentReadDto? Get(int id)
    {
        var department = _unitOfWork.Department.GetById(id);
        if (department == null) return null;
        return new DepartmentReadDto()
        {
            Id = department.Id,
            Name = department.Name,
        };
    }

    public List<DepartmentReadDto> GetAll()
    {
        var departments = _unitOfWork.Department.GetAll();
        return departments.Result.Select(department => new DepartmentReadDto()
        {
            Id = department.Id,
            Name = department.Name,
            
        }).ToList();
    }
    
     public async Task<FilteredDepartmentDto> GetFilteredDepartmentsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var departments = await _unitOfWork.Department.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = departments.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var departmentList = departments.ToList();

            var paginatedResults = departmentList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(paginatedResults);
            FilteredDepartmentDto result = new()
            {
                DepartmentDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (departments != null)
        {
            IEnumerable<Department> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(departments, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(departments, column, value2, operator2));
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
    private IEnumerable<Department> ApplyFilter(IEnumerable<Department> departments, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => departments.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => departments.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => departments.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => departments.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var departmentValue) => ApplyNumericFilter(departments, column, departmentValue, operatorType),
            _ => departments
        };
    }

    private IEnumerable<Department> ApplyNumericFilter(IEnumerable<Department> departments, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => departments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue == value),
        "neq" => departments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue != value),
        "gte" => departments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue >= value),
        "gt" => departments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue > value),
        "lte" => departments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue <= value),
        "lt" => departments.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var departmentValue) && departmentValue < value),
        _ => departments
    };
}


    public Task<List<DepartmentDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Department> departmentDto;
            departmentDto = _unitOfWork.Department.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var department = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departmentDto);
            return Task.FromResult(department.ToList());
        }

        var  departmentDtos = _unitOfWork.Department.GlobalSearch(searchKey);
        var departments = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departmentDtos);
        return Task.FromResult(departments.ToList());
    }
}
