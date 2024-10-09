using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class GoalTypeManager:IGoalTypeManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GoalTypeManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(GoalTypeAddDto goalTypeAddDto)
    {
        var goalType = new GoalType()
        {
            Type = goalTypeAddDto.Type,
            Description = goalTypeAddDto.Description,
            Status = goalTypeAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.GoalType.Add(goalType);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(GoalTypeUpdateDto goalTypeUpdateDto, int id)
    {
        var goalType = _unitOfWork.GoalType.GetById(id);
        
        
        if (goalType == null) return Task.FromResult(0);
        
        if(goalTypeUpdateDto.Type != null) goalType.Type = goalTypeUpdateDto.Type;
        if(goalTypeUpdateDto.Description != null) goalType.Description = goalTypeUpdateDto.Description;
        if(goalTypeUpdateDto.Status != null) goalType.Status = goalTypeUpdateDto.Status;

        goalType.UpdatedAt = DateTime.Now;
        _unitOfWork.GoalType.Update(goalType);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var goalType = _unitOfWork.GoalType.GetById(id);
        if (goalType==null) return Task.FromResult(0);
        goalType.IsDeleted = true;
        goalType.DeletedAt = DateTime.Now;
        _unitOfWork.GoalType.Update(goalType);
        return _unitOfWork.SaveChangesAsync();
    }

    public GoalTypeReadDto? Get(int id)
    {
        var goalType = _unitOfWork.GoalType.GetById(id);
        if (goalType == null) return null;
        return new GoalTypeReadDto()
        {
            Id = goalType.Id,
            Type = goalType.Type,
            Description = goalType.Description,
            Status = goalType.Status,
        };
    }

    public Task<List<GoalTypeReadDto>> GetAll()
    {
        var goalType = _unitOfWork.GoalType.GetAll();
        return Task.FromResult(goalType.Result.Select(p => new GoalTypeReadDto()
        {
            Id = p.Id,
            Type = p.Type,
            Description = p.Description,
            Status = p.Status,

        }).ToList());
    }
    
     public async Task<FilteredGoalTypeDto> GetFilteredGoalTypesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var goalTypes = await _unitOfWork.GoalType.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = goalTypes.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var goalTypeList = goalTypes.ToList();

            var paginatedResults = goalTypeList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedGoalTypes = new List<GoalTypeDto>();
            foreach (var goalType in paginatedResults)
            {
                mappedGoalTypes.Add(new GoalTypeDto()
                {
                    Id = goalType.Id,
                    Type = goalType.Type,
                    Description = goalType.Description,
                    Status = goalType.Status,
                });
            }
            FilteredGoalTypeDto filteredGoalTypeDto = new()
            {
                GoalTypeDto = mappedGoalTypes,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredGoalTypeDto;
        }

        if (goalTypes != null)
        {
            IEnumerable<GoalType> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(goalTypes, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(goalTypes, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var goalTypes = paginatedResults.ToList();
            // var mappedGoalType = _mapper.Map<IEnumerable<GoalType>, IEnumerable<GoalTypeDto>>(goalTypes);
            
            var mappedGoalTypes = new List<GoalTypeDto>();

            foreach (var goalType in paginatedResults)
            {
                
                mappedGoalTypes.Add(new GoalTypeDto()
                {
                    Id = goalType.Id,
                    Type = goalType.Type,
                    Description = goalType.Description,
                    Status = goalType.Status,
                    
                });
            }
            FilteredGoalTypeDto filteredGoalTypeDto = new()
            {
                GoalTypeDto = mappedGoalTypes,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredGoalTypeDto;
        }

        return new FilteredGoalTypeDto();
    }
    private IEnumerable<GoalType> ApplyFilter(IEnumerable<GoalType> goalTypes, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => goalTypes.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => goalTypes.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => goalTypes.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => goalTypes.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var goalTypeValue) => ApplyNumericFilter(goalTypes, column, goalTypeValue, operatorType),
            _ => goalTypes
        };
    }

    private IEnumerable<GoalType> ApplyNumericFilter(IEnumerable<GoalType> goalTypes, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => goalTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalTypeValue) && goalTypeValue == value),
        "neq" => goalTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalTypeValue) && goalTypeValue != value),
        "gte" => goalTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalTypeValue) && goalTypeValue >= value),
        "gt" => goalTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalTypeValue) && goalTypeValue > value),
        "lte" => goalTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalTypeValue) && goalTypeValue <= value),
        "lt" => goalTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalTypeValue) && goalTypeValue < value),
        _ => goalTypes
    };
}


    public Task<List<GoalTypeDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<GoalType> goalTypeDto;
            goalTypeDto = _unitOfWork.GoalType.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var goalType = _mapper.Map<IEnumerable<GoalType>, IEnumerable<GoalTypeDto>>(goalTypeDto);
            return Task.FromResult(goalType.ToList());
        }

        var  goalTypeDtos = _unitOfWork.GoalType.GlobalSearch(searchKey);
        var goalTypes = _mapper.Map<IEnumerable<GoalType>, IEnumerable<GoalTypeDto>>(goalTypeDtos);
        return Task.FromResult(goalTypes.ToList());
    }

}
