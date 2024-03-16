
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class DesignationManager:IDesignationManager
{
    private readonly IDesignationRepo _designationRepo;
    private readonly IMapper _mapper;
    public DesignationManager(IDesignationRepo designationRepo, IMapper mapper)
    {
        _designationRepo = designationRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(DesignationAddDto designationAddDto)
    {
        var designation = new Designation()
        {
            Name = designationAddDto.Name,
            DepartmentId = designationAddDto.DepartmentId
        };
        return _designationRepo.Add(designation);
    }

    public Task<int> Update(DesignationUpdateDto designationUpdateDto,int id)
    {
        var designation = _designationRepo.GetById(id);
        
        if (designation.Result == null) return Task.FromResult(0);
        if(designationUpdateDto.Name != null) designation.Result.Name = designationUpdateDto.Name;
        if(designationUpdateDto.DepartmentId != null) designation.Result.DepartmentId = designationUpdateDto.DepartmentId;

       return _designationRepo.Update(designation.Result);
    }

    public Task<int> Delete(int id)
    {
        var designation = _designationRepo.GetById(id);
        if (designation.Result != null) return _designationRepo.Delete(designation.Result);
        return Task.FromResult(0);
    }

    public DesignationReadDto? Get(int id)
    {
        var designation = _designationRepo.GetById(id);
        if (designation.Result == null) return null;
        return new DesignationReadDto()
        {
            Id = designation.Result.Id,
            Name = designation.Result.Name,
            DepartmentId = designation.Result.DepartmentId,
        };
    }

    public List<DesignationReadDto> GetAll()
    {
        var designations = _designationRepo.GetAll();
        return designations.Result.Select(designation => new DesignationReadDto()
        {
            Id = designation.Id,
            Name = designation.Name,
            DepartmentId = designation.DepartmentId
            
        }).ToList();
    }
    
      public async Task<FilteredDesignationDto> GetFilteredDesignationsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _designationRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(paginatedResults);
            FilteredDesignationDto result = new()
            {
                DesignationDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Designation> filteredResults;
        
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

            var mappedDesignation = _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(paginatedResults);

            FilteredDesignationDto filteredDesignationDto = new()
            {
                DesignationDto = mappedDesignation,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredDesignationDto;
        }

        return new FilteredDesignationDto();
    }
    private IEnumerable<Designation> ApplyFilter(IEnumerable<Designation> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var designationValue) => ApplyNumericFilter(users, column, designationValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Designation> ApplyNumericFilter(IEnumerable<Designation> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue < value),
        _ => users
    };
}


    public Task<List<DesignationDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Designation> user;
            user = _designationRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var designation = _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(user);
            return Task.FromResult(designation.ToList());
        }

        var  users = _designationRepo.GlobalSearch(searchKey);
        var designations = _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(users);
        return Task.FromResult(designations.ToList());
    }

}
