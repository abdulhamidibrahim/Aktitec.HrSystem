using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class AptitudeResultsManager(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IAptitudeResultsManager
{
    public Task<int> Add(AptitudeResultsAddDto aptitudeResultsAddDto)
    {
        var aptitudeResults = new AptitudeResult
        {
            EmployeeId = aptitudeResultsAddDto.EmployeeId,
            JobId = aptitudeResultsAddDto.JobId,
            CategoryWiseMark = aptitudeResultsAddDto.CategoryWiseMark,
            Status = aptitudeResultsAddDto.Status,
            TotalMark = aptitudeResultsAddDto.TotalMark,
        };
        
        unitOfWork.AptitudeResults.Add(aptitudeResults);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(AptitudeResultsUpdateDto aptitudeResultsUpdateDto, int id)
    {
        var aptitudeResults = unitOfWork.AptitudeResults.GetById(id);
        
        if (aptitudeResults == null) return Task.FromResult(0);

        if (aptitudeResultsUpdateDto.CategoryWiseMark.IsNullOrEmpty()) aptitudeResults.CategoryWiseMark = aptitudeResultsUpdateDto.CategoryWiseMark;
        if (aptitudeResultsUpdateDto.TotalMark.IsNullOrEmpty()) aptitudeResults.TotalMark = aptitudeResultsUpdateDto.TotalMark;
        if (aptitudeResultsUpdateDto.EmployeeId != 0) aptitudeResults.EmployeeId = aptitudeResultsUpdateDto.EmployeeId;
        if (aptitudeResultsUpdateDto.JobId != 0) aptitudeResults.JobId = aptitudeResultsUpdateDto.JobId;
        if (aptitudeResultsUpdateDto.Status.IsNullOrEmpty()) aptitudeResults.Status = aptitudeResultsUpdateDto.Status;
        
        
        
        unitOfWork.AptitudeResults.Update(aptitudeResults);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var aptitudeResults = unitOfWork.AptitudeResults.GetById(id);
        if (aptitudeResults==null) return Task.FromResult(0);
        aptitudeResults.IsDeleted = true;
        unitOfWork.AptitudeResults.Update(aptitudeResults);
        return unitOfWork.SaveChangesAsync();
    }

    public AptitudeResultsReadDto? Get(int id)
    {
        var aptitudeResults = unitOfWork.AptitudeResults.GetById(id);
        if (aptitudeResults == null) return null;
        
        return new AptitudeResultsReadDto()
        {
            Id = aptitudeResults.Id,
            CategoryWiseMark = aptitudeResults.CategoryWiseMark,
            EmployeeId = aptitudeResults.EmployeeId,
            JobId = aptitudeResults.JobId,
            Status = aptitudeResults.Status,
            TotalMark = aptitudeResults.TotalMark,
            CreatedAt = aptitudeResults.CreatedAt,
            CreatedBy = aptitudeResults.CreatedBy,
            UpdatedAt = aptitudeResults.UpdatedAt,
            UpdatedBy = aptitudeResults.UpdatedBy,    
        };
    }

    public async Task<List<AptitudeResultsReadDto>> GetAll()
    {
        var aptitudeResults = await unitOfWork.AptitudeResults.GetAll();
        return aptitudeResults.Select(aptitudeResults => new AptitudeResultsReadDto()
        {
            Id = aptitudeResults.Id,
            CategoryWiseMark = aptitudeResults.CategoryWiseMark,
            EmployeeId = aptitudeResults.EmployeeId,
            JobId = aptitudeResults.JobId,
            Status = aptitudeResults.Status,
            TotalMark = aptitudeResults.TotalMark,
            CreatedAt = aptitudeResults.CreatedAt,
            CreatedBy = aptitudeResults.CreatedBy,
            UpdatedAt = aptitudeResults.UpdatedAt,
            UpdatedBy = aptitudeResults.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredAptitudeResultsDto> GetFilteredAptitudeResultsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var aptitudeResultsList = await unitOfWork.AptitudeResults.GetAllAptitudeResults();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = aptitudeResultsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = aptitudeResultsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var aptitudeResultsDto = new List<AptitudeResultsDto>();
            foreach (var aptitudeResults in paginatedResults)
            {
                aptitudeResultsDto.Add(new AptitudeResultsDto()
                {
                    Id = aptitudeResults.Id,
                    CategoryWiseMark = aptitudeResults.CategoryWiseMark,
                    EmployeeId = aptitudeResults.EmployeeId,
                    JobId = aptitudeResults.JobId,
                    Status = aptitudeResults.Status,
                    TotalMark = aptitudeResults.TotalMark,
                    Employee = mapper.Map<Employee,EmployeeDto>(aptitudeResults.Employee),
                    Job = mapper.Map<Job,JobsDto>(aptitudeResults.Job),
                    CreatedAt = aptitudeResults.CreatedAt,
                    CreatedBy = aptitudeResults.CreatedBy,
                    UpdatedAt = aptitudeResults.UpdatedAt,
                    UpdatedBy = aptitudeResults.UpdatedBy,    
                });
            }
            FilteredAptitudeResultsDto filteredBudgetsExpensesDto = new()
            {
                AptitudeResultsDto = aptitudeResultsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (aptitudeResultsList != null)
        {
            IEnumerable<AptitudeResult> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(aptitudeResultsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(aptitudeResultsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var aptitudeResultss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(aptitudeResultss);
            
            var mappedAptitudeResults = new List<AptitudeResultsDto>();

            foreach (var aptitudeResults in paginatedResults)
            {
                
                mappedAptitudeResults.Add(new AptitudeResultsDto()
                {
                    Id = aptitudeResults.Id,
                    CategoryWiseMark = aptitudeResults.CategoryWiseMark,
                    EmployeeId = aptitudeResults.EmployeeId,
                    JobId = aptitudeResults.JobId,
                    Status = aptitudeResults.Status,
                    TotalMark = aptitudeResults.TotalMark,
                    Employee = mapper.Map<Employee,EmployeeDto>(aptitudeResults.Employee),
                    Job = mapper.Map<Job,JobsDto>(aptitudeResults.Job),
                    CreatedAt = aptitudeResults.CreatedAt,
                    CreatedBy = aptitudeResults.CreatedBy,
                    UpdatedAt = aptitudeResults.UpdatedAt,
                    UpdatedBy = aptitudeResults.UpdatedBy,    
                });
            }
            FilteredAptitudeResultsDto filteredAptitudeResultsDto = new()
            {
                AptitudeResultsDto = mappedAptitudeResults,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredAptitudeResultsDto;
        }

        return new FilteredAptitudeResultsDto();
    }
    private IEnumerable<AptitudeResult> ApplyFilter(IEnumerable<AptitudeResult> aptitudeResults, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => aptitudeResults.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => aptitudeResults.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => aptitudeResults.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => aptitudeResults.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var aptitudeResultsValue) => ApplyNumericFilter(aptitudeResults, column, aptitudeResultsValue, operatorType),
            _ => aptitudeResults
        };
    }

    private IEnumerable<AptitudeResult> ApplyNumericFilter(IEnumerable<AptitudeResult> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var aptitudeResultsValue) && aptitudeResultsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var aptitudeResultsValue) && aptitudeResultsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var aptitudeResultsValue) && aptitudeResultsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var aptitudeResultsValue) && aptitudeResultsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var aptitudeResultsValue) && aptitudeResultsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var aptitudeResultsValue) && aptitudeResultsValue < value),
        _ => policys
    };
}


    public Task<List<AptitudeResultsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<AptitudeResult> enumerable = unitOfWork.AptitudeResults.GetAllAptitudeResults().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var aptitudeResults = enumerable.Select(aptitudeResults => new AptitudeResultsDto()
            {
                Id = aptitudeResults.Id,
                CategoryWiseMark = aptitudeResults.CategoryWiseMark,
                EmployeeId = aptitudeResults.EmployeeId,
                JobId = aptitudeResults.JobId,
                Status = aptitudeResults.Status,
                TotalMark = aptitudeResults.TotalMark,
                Employee = mapper.Map<Employee,EmployeeDto>(aptitudeResults.Employee),
                Job = mapper.Map<Job,JobsDto>(aptitudeResults.Job),
                CreatedAt = aptitudeResults.CreatedAt,
                CreatedBy = aptitudeResults.CreatedBy,
                UpdatedAt = aptitudeResults.UpdatedAt,
                UpdatedBy = aptitudeResults.UpdatedBy,    
            });
            return Task.FromResult(aptitudeResults.ToList());
        }

        var  queryable = unitOfWork.AptitudeResults.GlobalSearch(searchKey);
        var aptitudeResultss = queryable.Select(aptitudeResults => new AptitudeResultsDto()
        {
            Id = aptitudeResults.Id,
            CategoryWiseMark = aptitudeResults.CategoryWiseMark,
            EmployeeId = aptitudeResults.EmployeeId,
            JobId = aptitudeResults.JobId,
            Status = aptitudeResults.Status,
            TotalMark = aptitudeResults.TotalMark,
            Employee = mapper.Map<Employee,EmployeeDto>(aptitudeResults.Employee),
            Job = mapper.Map<Job,JobsDto>(aptitudeResults.Job),
            CreatedAt = aptitudeResults.CreatedAt,
            CreatedBy = aptitudeResults.CreatedBy,
            UpdatedAt = aptitudeResults.UpdatedAt,
            UpdatedBy = aptitudeResults.UpdatedBy,    
        });
        return Task.FromResult(aptitudeResultss.ToList());
    }

}
