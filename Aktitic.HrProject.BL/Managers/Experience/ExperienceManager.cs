
using System.Collections;
using System.Net;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ExperienceManager(
    // UserUtility userUtility,
    IUnitOfWork unitOfWork) : IExperienceManager
{
    public Task<int> Add(ExperienceAddDto experienceAddDto)
    {
        var experience = new Experience
        {
            ExperienceLevel = experienceAddDto.ExperienceLevel,
            Status = experienceAddDto.Status,
            // CreatedAt = DateTime.Now,
            // CreatedBy = userUtility.GetUserId(),
        };
        
        unitOfWork.Experiences.Add(experience);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ExperienceUpdateDto experienceUpdateDto, int id)
    {
        var experience = unitOfWork.Experiences.GetById(id);
        
        if (experience == null) return Task.FromResult(0);

        if (experienceUpdateDto.ExperienceLevel.IsNullOrEmpty()) 
            experience.ExperienceLevel = experienceUpdateDto.ExperienceLevel; 
        if (experienceUpdateDto.Status != experience.Status)
            experience.Status = experienceUpdateDto.Status;
        
        
        // experience.UpdatedAt = DateTime.Now;
        // experience.UpdatedBy = userUtility.GetUserId();
        
        unitOfWork.Experiences.Update(experience);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var experience = unitOfWork.Experiences.GetById(id);
        if (experience==null) return Task.FromResult(0);
        experience.IsDeleted = true;
        // experience.DeletedAt = DateTime.Now;
        // experience.DeletedBy = userUtility.GetUserId();
        unitOfWork.Experiences.Update(experience);
        return unitOfWork.SaveChangesAsync();
    }

    public ExperienceReadDto? Get(int id)
    {
        var experience = unitOfWork.Experiences.GetById(id);
        if (experience == null) return null;
        
        return new ExperienceReadDto()
        {
            Id = experience.Id,
            ExperienceLevel = experience.ExperienceLevel,
            Status = experience.Status,
            CreatedAt = experience.CreatedAt,
            CreatedBy = experience.CreatedBy,
            UpdatedAt = experience.UpdatedAt,
            UpdatedBy = experience.UpdatedBy,    
        };
    }

    public async Task<List<ExperienceReadDto>> GetAll()
    {
        var experience = await unitOfWork.Experiences.GetAll();
        return experience.Select(e => new ExperienceReadDto()
        {
            Id = e.Id,
            ExperienceLevel = e.ExperienceLevel,
            Status = e.Status,
            CreatedAt = e.CreatedAt,
            CreatedBy = e.CreatedBy,
            UpdatedAt = e.UpdatedAt,
            UpdatedBy = e.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredExperiencesDto> GetFilteredExperienceAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var experienceList = await unitOfWork.Experiences.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = experienceList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = experienceList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var experienceDto = new List<ExperienceDto>();
            foreach (var experience in paginatedResults)
            {
                experienceDto.Add(new ExperienceDto()
                {
                    Id = experience.Id,
                    ExperienceLevel = experience.ExperienceLevel,
                    
                    Status = experience.Status,
                   
                    CreatedAt = experience.CreatedAt,
                    CreatedBy = experience.CreatedBy,
                    UpdatedAt = experience.UpdatedAt,
                    UpdatedBy = experience.UpdatedBy,    
                });
            }
            FilteredExperiencesDto filteredBudgetsExpensesDto = new()
            {
                ExperienceDto = experienceDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (experienceList != null)
        {
            IEnumerable<Experience> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(experienceList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(experienceList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var experiences = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(experiences);
            
            var mappedExperience = new List<ExperienceDto>();

            foreach (var experience in paginatedResults)
            {
                
                mappedExperience.Add(new ExperienceDto()
                {
                    Id = experience.Id,
                    ExperienceLevel = experience.ExperienceLevel,
                 
                    Status = experience.Status,
                    
                    CreatedAt = experience.CreatedAt,
                    CreatedBy = experience.CreatedBy,
                    UpdatedAt = experience.UpdatedAt,
                    UpdatedBy = experience.UpdatedBy,    
                });
            }
            FilteredExperiencesDto filteredExperienceDto = new()
            {
                ExperienceDto = mappedExperience,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredExperienceDto;
        }

        return new FilteredExperiencesDto();
    }
    private IEnumerable<Experience> ApplyFilter(IEnumerable<Experience> experience, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => experience.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => experience.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => experience.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => experience.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var experienceValue) => ApplyNumericFilter(experience, column, experienceValue, operatorType),
            _ => experience
        };
    }

    private IEnumerable<Experience> ApplyNumericFilter(IEnumerable<Experience> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var experienceValue) && experienceValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var experienceValue) && experienceValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var experienceValue) && experienceValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var experienceValue) && experienceValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var experienceValue) && experienceValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var experienceValue) && experienceValue < value),
        _ => policys
    };
}


    public Task<List<ExperienceDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Experience> enumerable = unitOfWork.Experiences.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var experience = enumerable.Select(experience => new ExperienceDto()
            {
                Id = experience.Id, 
                ExperienceLevel = experience.ExperienceLevel,
               
                Status = experience.Status,
              
                CreatedAt = experience.CreatedAt,
                CreatedBy = experience.CreatedBy,
                UpdatedAt = experience.UpdatedAt,
                UpdatedBy = experience.UpdatedBy,    
            });
            return Task.FromResult(experience.ToList());
        }

        var  queryable = unitOfWork.Experiences.GlobalSearch(searchKey);
        var experiences = queryable.Select(experience => new ExperienceDto()
        {
            Id = experience.Id, 
            ExperienceLevel = experience.ExperienceLevel,
               
            Status = experience.Status,
              
            CreatedAt = experience.CreatedAt,
            CreatedBy = experience.CreatedBy,
            UpdatedAt = experience.UpdatedAt,
            UpdatedBy = experience.UpdatedBy,    
        });
        return Task.FromResult(experiences.ToList());
    }

}
