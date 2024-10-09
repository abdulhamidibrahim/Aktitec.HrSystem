using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TrainerManager:ITrainerManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TrainerManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(TrainerAddDto trainerAddDto)
    {
        var trainer = new Trainer()
        {
            FirstName = trainerAddDto.FirstName,
            LastName = trainerAddDto.LastName,
            Role = trainerAddDto.Role,
            Email = trainerAddDto.Email,
            Phone = trainerAddDto.Phone,
            Description = trainerAddDto.Description,
            Status = trainerAddDto.Status,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Trainer.Add(trainer);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TrainerUpdateDto trainerUpdateDto, int id)
    {
        var trainer = _unitOfWork.Trainer.GetById(id);
        
        
        if (trainer == null) return Task.FromResult(0);
        
        if(trainerUpdateDto.FirstName != null) trainer.FirstName = trainerUpdateDto.FirstName;
        if(trainerUpdateDto.LastName != null) trainer.LastName = trainerUpdateDto.LastName;
        if(trainerUpdateDto.Role != null) trainer.Role = trainerUpdateDto.Role;
        if(trainerUpdateDto.Description != null) trainer.Description = trainerUpdateDto.Description;
        if(trainerUpdateDto.Email != null) trainer.Email = trainerUpdateDto.Email;
        if(trainerUpdateDto.Phone != null) trainer.Phone = trainerUpdateDto.Phone;
        if(trainerUpdateDto.Status != null) trainer.Status = trainerUpdateDto.Status;

        trainer.UpdatedAt = DateTime.Now;
         _unitOfWork.Trainer.Update(trainer);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var trainer = _unitOfWork.Trainer.GetById(id);
        if (trainer==null) return Task.FromResult(0);
        trainer.IsDeleted = true;
        trainer.DeletedAt = DateTime.Now;
        _unitOfWork.Trainer.Update(trainer);
        return _unitOfWork.SaveChangesAsync();
    }

    public TrainerReadDto? Get(int id)
    {
        var trainer = _unitOfWork.Trainer.GetById(id);
        if (trainer == null) return null;
        return new TrainerReadDto()
        {
            Id = trainer.Id,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            Role = trainer.Role,
            Email = trainer.Email,
            Phone = trainer.Phone,
            Description = trainer.Description,
            Status = trainer.Status,
        };
    }

    public Task<List<TrainerReadDto>> GetAll()
    {
        var trainer = _unitOfWork.Trainer.GetAll();
        return Task.FromResult(trainer.Result.Select(t => new TrainerReadDto()
        {
            Id = t.Id,
            FirstName = t.FirstName,
            LastName = t.LastName,
            Role = t.Role,
            Email = t.Email,
            Phone = t.Phone,
            Description = t.Description,
            Status = t.Status,

        }).ToList());
    }
    
     public async Task<FilteredTrainerDto> GetFilteredTrainersAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var trainers = await _unitOfWork.Trainer.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = trainers.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var trainerList = trainers.ToList();

            var paginatedResults = trainerList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedTrainers = new List<TrainerDto>();
            foreach (var trainer in paginatedResults)
            {
                mappedTrainers.Add(new TrainerDto()
                {
                        Id = trainer.Id,
                        FirstName = trainer.FirstName,
                        LastName = trainer.LastName,
                        Role = trainer.Role,
                        Email = trainer.Email,
                        Phone = trainer.Phone,
                        Description = trainer.Description,
                        Status = trainer.Status,
                });
            }
            FilteredTrainerDto filteredTrainerDto = new()
            {
                TrainerDto = mappedTrainers,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredTrainerDto;
        }

        if (trainers != null)
        {
            IEnumerable<Trainer> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(trainers, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(trainers, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var trainers = paginatedResults.ToList();
            // var mappedTrainer = _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerDto>>(trainers);
            
            var mappedTrainers = new List<TrainerDto>();

            foreach (var trainer in paginatedResults)
            {
                
                mappedTrainers.Add(new TrainerDto()
                {
                    Id = trainer.Id,
                    FirstName = trainer.FirstName,
                    LastName = trainer.LastName,
                    Role = trainer.Role,
                    Email = trainer.Email,
                    Phone = trainer.Phone,
                    Description = trainer.Description,
                    Status = trainer.Status,
                });
            }
            FilteredTrainerDto filteredTrainerDto = new()
            {
                TrainerDto = mappedTrainers,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTrainerDto;
        }

        return new FilteredTrainerDto();
    }
    private IEnumerable<Trainer> ApplyFilter(IEnumerable<Trainer> trainers, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => trainers.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => trainers.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => trainers.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => trainers.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var trainerValue) => ApplyNumericFilter(trainers, column, trainerValue, operatorType),
            _ => trainers
        };
    }

    private IEnumerable<Trainer> ApplyNumericFilter(IEnumerable<Trainer> trainers, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => trainers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainerValue) && trainerValue == value),
        "neq" => trainers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainerValue) && trainerValue != value),
        "gte" => trainers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainerValue) && trainerValue >= value),
        "gt" => trainers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainerValue) && trainerValue > value),
        "lte" => trainers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainerValue) && trainerValue <= value),
        "lt" => trainers.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var trainerValue) && trainerValue < value),
        _ => trainers
    };
}


    public Task<List<TrainerDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Trainer> trainerDto;
            trainerDto = _unitOfWork.Trainer.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var trainer = _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerDto>>(trainerDto);
            return Task.FromResult(trainer.ToList());
        }

        var  trainerDtos = _unitOfWork.Trainer.GlobalSearch(searchKey);
        var trainers = _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerDto>>(trainerDtos);
        return Task.FromResult(trainers.ToList());
    }

}
