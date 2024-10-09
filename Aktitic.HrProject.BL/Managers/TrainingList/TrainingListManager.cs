using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TrainingListManager:ITrainingListManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TrainingListManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(TrainingListAddDto trainingListAddDto)
    {
        var trainingList = new TrainingList()
        { 
            TrainingTypeId = trainingListAddDto.TrainingTypeId,
            TrainerId = trainingListAddDto.TrainerId,
            EmployeeId = trainingListAddDto.EmployeeId,
            Cost = trainingListAddDto.Cost,
            StartDate = trainingListAddDto.StartDate,
            EndDate = trainingListAddDto.EndDate,
            Description = trainingListAddDto.Description,
            Status = trainingListAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.TrainingList.Add(trainingList);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TrainingListUpdateDto trainingListUpdateDto, int id)
    {
        var trainingList = _unitOfWork.TrainingList.GetById(id);
        
        
        if (trainingList == null) return Task.FromResult(0);
        
        
        if(trainingListUpdateDto.TrainingTypeId != null) trainingList.TrainingTypeId = trainingListUpdateDto.TrainingTypeId;
        if(trainingListUpdateDto.TrainerId != null) trainingList.TrainerId = trainingListUpdateDto.TrainerId;
        if(trainingListUpdateDto.EmployeeId != null) trainingList.EmployeeId = trainingListUpdateDto.EmployeeId;
        if(trainingListUpdateDto.Cost != null) trainingList.Cost = trainingListUpdateDto.Cost;
        if(trainingListUpdateDto.StartDate != null) trainingList.StartDate = trainingListUpdateDto.StartDate;
        if(trainingListUpdateDto.EndDate != null) trainingList.EndDate = trainingListUpdateDto.EndDate;
        if(trainingListUpdateDto.Description != null) trainingList.Description = trainingListUpdateDto.Description;
        if(trainingListUpdateDto.Status != null) trainingList.Status = trainingListUpdateDto.Status;

        trainingList.UpdatedAt = DateTime.Now;
        
        _unitOfWork.TrainingList.Update(trainingList);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var training = _unitOfWork.TrainingList.GetById(id);
        if (training==null) return Task.FromResult(0);
        training.IsDeleted = true;
        training.DeletedAt = DateTime.Now;
        _unitOfWork.TrainingList.Update(training);
        return _unitOfWork.SaveChangesAsync();
    }

    public TrainingListReadDto? Get(int id)
    {
        var trainingList = _unitOfWork.TrainingList.GetWithEmployeeAndTrainer(id).FirstOrDefault();
        if (trainingList == null) return null;
        return new TrainingListReadDto()
        {
            Id = trainingList.Id,
            TrainingTypeId = trainingList.TrainingTypeId,
            TrainingType = _mapper.Map<TrainingType,TrainingTypeDto>(trainingList.TrainingType)?.Type,
            TrainerId = trainingList.TrainerId,
            Trainer = _mapper.Map<Trainer,TrainerDto>(trainingList.Trainer).FullName,
            EmployeeId = trainingList.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(trainingList.Employee),
            Cost = trainingList.Cost,
            StartDate = trainingList.StartDate,
            EndDate = trainingList.EndDate,
            Description = trainingList.Description,
            Status = trainingList.Status,
        };
    }

    public Task<List<TrainingListReadDto>> GetAll()
    {
        var trainingList = _unitOfWork.TrainingList.GetAllWithEmployeeAndTrainer();
         var trainingLists = trainingList.AsEnumerable();
        return Task.FromResult(trainingLists.Select(t => new TrainingListReadDto()
        {
            Id = t.Id,
            TrainingTypeId = t.TrainingTypeId,
            TrainingType = t.TrainingType?.Type,
            TrainerId = t.TrainerId,
            Trainer = t.Trainer?.FullName,
            EmployeeId = t.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(t.Employee),
            Cost = t.Cost,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Description = t.Description,
            Status = t.Status,

        }).ToList());
    }
    
     public async Task<FilteredTrainingListDto> GetFilteredTrainingListsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var trainingLists =  _unitOfWork.TrainingList.GetAllWithEmployeeAndTrainer();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = trainingLists.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var trainingListList = trainingLists.ToList();

            var paginatedResults = trainingListList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedTrainingLists = new List<TrainingListDto>();
            foreach (var trainingList in paginatedResults)
            {
                mappedTrainingLists.Add(new TrainingListDto()
                {
                    Id = trainingList.Id,
                    TrainingTypeId = trainingList.TrainingTypeId,
                    TrainingType = (trainingList.TrainingType)?.Type,
                    TrainerId = trainingList.TrainerId,
                    Trainer = (trainingList.Trainer)?.FullName,
                    EmployeeId = trainingList.EmployeeId,
                    Employee = _mapper.Map<Employee,EmployeeDto>(trainingList.Employee),
                    Cost = trainingList.Cost,
                    StartDate = trainingList.StartDate,
                    EndDate = trainingList.EndDate,
                    Description = trainingList.Description,
                    Status = trainingList.Status,
                });
            }
            FilteredTrainingListDto filteredTrainingListDto = new()
            {
                TrainingListDto = mappedTrainingLists,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredTrainingListDto;
        }

        if (trainingLists != null)
        {
            IEnumerable<TrainingList> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(trainingLists, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(trainingLists, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var trainingLists = paginatedResults.ToList();
            // var mappedTrainingList = _mapper.Map<IEnumerable<TrainingList>, IEnumerable<TrainingListDto>>(trainingLists);
            
            var mappedTrainingLists = new List<TrainingListDto>();

            foreach (var trainingList in paginatedResults)
            {
                
                mappedTrainingLists.Add(new TrainingListDto()
                {
                    Id = trainingList.Id,
                    TrainingTypeId = trainingList.TrainingTypeId,
                    TrainingType = (trainingList.TrainingType)?.Type,
                    TrainerId = trainingList.TrainerId,
                    Trainer = (trainingList.Trainer)?.FullName,
                    EmployeeId = trainingList.EmployeeId,
                    Employee = _mapper.Map<Employee,EmployeeDto>(trainingList.Employee),
                    Cost = trainingList.Cost,
                    StartDate = trainingList.StartDate,
                    EndDate = trainingList.EndDate,
                    Description = trainingList.Description,
                    Status = trainingList.Status,
                });
            }
            FilteredTrainingListDto filteredTrainingListDto = new()
            {
                TrainingListDto = mappedTrainingLists,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTrainingListDto;
        }

        return new FilteredTrainingListDto();
    }
    private IEnumerable<TrainingList> ApplyFilter(IEnumerable<TrainingList> trainingLists, string? column, string? value, string? operatorList)
    {
        // value2 ??= value;

        return operatorList switch
        {
            "contains" => trainingLists.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => trainingLists.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => trainingLists.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => trainingLists.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var trainingListValue) => ApplyNumericFilter(trainingLists, column, trainingListValue, operatorList),
            _ => trainingLists
        };
    }

    private IEnumerable<TrainingList> ApplyNumericFilter(IEnumerable<TrainingList> trainingLists, string? column, decimal? value, string? operatorList)
{
    return operatorList?.ToLower() switch
    {
        "eq" => trainingLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingListValue) && trainingListValue == value),
        "neq" => trainingLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingListValue) && trainingListValue != value),
        "gte" => trainingLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingListValue) && trainingListValue >= value),
        "gt" => trainingLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingListValue) && trainingListValue > value),
        "lte" => trainingLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingListValue) && trainingListValue <= value),
        "lt" => trainingLists.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainingListValue) && trainingListValue < value),
        _ => trainingLists
    };
}


    public Task<List<TrainingListDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<TrainingList> trainingListDto;
            trainingListDto = _unitOfWork.TrainingList.GetAllWithEmployeeAndTrainer()
                .AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var trainingList = trainingListDto.Select(t => new TrainingListDto()
            {
                Id = t.Id,
                TrainingTypeId = t.TrainingTypeId,
                TrainingType = (t.TrainingType)?.Type,
                TrainerId = t.TrainerId,
                Trainer = (t.Trainer)?.FullName,
                EmployeeId = t.EmployeeId,
                Employee = _mapper.Map<Employee,EmployeeDto>(t.Employee),
                Cost = t.Cost,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Description = t.Description,
                Status = t.Status,
            });
            return Task.FromResult(trainingList.ToList());
        }

        var  trainingListsDto = _unitOfWork.TrainingList.GlobalSearch(searchKey);
        var trainingLists = trainingListsDto.Select(t => new TrainingListDto()
        {
                
                Id = t.Id,
                TrainingTypeId = t.TrainingTypeId,
                TrainingType = t.TrainingType != null ? t.TrainingType.Type : null,
                TrainerId = t.TrainerId,
                Trainer = t.Trainer != null ? t.Trainer.FullName : null,
                EmployeeId = t.EmployeeId,
                Employee = _mapper.Map<Employee,EmployeeDto>(t.Employee),   
                Cost = t.Cost,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Description = t.Description,
                Status = t.Status,
        });
                
        return Task.FromResult(trainingLists.ToList());
    }

}
