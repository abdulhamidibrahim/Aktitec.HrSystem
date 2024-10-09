using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TrainingTypeManager:ITrainingTypeManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TrainingTypeManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(TrainingTypeAddDto trainingTypeAddDto)
    {
        var trainingType = new TrainingType()
        {
            Type = trainingTypeAddDto.Type,
            Description = trainingTypeAddDto.Description,
            Status = trainingTypeAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.TrainingType.Add(trainingType);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TrainingTypeUpdateDto trainingTypeUpdateDto, int id)
    {
        var trainingType = _unitOfWork.TrainingType.GetById(id);
        
        
        if (trainingType == null) return Task.FromResult(0);
        
        
        if(trainingTypeUpdateDto.Description != null) trainingType.Description = trainingTypeUpdateDto.Description;
        if(trainingTypeUpdateDto.Type != null) trainingType.Type = trainingTypeUpdateDto.Type;
        if(trainingTypeUpdateDto.Status != null) trainingType.Status = trainingTypeUpdateDto.Status;

        trainingType.UpdatedAt = DateTime.Now;
        
        _unitOfWork.TrainingType.Update(trainingType);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var type = _unitOfWork.TrainingType.GetById(id);
        if (type==null) return Task.FromResult(0);
        type.IsDeleted = true;
        type.DeletedAt = DateTime.Now;
        _unitOfWork.TrainingType.Update(type);
        return _unitOfWork.SaveChangesAsync();
    }

    public TrainingTypeReadDto? Get(int id)
    {
        var trainingType = _unitOfWork.TrainingType.GetById(id);
        if (trainingType == null) return null;
        return new TrainingTypeReadDto()
        {
            Id = trainingType.Id,
            Type = trainingType.Type,
            Description = trainingType.Description,
            Status = trainingType.Status,
        };
    }

    public Task<List<TrainingTypeReadDto>> GetAll()
    {
        var trainingType = _unitOfWork.TrainingType.GetAll();
        return Task.FromResult(trainingType.Result.Select(t => new TrainingTypeReadDto()
        {
            Id = t.Id,
            Type = t.Type,
            Description = t.Description,
            Status = t.Status,

        }).ToList());
    }
    
     public async Task<FilteredTrainingTypeDto> GetFilteredTrainingTypesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var trainingTypes = await _unitOfWork.TrainingType.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = trainingTypes.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var trainingTypeList = trainingTypes.ToList();

            var paginatedResults = trainingTypeList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedTrainingTypes = new List<TrainingTypeDto>();
            foreach (var trainingType in paginatedResults)
            {
                mappedTrainingTypes.Add(new TrainingTypeDto()
                {
                        Id = trainingType.Id,
                        Type = trainingType.Type,
                        Description = trainingType.Description,
                        Status = trainingType.Status,
                });
            }
            FilteredTrainingTypeDto filteredTrainingTypeDto = new()
            {
                TrainingTypeDto = mappedTrainingTypes,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredTrainingTypeDto;
        }

        if (trainingTypes != null)
        {
            IEnumerable<TrainingType> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(trainingTypes, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(trainingTypes, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var trainingTypes = paginatedResults.ToList();
            // var mappedTrainingType = _mapper.Map<IEnumerable<TrainingType>, IEnumerable<TrainingTypeDto>>(trainingTypes);
            
            var mappedTrainingTypes = new List<TrainingTypeDto>();

            foreach (var trainingType in paginatedResults)
            {
                
                mappedTrainingTypes.Add(new TrainingTypeDto()
                {
                    Id = trainingType.Id,
                    Type = trainingType.Type,
                    Description = trainingType.Description,
                    Status = trainingType.Status,
                });
            }
            FilteredTrainingTypeDto filteredTrainingTypeDto = new()
            {
                TrainingTypeDto = mappedTrainingTypes,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTrainingTypeDto;
        }

        return new FilteredTrainingTypeDto();
    }
    private IEnumerable<TrainingType> ApplyFilter(IEnumerable<TrainingType> trainingTypes, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => trainingTypes.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => trainingTypes.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => trainingTypes.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => trainingTypes.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var trainingTypeValue) => ApplyNumericFilter(trainingTypes, column, trainingTypeValue, operatorType),
            _ => trainingTypes
        };
    }

    private IEnumerable<TrainingType> ApplyNumericFilter(IEnumerable<TrainingType> trainingTypes, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => trainingTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingTypeValue) && trainingTypeValue == value),
        "neq" => trainingTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingTypeValue) && trainingTypeValue != value),
        "gte" => trainingTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingTypeValue) && trainingTypeValue >= value),
        "gt" => trainingTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingTypeValue) && trainingTypeValue > value),
        "lte" => trainingTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingTypeValue) && trainingTypeValue <= value),
        "lt" => trainingTypes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingTypeValue) && trainingTypeValue < value),
        _ => trainingTypes
    };
}


    public Task<List<TrainingTypeDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<TrainingType> trainingTypeDto;
            trainingTypeDto = _unitOfWork.TrainingType.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var trainingType = _mapper.Map<IEnumerable<TrainingType>, IEnumerable<TrainingTypeDto>>(trainingTypeDto);
            return Task.FromResult(trainingType.ToList());
        }

        var  trainingTypeDtos = _unitOfWork.TrainingType.GlobalSearch(searchKey);
        var trainingTypes = _mapper.Map<IEnumerable<TrainingType>, IEnumerable<TrainingTypeDto>>(trainingTypeDtos);
        return Task.FromResult(trainingTypes.ToList());
    }

}
