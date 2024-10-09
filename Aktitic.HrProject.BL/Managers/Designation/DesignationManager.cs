using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class DesignationManager:IDesignationManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DesignationManager( IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(DesignationAddDto designationAddDto)
    {
        var designation = new Designation()
        {
            Name = designationAddDto.Name,
            DepartmentId = designationAddDto.DepartmentId,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Designation.Add(designation);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(DesignationUpdateDto designationUpdateDto,int id)
    {
        var designation = _unitOfWork.Designation.GetById(id);
        
        if (designation == null) return Task.FromResult(0);
        if(designationUpdateDto.Name != null) designation.Name = designationUpdateDto.Name;
        if(designationUpdateDto.DepartmentId != null) designation.DepartmentId = designationUpdateDto.DepartmentId;

        designation.UpdatedAt = DateTime.Now;
        _unitOfWork.Designation.Update(designation);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var designation = _unitOfWork.Designation.GetById(id);
        if (designation==null) return Task.FromResult(0);
        designation.IsDeleted = true;
        designation.DeletedAt = DateTime.Now;
        _unitOfWork.Designation.Update(designation);
        return _unitOfWork.SaveChangesAsync();
    }

    public  DesignationReadDto? Get(int id)
    {
        var designation =  _unitOfWork.Designation.GetById(id);
        if (designation == null)
            return new DesignationReadDto();

        var department =  _unitOfWork.Department.GetById(designation.DepartmentId);

        var mappedDepartment = department != null
            ? _mapper.Map<Department, DepartmentDto>(department)
            : new DepartmentDto();

        return new DesignationReadDto
        {
            Id = designation.Id,
            Name = designation.Name,
            DepartmentId = designation.DepartmentId,
            Department = mappedDepartment
        };
    }

    public async Task<List<DesignationReadDto>> GetAll()
    {
        var  designations = await _unitOfWork.Designation.GetAll();
        return designations.Select(designation => new DesignationReadDto()
        {
            Id = designation.Id,
            Name = designation.Name,
            DepartmentId = designation.DepartmentId
            
        }).ToList();
    }
    
      public async Task<FilteredDesignationDto> GetFilteredDesignationsAsync
          (string? column, string? value1, string? operator1, string? value2,
              string? operator2, int page, int pageSize)
    {
        var designations = _unitOfWork.Designation.GetDesignationsWithDepartments();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = designations.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var designationList = designations.ToList();

            var paginatedResults = designationList.Skip((page - 1) * pageSize).Take(pageSize);

            List<DesignationDto> designationDto = new();
            foreach (var designation in paginatedResults)
            {
                designationDto.Add(new DesignationDto()
                {
                    Name = designation.Name,
                    Id = designation.Id,
                    DepartmentId = designation.DepartmentId,
                    Department = designation.Department?.Name,
                });
            }
            
            FilteredDesignationDto result = new()
            {
                DesignationDto = designationDto,
                TotalCount = count,
                TotalPages = pages
            };
            // foreach (var designation in result.DesignationDto)
            // {
            //     designation.DepartmentId = _mapper.Map<DepartmentId, DepartmentDto>(designation.DepartmentId);
            // }
            return result;
        }

        if (designations != null)
        {
            IEnumerable<Designation> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(designations, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(designations, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            List<DesignationDto> designationDto = new();
            foreach (var designation in paginatedResults)
            {
                designationDto.Add(new DesignationDto()
                {
                    Name = designation.Name,
                    Id = designation.Id,
                    DepartmentId = designation.DepartmentId,
                    Department = designation.Department?.Name,
                });
            }
            FilteredDesignationDto filteredDesignationDto = new()
            {
                DesignationDto = designationDto,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredDesignationDto;
        }

        return new FilteredDesignationDto();
    }
    private IEnumerable<Designation> ApplyFilter(IEnumerable<Designation> designations, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => designations.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => designations.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => designations.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => designations.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var designationValue) => ApplyNumericFilter(designations, column, designationValue, operatorType),
            _ => designations
        };
    }

    private IEnumerable<Designation> ApplyNumericFilter(IEnumerable<Designation> designations, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => designations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue == value),
        "neq" => designations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue != value),
        "gte" => designations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue >= value),
        "gt" => designations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue > value),
        "lte" => designations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue <= value),
        "lt" => designations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var designationValue) && designationValue < value),
        _ => designations
    };
}


    public Task<List<DesignationDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Designation> designationDto;
            designationDto = _unitOfWork.Designation.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var designation = _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(designationDto);
            return Task.FromResult(designation.ToList());
        }

        var  designationDtos = _unitOfWork.Designation.GlobalSearch(searchKey);
        var designations = _mapper.Map<IEnumerable<Designation>, IEnumerable<DesignationDto>>(designationDtos);
        return Task.FromResult(designations.ToList());
    }

}
