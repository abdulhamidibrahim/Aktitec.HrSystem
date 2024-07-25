
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PerformanceAppraisalManager:IPerformanceAppraisalManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PerformanceAppraisalManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(PerformanceAppraisalAddDto performanceAppraisalAddDto)
    {
        var performanceAppraisal = new PerformanceAppraisal()
        {
            EmployeeId = performanceAppraisalAddDto.EmployeeId,
            Date = performanceAppraisalAddDto.Date,
            Status = performanceAppraisalAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.PerformanceAppraisal.Add(performanceAppraisal);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PerformanceAppraisalUpdateDto performanceAppraisalUpdateDto, int id)
    {
        var performanceAppraisal = _unitOfWork.PerformanceAppraisal.GetById(id);
        
        
        if (performanceAppraisal == null) return Task.FromResult(0);
        
        
        if(performanceAppraisalUpdateDto.EmployeeId != null) performanceAppraisal.EmployeeId = performanceAppraisalUpdateDto.EmployeeId;
        if(performanceAppraisalUpdateDto.Date != null) performanceAppraisal.Date = performanceAppraisalUpdateDto.Date;
        if(performanceAppraisalUpdateDto.Status != null) performanceAppraisal.Status = performanceAppraisalUpdateDto.Status;

        performanceAppraisal.UpdatedAt = DateTime.Now;
        _unitOfWork.PerformanceAppraisal.Update(performanceAppraisal);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var performanceAppraisal = _unitOfWork.PerformanceAppraisal.GetById(id);
        if (performanceAppraisal==null) return Task.FromResult(0);
        performanceAppraisal.IsDeleted = true;
        performanceAppraisal.DeletedAt = DateTime.Now;
        _unitOfWork.PerformanceAppraisal.Update(performanceAppraisal);
        return _unitOfWork.SaveChangesAsync();
    }

    public PerformanceAppraisalReadDto? Get(int id)
    {
        var performanceAppraisal = _unitOfWork.PerformanceAppraisal.GetWithEmployees(id).FirstOrDefault();
        if (performanceAppraisal == null) return null;
        return new PerformanceAppraisalReadDto()
        {
            Id = performanceAppraisal.Id,
            Employee = _mapper.Map<Employee,EmployeeDto>(performanceAppraisal.Employee),
            Designation = performanceAppraisal.Employee
                .Department!
                .Designations != null ? 
                performanceAppraisal
                    .Employee
                    .Department
                    .Designations
                    .Select(x=>x.Name)
                    .FirstOrDefault() : null,
            Department = performanceAppraisal.Employee?.Department?.Name,
            Date = performanceAppraisal.Date,
            Status = performanceAppraisal.Status,
        };
    }

    public Task<List<PerformanceAppraisalReadDto>> GetAll()
    {
        var performanceAppraisal = _unitOfWork.PerformanceAppraisal.GetAllWithEmployees();
        return Task.FromResult(performanceAppraisal.Select(appraisal => new PerformanceAppraisalReadDto()
        {
            Id = appraisal.Id,
            Employee = _mapper.Map<Employee,EmployeeDto>(appraisal.Employee),
            Designation = appraisal.Employee
                .Department!
                .Designations != null ? 
                appraisal
                    .Employee
                    .Department
                    .Designations
                    .Select(x=>x.Name)
                    .FirstOrDefault() : null,
            Department = appraisal.Employee != null ? appraisal.Employee.Department != null ? appraisal.Employee.Department.Name : null : null,
            Date = appraisal.Date,
            Status = appraisal.Status,
        }).ToList());
    }
    
     public async Task<FilteredPerformanceAppraisalDto> GetFilteredPerformanceAppraisalsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var performanceAppraisals =  _unitOfWork.PerformanceAppraisal.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = performanceAppraisals.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var performanceAppraisalList = performanceAppraisals.ToList();

            var paginatedResults = performanceAppraisalList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedPerformanceAppraisals = new List<PerformanceAppraisalDto>();
            foreach (var performanceAppraisal in paginatedResults)
            {
                mappedPerformanceAppraisals.Add(new PerformanceAppraisalDto()
                {
                    Id = performanceAppraisal.Id,
                    Employee = _mapper.Map<Employee,EmployeeDto>(performanceAppraisal.Employee),
                    Designation = performanceAppraisal.Employee
                        .Department!
                        .Designations != null ? 
                        performanceAppraisal
                            .Employee
                            .Department
                            .Designations
                            .Select(x=>x.Name)
                            .FirstOrDefault() : null,
                    Department = performanceAppraisal.Employee?.Department?.Name,
                    Date = performanceAppraisal.Date,
                    Status = performanceAppraisal.Status,
                });
            }
            FilteredPerformanceAppraisalDto filteredPerformanceAppraisalDto = new()
            {
                PerformanceAppraisalDto = mappedPerformanceAppraisals,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredPerformanceAppraisalDto;
        }

        if (performanceAppraisals != null)
        {
            IEnumerable<PerformanceAppraisal> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(performanceAppraisals, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(performanceAppraisals, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var performanceAppraisals = paginatedResults.ToList();
            // var mappedPerformanceAppraisal = _mapper.Map<IEnumerable<PerformanceAppraisal>, IEnumerable<PerformanceAppraisalDto>>(performanceAppraisals);
            
            var mappedPerformanceAppraisals = new List<PerformanceAppraisalDto>();

            foreach (var performanceAppraisal in paginatedResults)
            {
                
                mappedPerformanceAppraisals.Add(new PerformanceAppraisalDto()
                {
                    Id = performanceAppraisal.Id,
                    Employee = _mapper.Map<Employee,EmployeeDto>(performanceAppraisal.Employee),
                    Designation = performanceAppraisal.Employee
                        .Department!
                        .Designations != null ? 
                        performanceAppraisal
                            .Employee
                            .Department
                            .Designations
                            .Select(x=>x.Name)
                            .FirstOrDefault() : null,
                    Department = performanceAppraisal.Employee?.Department?.Name,
                    Date = performanceAppraisal.Date,
                    Status = performanceAppraisal.Status,
                });
            }
            FilteredPerformanceAppraisalDto filteredPerformanceAppraisalDto = new()
            {
                PerformanceAppraisalDto = mappedPerformanceAppraisals,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPerformanceAppraisalDto;
        }

        return new FilteredPerformanceAppraisalDto();
    }
    private IEnumerable<PerformanceAppraisal> ApplyFilter(IEnumerable<PerformanceAppraisal> performanceAppraisals, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => performanceAppraisals.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => performanceAppraisals.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => performanceAppraisals.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => performanceAppraisals.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var performanceAppraisalValue) => ApplyNumericFilter(performanceAppraisals, column, performanceAppraisalValue, operatorType),
            _ => performanceAppraisals
        };
    }

    private IEnumerable<PerformanceAppraisal> ApplyNumericFilter(IEnumerable<PerformanceAppraisal> performanceAppraisals, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => performanceAppraisals.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceAppraisalValue) && performanceAppraisalValue == value),
        "neq" => performanceAppraisals.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceAppraisalValue) && performanceAppraisalValue != value),
        "gte" => performanceAppraisals.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceAppraisalValue) && performanceAppraisalValue >= value),
        "gt" => performanceAppraisals.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceAppraisalValue) && performanceAppraisalValue > value),
        "lte" => performanceAppraisals.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceAppraisalValue) && performanceAppraisalValue <= value),
        "lt" => performanceAppraisals.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var performanceAppraisalValue) && performanceAppraisalValue < value),
        _ => performanceAppraisals
    };
}


    public Task<List<PerformanceAppraisalDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<PerformanceAppraisal> performanceAppraisalDto;
            performanceAppraisalDto = _unitOfWork.PerformanceAppraisal.GetAllWithEmployees().AsEnumerable().Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var performanceAppraisal = performanceAppraisalDto.Select(appraisal => new PerformanceAppraisalDto()
            {
                Id = appraisal.Id,
                Employee = _mapper.Map<Employee,EmployeeDto>(appraisal.Employee),
                Designation = appraisal.Employee
                    .Department!
                    .Designations != null ? 
                    appraisal
                        .Employee
                        .Department
                        .Designations
                        .Select(x=>x.Name)
                        .FirstOrDefault() : null,
                Department = appraisal.Employee?.Department?.Name,
                Date = appraisal.Date,
                Status = appraisal.Status,
            });
            return Task.FromResult(performanceAppraisal.ToList());
        }

        var  performanceAppraisalDtos = _unitOfWork.PerformanceAppraisal.GlobalSearch(searchKey);
        var performanceAppraisals = performanceAppraisalDtos.Select(appraisal => new PerformanceAppraisalDto()
        {
            Id = appraisal.Id,
            Employee = _mapper.Map<Employee,EmployeeDto>(appraisal.Employee),
            Designation = appraisal.Employee
                .Department!
                .Designations != null ? 
                appraisal
                    .Employee
                    .Department
                    .Designations
                    .Select(x=>x.Name)
                    .FirstOrDefault() : null,
            Department = appraisal.Employee.Department != null ? appraisal.Employee.Department.Name : null,
            Date = appraisal.Date,
            Status = appraisal.Status,
        });
        return Task.FromResult(performanceAppraisals.ToList());
    }

}
