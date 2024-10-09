using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class GoalListManager:IGoalListManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GoalListManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(GoalListAddDto goalListAddDto)
    {
        var goalList = new GoalList()
        {
            TypeId = goalListAddDto.TypeId,
            Subject = goalListAddDto.Subject,
            TargetAchievement = goalListAddDto.TargetAchievement,
            StartDate = goalListAddDto.StartDate,
            EndDate = goalListAddDto.EndDate,
            Description = goalListAddDto.Description,
            Status = goalListAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.GoalList.Add(goalList);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(GoalListUpdateDto goalListUpdateDto, int id)
    {
        var goalList = _unitOfWork.GoalList.GetById(id);
        
        
        if (goalList == null) return Task.FromResult(0);
        
        if(goalListUpdateDto.TypeId != null) goalList.TypeId = goalListUpdateDto.TypeId;
        if(goalListUpdateDto.Subject != null) goalList.Subject = goalListUpdateDto.Subject;
        if(goalListUpdateDto.TargetAchievement != null) goalList.TargetAchievement = goalListUpdateDto.TargetAchievement;
        if(goalListUpdateDto.StartDate != null) goalList.StartDate = goalListUpdateDto.StartDate;
        if(goalListUpdateDto.EndDate != null) goalList.EndDate = goalListUpdateDto.EndDate;
        if(goalListUpdateDto.Description != null) goalList.Description = goalListUpdateDto.Description;
        if(goalListUpdateDto.Status != null) goalList.Status = goalListUpdateDto.Status;

        goalList.UpdatedAt = DateTime.Now;
        _unitOfWork.GoalList.Update(goalList);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var goalList = _unitOfWork.GoalList.GetById(id);
        if (goalList==null) return Task.FromResult(0);
        goalList.IsDeleted = true;
        goalList.DeletedAt = DateTime.Now;
        _unitOfWork.GoalList.Update(goalList);
        return _unitOfWork.SaveChangesAsync();
    }

    public GoalListReadDto? Get(int id)
    {
        var goalList = _unitOfWork.GoalList.GetWithGoalType(id).FirstOrDefault();
        if (goalList == null) return null;
        return new GoalListReadDto()
        {
            Id = goalList.Id,
            TypeId = goalList.TypeId,
            GoalType = goalList.GoalType?.Type,
            Subject = goalList.Subject,
            TargetAchievement = goalList.TargetAchievement,
            StartDate = goalList.StartDate,
            EndDate = goalList.EndDate,
            Description = goalList.Description,
            Status = goalList.Status,
        };
    }

    public Task<List<GoalListReadDto>> GetAll()
    {
        var goalList = _unitOfWork.GoalList.GetAllWithGoalType();
        return Task.FromResult(goalList.Select(g => new GoalListReadDto()
        {
            Id = g.Id,
            TypeId = g.TypeId,
            GoalType = g.GoalType != null ? g.GoalType.Type : null,
            Subject = g.Subject,
            TargetAchievement = g.TargetAchievement,
            StartDate = g.StartDate,
            EndDate = g.EndDate,
            Description = g.Description,
            Status = g.Status,

        }).ToList());
    }
    
     public async Task<FilteredGoalListDto> GetFilteredGoalListsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var goalLists =  _unitOfWork.GoalList.GetAllWithGoalType();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = goalLists.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var goalListList = goalLists.ToList();

            var paginatedResults = goalListList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedGoalLists = new List<GoalListDto>();
            foreach (var goalList in paginatedResults)
            {
                mappedGoalLists.Add(new GoalListDto()
                {
                    Id = goalList.Id,
                    TypeId = goalList.TypeId,
                    GoalType = (goalList.GoalType)?.Type,
                    Subject = goalList.Subject,
                    TargetAchievement = goalList.TargetAchievement,
                    StartDate = goalList.StartDate,
                    EndDate = goalList.EndDate,
                    Description = goalList.Description,
                    Status = goalList.Status,
                });
            }
            FilteredGoalListDto filteredGoalListDto = new()
            {
                GoalListDto = mappedGoalLists,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredGoalListDto;
        }

        if (goalLists != null)
        {
            IEnumerable<GoalList> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(goalLists, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(goalLists, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var goalLists = paginatedResults.ToList();
            // var mappedGoalList = _mapper.Map<IEnumerable<GoalList>, IEnumerable<GoalListDto>>(goalLists);
            
            var mappedGoalLists = new List<GoalListDto>();

            foreach (var goalList in paginatedResults)
            {
                
                mappedGoalLists.Add(new GoalListDto()
                {
                    Id = goalList.Id,
                    TypeId = goalList.TypeId,
                    GoalType = (goalList.GoalType)?.Type,
                    Subject = goalList.Subject,
                    TargetAchievement = goalList.TargetAchievement,
                    StartDate = goalList.StartDate,
                    EndDate = goalList.EndDate,
                    Description = goalList.Description,
                    Status = goalList.Status,
                    
                });
            }
            FilteredGoalListDto filteredGoalListDto = new()
            {
                GoalListDto = mappedGoalLists,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredGoalListDto;
        }

        return new FilteredGoalListDto();
    }
    private IEnumerable<GoalList> ApplyFilter(IEnumerable<GoalList> goalLists, string? column, string? value, string? operatorList)
    {
        // value2 ??= value;

        return operatorList switch
        {
            "contains" => goalLists.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => goalLists.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => goalLists.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => goalLists.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var goalListValue) => ApplyNumericFilter(goalLists, column, goalListValue, operatorList),
            _ => goalLists
        };
    }

    private IEnumerable<GoalList> ApplyNumericFilter(IEnumerable<GoalList> goalLists, string? column, decimal? value, string? operatorList)
{
    return operatorList?.ToLower() switch
    {
        "eq" => goalLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalListValue) && goalListValue == value),
        "neq" => goalLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalListValue) && goalListValue != value),
        "gte" => goalLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalListValue) && goalListValue >= value),
        "gt" => goalLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalListValue) && goalListValue > value),
        "lte" => goalLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalListValue) && goalListValue <= value),
        "lt" => goalLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var goalListValue) && goalListValue < value),
        _ => goalLists
    };
}


    public Task<List<GoalListDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<GoalList> goalListDto;
            goalListDto = _unitOfWork.GoalList.GetAllWithGoalType().AsEnumerable().Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var goalList = goalListDto.Select(g => new GoalListDto()
            {
                Id = g.Id,
                TypeId = g.TypeId,
                GoalType =g.GoalType?.Type,
                Subject = g.Subject,
                TargetAchievement = g.TargetAchievement,
                StartDate = g.StartDate,
                EndDate = g.EndDate,
                Description = g.Description,
                Status = g.Status,
            });
            return Task.FromResult(goalList.ToList());
        }

        var  goalListDtos = _unitOfWork.GoalList.GlobalSearch(searchKey);
        var goalLists = goalListDtos.Select(goalList => new GoalListDto()
        {
            Id = goalList.Id,
            TypeId = goalList.TypeId,
            GoalType =goalList.GoalType != null ? goalList.GoalType.Type : null,
            Subject = goalList.Subject,
            TargetAchievement = goalList.TargetAchievement,
            StartDate = goalList.StartDate,
            EndDate = goalList.EndDate,
            Description = goalList.Description,
            Status = goalList.Status,
        });
        return Task.FromResult(goalLists.ToList());
    }

}
