
using System.Collections;
using System.Net;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ShortlistManager(
    UserUtility userUtility,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IShortlistsManager
{
    public Task<int> Add(ShortlistAddDto assetsAddDto)
    {
        var assets = new Shortlist
        {
            EmployeeId = assetsAddDto.EmployeeId,
            JobId = assetsAddDto.JobId,
            Status = assetsAddDto.Status,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
        };
        
        unitOfWork.Shortlists.Add(assets);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ShortlistUpdateDto assetsUpdateDto, int id)
    {
        var assets = unitOfWork.Shortlists.GetById(id);
        
        if (assets == null) return Task.FromResult(0);
        
        assets.EmployeeId = assetsUpdateDto.EmployeeId;
        assets.JobId = assetsUpdateDto.JobId;
        assets.Status = assetsUpdateDto.Status;
        
        
        assets.UpdatedAt = DateTime.Now;
        assets.UpdatedBy = userUtility.GetUserId();
        
        unitOfWork.Shortlists.Update(assets);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var assets = unitOfWork.Shortlists.GetById(id);
        if (assets==null) return Task.FromResult(0);
        assets.IsDeleted = true;
        assets.DeletedAt = DateTime.Now;
        assets.DeletedBy = userUtility.GetUserId();
        unitOfWork.Shortlists.Update(assets);
        return unitOfWork.SaveChangesAsync();
    }

    public ShortlistReadDto? Get(int id)
    {
        var assets = unitOfWork.Shortlists.GetById(id);
        if (assets == null) return null;
        
        return new ShortlistReadDto()
        {
            Id = assets.Id,
            JobId = assets.JobId,
            EmployeeId = assets.EmployeeId,
            Status = assets.Status,
            CreatedAt = assets.CreatedAt,
            CreatedBy = assets.CreatedBy,
            UpdatedAt = assets.UpdatedAt,
            UpdatedBy = assets.UpdatedBy,    
        };
    }

    public async Task<List<ShortlistReadDto>> GetAll()
    {
        var assets = await unitOfWork.Shortlists.GetAll();
        return assets.Select(assets => new ShortlistReadDto()
        {
            Id = assets.Id,
            JobId = assets.JobId,
            EmployeeId = assets.EmployeeId,
            Status = assets.Status,
            CreatedAt = assets.CreatedAt,
            CreatedBy = assets.CreatedBy,
            UpdatedAt = assets.UpdatedAt,
            UpdatedBy = assets.UpdatedBy,    
            
        }).ToList();
    }


    public async Task<FilteredShortlistsDto> GetFilteredShortlistsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var assetsList = await unitOfWork.Shortlists.GetAllShortlists();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = assetsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = assetsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var assetsDto = new List<ShortlistDto>();
            foreach (var assets in paginatedResults)
            {
                assetsDto.Add(new ShortlistDto()
                {
                    Id = assets.Id,
                    JobId = assets.JobId,
                    EmployeeId = assets.EmployeeId,
                    Status = assets.Status,
                    Job = mapper.Map<Job,JobsDto>(assets.Job),
                    Employee = mapper.Map<Employee,EmployeeDto>(assets.Employee),
                    CreatedAt = assets.CreatedAt,
                    CreatedBy = assets.CreatedBy,
                    UpdatedAt = assets.UpdatedAt,
                    UpdatedBy = assets.UpdatedBy,    
                });
            }
            FilteredShortlistsDto filteredBudgetsExpensesDto = new()
            {
                ShortlistDtos = assetsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (assetsList != null)
        {
            IEnumerable<Shortlist> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(assetsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(assetsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var assetss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(assetss);
            
            var mappedShortlist = new List<ShortlistDto>();

            foreach (var assets in paginatedResults)
            {
                
                mappedShortlist.Add(new ShortlistDto()
                {
                    Id = assets.Id,
                    JobId = assets.JobId,
                    EmployeeId = assets.EmployeeId,
                    Status = assets.Status,
                    Job = mapper.Map<Job,JobsDto>(assets.Job),
                    Employee = mapper.Map<Employee,EmployeeDto>(assets.Employee),
                    CreatedAt = assets.CreatedAt,
                    CreatedBy = assets.CreatedBy,
                    UpdatedAt = assets.UpdatedAt,
                    UpdatedBy = assets.UpdatedBy,    
                });
            }
            FilteredShortlistsDto filteredShortlistDto = new()
            {
                ShortlistDtos = mappedShortlist,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredShortlistDto;
        }

        return new FilteredShortlistsDto();
    }
    private IEnumerable<Shortlist> ApplyFilter(IEnumerable<Shortlist> assets, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => assets.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => assets.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => assets.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => assets.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var assetsValue) => ApplyNumericFilter(assets, column, assetsValue, operatorType),
            _ => assets
        };
    }

    private IEnumerable<Shortlist> ApplyNumericFilter(IEnumerable<Shortlist> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var assetsValue) && assetsValue < value),
        _ => policys
    };
}


    public Task<List<ShortlistDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Shortlist> enumerable = unitOfWork.Shortlists.GetAllShortlists().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var assets = enumerable.Select(assets => new ShortlistDto()
            {
                Id = assets.Id,
                JobId = assets.JobId,
                EmployeeId = assets.EmployeeId,
                Status = assets.Status,
                Job = mapper.Map<Job,JobsDto>(assets.Job),
                Employee = mapper.Map<Employee,EmployeeDto>(assets.Employee),
                CreatedAt = assets.CreatedAt,
                CreatedBy = assets.CreatedBy,
                UpdatedAt = assets.UpdatedAt,
                UpdatedBy = assets.UpdatedBy,    
            });
            return Task.FromResult(assets.ToList());
        }

        var  queryable = unitOfWork.Shortlists.GlobalSearch(searchKey);
        var assetss = queryable.Select(assets => new ShortlistDto()
        {
            Id = assets.Id,
            JobId = assets.JobId,
            EmployeeId = assets.EmployeeId,
            Status = assets.Status,
            Job = mapper.Map<Job,JobsDto>(assets.Job),
            Employee = mapper.Map<Employee,EmployeeDto>(assets.Employee),
            CreatedAt = assets.CreatedAt,
            CreatedBy = assets.CreatedBy,
            UpdatedAt = assets.UpdatedAt,
            UpdatedBy = assets.UpdatedBy,    
        });
        return Task.FromResult(assetss.ToList());
    }

}
