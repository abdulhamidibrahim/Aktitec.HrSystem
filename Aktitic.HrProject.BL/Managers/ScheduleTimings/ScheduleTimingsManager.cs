
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
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ScheduleTimingsManager(
    UserUtility userUtility,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IScheduleTimingsManager
{
    public Task<int> Add(ScheduleTimingsAddDto scheduleTimingsAddDto)
    {
        var scheduleTimings = new ScheduleTiming
        {
            EmployeeId = scheduleTimingsAddDto.EmployeeId,
            JobId = scheduleTimingsAddDto.JobId,
            ScheduleDate1 = scheduleTimingsAddDto.ScheduleDate1,
            ScheduleDate2 = scheduleTimingsAddDto.ScheduleDate2,
            ScheduleDate3 = scheduleTimingsAddDto.ScheduleDate3,
            SelectTime1 = scheduleTimingsAddDto.SelectTime1,
            SelectTime2 = scheduleTimingsAddDto.SelectTime2,
            SelectTime3 = scheduleTimingsAddDto.SelectTime3,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
        };
        
        unitOfWork.ScheduleTimings.Add(scheduleTimings);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ScheduleTimingsUpdateDto scheduleTimingsUpdateDto, int id)
    {
        var scheduleTimings = unitOfWork.ScheduleTimings.GetById(id);
        
        if (scheduleTimings == null) return Task.FromResult(0);

        if (scheduleTimingsUpdateDto.SelectTime1.IsNullOrEmpty()) 
            scheduleTimings.SelectTime1 = scheduleTimingsUpdateDto.SelectTime1;
        if (scheduleTimingsUpdateDto.SelectTime2.IsNullOrEmpty()) 
            scheduleTimings.SelectTime2 = scheduleTimingsUpdateDto.SelectTime2;
        if (scheduleTimingsUpdateDto.SelectTime3.IsNullOrEmpty()) 
            scheduleTimings.SelectTime3 = scheduleTimingsUpdateDto.SelectTime3;
        if (scheduleTimingsUpdateDto.EmployeeId != 0)
            scheduleTimings.EmployeeId = scheduleTimingsUpdateDto.EmployeeId;
        if (scheduleTimingsUpdateDto.JobId != 0)
            scheduleTimings.JobId = scheduleTimingsUpdateDto.JobId;
        if (scheduleTimingsUpdateDto.ScheduleDate1 != default) 
            scheduleTimings.ScheduleDate1 = scheduleTimingsUpdateDto.ScheduleDate1;
        if (scheduleTimingsUpdateDto.ScheduleDate2 != default) 
            scheduleTimings.ScheduleDate2 = scheduleTimingsUpdateDto.ScheduleDate2;
        if (scheduleTimingsUpdateDto.ScheduleDate3 != default) 
            scheduleTimings.ScheduleDate3 = scheduleTimingsUpdateDto.ScheduleDate3;
        
        
        scheduleTimings.UpdatedAt = DateTime.Now;
        scheduleTimings.UpdatedBy = userUtility.GetUserId();
        
        unitOfWork.ScheduleTimings.Update(scheduleTimings);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var scheduleTimings = unitOfWork.ScheduleTimings.GetById(id);
        if (scheduleTimings==null) return Task.FromResult(0);
        scheduleTimings.IsDeleted = true;
        scheduleTimings.DeletedAt = DateTime.Now;
        scheduleTimings.DeletedBy = userUtility.GetUserId();
        unitOfWork.ScheduleTimings.Update(scheduleTimings);
        return unitOfWork.SaveChangesAsync();
    }

    public ScheduleTimingsReadDto? Get(int id)
    {
        var scheduleTimings = unitOfWork.ScheduleTimings.GetById(id);
        if (scheduleTimings == null) return null;
        
        return new ScheduleTimingsReadDto()
        {
            Id = scheduleTimings.Id,
            EmployeeId = scheduleTimings.EmployeeId,
            JobId = scheduleTimings.JobId,
            ScheduleDate1 = scheduleTimings.ScheduleDate1,
            ScheduleDate2 = scheduleTimings.ScheduleDate2,
            ScheduleDate3 = scheduleTimings.ScheduleDate3,
            SelectTime1 = scheduleTimings.SelectTime1,
            SelectTime2 = scheduleTimings.SelectTime2,
            SelectTime3 = scheduleTimings.SelectTime3,
           
            CreatedAt = scheduleTimings.CreatedAt,
            CreatedBy = scheduleTimings.CreatedBy,
            UpdatedAt = scheduleTimings.UpdatedAt,
            UpdatedBy = scheduleTimings.UpdatedBy,    
        };
    }

    public async Task<List<ScheduleTimingsReadDto>> GetAll()
    {
        var scheduleTimings = await unitOfWork.ScheduleTimings.GetAll();
        return scheduleTimings.Select(scheduleTimings => new ScheduleTimingsReadDto()
        {
            Id = scheduleTimings.Id,
            EmployeeId = scheduleTimings.EmployeeId,
            JobId = scheduleTimings.JobId,
            ScheduleDate1 = scheduleTimings.ScheduleDate1,
            ScheduleDate2 = scheduleTimings.ScheduleDate2,
            ScheduleDate3 = scheduleTimings.ScheduleDate3,
            SelectTime1 = scheduleTimings.SelectTime1,
            SelectTime2 = scheduleTimings.SelectTime2,
            SelectTime3 = scheduleTimings.SelectTime3,
           
            CreatedAt = scheduleTimings.CreatedAt,
            CreatedBy = scheduleTimings.CreatedBy,
            UpdatedAt = scheduleTimings.UpdatedAt,
            UpdatedBy = scheduleTimings.UpdatedBy,    
        }).ToList();
    }

   

    public async Task<FilteredScheduleTimingsDto> GetFilteredScheduleTimingsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var scheduleTimingsList = await unitOfWork.ScheduleTimings.GetAllScheduleTimings();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = scheduleTimingsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = scheduleTimingsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var scheduleTimingsDto = new List<ScheduleTimingsDto>();
            foreach (var scheduleTimings in paginatedResults)
            {
                scheduleTimingsDto.Add(new ScheduleTimingsDto()
                {
                    Id = scheduleTimings.Id,
                    EmployeeId = scheduleTimings.EmployeeId,
                    JobId = scheduleTimings.JobId,
                    ScheduleDate1 = scheduleTimings.ScheduleDate1,
                    ScheduleDate2 = scheduleTimings.ScheduleDate2,
                    ScheduleDate3 = scheduleTimings.ScheduleDate3,
                    SelectTime1 = scheduleTimings.SelectTime1,
                    SelectTime2 = scheduleTimings.SelectTime2,
                    SelectTime3 = scheduleTimings.SelectTime3,
                    Employee = mapper.Map<Employee, EmployeeDto>(scheduleTimings.Employee),
                    Job = mapper.Map<Job,JobsDto>(scheduleTimings.Job),
                    CreatedAt = scheduleTimings.CreatedAt,
                    CreatedBy = scheduleTimings.CreatedBy,
                    UpdatedAt = scheduleTimings.UpdatedAt,
                    UpdatedBy = scheduleTimings.UpdatedBy,    
                });
            }
            FilteredScheduleTimingsDto filteredBudgetsExpensesDto = new()
            {
                ScheduleTimingsDto = scheduleTimingsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (scheduleTimingsList != null)
        {
            IEnumerable<ScheduleTiming> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(scheduleTimingsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(scheduleTimingsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var scheduleTimingss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(scheduleTimingss);
            
            var mappedScheduleTimings = new List<ScheduleTimingsDto>();

            foreach (var scheduleTimings in paginatedResults)
            {
                
                mappedScheduleTimings.Add(new ScheduleTimingsDto()
                {
                    Id = scheduleTimings.Id,
                    EmployeeId = scheduleTimings.EmployeeId,
                    JobId = scheduleTimings.JobId,
                    ScheduleDate1 = scheduleTimings.ScheduleDate1,
                    ScheduleDate2 = scheduleTimings.ScheduleDate2,
                    ScheduleDate3 = scheduleTimings.ScheduleDate3,
                    SelectTime1 = scheduleTimings.SelectTime1,
                    SelectTime2 = scheduleTimings.SelectTime2,
                    SelectTime3 = scheduleTimings.SelectTime3,
                    Employee = mapper.Map<Employee, EmployeeDto>(scheduleTimings.Employee),
                    Job = mapper.Map<Job,JobsDto>(scheduleTimings.Job),
                    CreatedAt = scheduleTimings.CreatedAt,
                    CreatedBy = scheduleTimings.CreatedBy,
                    UpdatedAt = scheduleTimings.UpdatedAt,
                    UpdatedBy = scheduleTimings.UpdatedBy,    
                });
            }
            FilteredScheduleTimingsDto filteredScheduleTimingsDto = new()
            {
                ScheduleTimingsDto = mappedScheduleTimings,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredScheduleTimingsDto;
        }

        return new FilteredScheduleTimingsDto();
    }
    private IEnumerable<ScheduleTiming> ApplyFilter(IEnumerable<ScheduleTiming> scheduleTimings, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => scheduleTimings.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => scheduleTimings.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => scheduleTimings.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => scheduleTimings.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var scheduleTimingsValue) => ApplyNumericFilter(scheduleTimings, column, scheduleTimingsValue, operatorType),
            _ => scheduleTimings
        };
    }

    private IEnumerable<ScheduleTiming> ApplyNumericFilter(IEnumerable<ScheduleTiming> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var scheduleTimingsValue) && scheduleTimingsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var scheduleTimingsValue) && scheduleTimingsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var scheduleTimingsValue) && scheduleTimingsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var scheduleTimingsValue) && scheduleTimingsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var scheduleTimingsValue) && scheduleTimingsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var scheduleTimingsValue) && scheduleTimingsValue < value),
        _ => policys
    };
}


    public Task<List<ScheduleTimingsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<ScheduleTiming> enumerable = unitOfWork.ScheduleTimings.GetAllScheduleTimings().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var scheduleTimings = enumerable.Select(scheduleTimings => new ScheduleTimingsDto()
            {
                Id = scheduleTimings.Id,
                EmployeeId = scheduleTimings.EmployeeId,
                JobId = scheduleTimings.JobId,
                ScheduleDate1 = scheduleTimings.ScheduleDate1,
                ScheduleDate2 = scheduleTimings.ScheduleDate2,
                ScheduleDate3 = scheduleTimings.ScheduleDate3,
                SelectTime1 = scheduleTimings.SelectTime1,
                SelectTime2 = scheduleTimings.SelectTime2,
                SelectTime3 = scheduleTimings.SelectTime3,
                Employee = mapper.Map<Employee, EmployeeDto>(scheduleTimings.Employee),
                Job = mapper.Map<Job,JobsDto>(scheduleTimings.Job),
                CreatedAt = scheduleTimings.CreatedAt,
                CreatedBy = scheduleTimings.CreatedBy,
                UpdatedAt = scheduleTimings.UpdatedAt,
                UpdatedBy = scheduleTimings.UpdatedBy,    
            });
            return Task.FromResult(scheduleTimings.ToList());
        }

        var  queryable = unitOfWork.ScheduleTimings.GlobalSearch(searchKey);
        var scheduleTimingss = queryable.Select(scheduleTimings => new ScheduleTimingsDto()
        {
            Id = scheduleTimings.Id,
            EmployeeId = scheduleTimings.EmployeeId,
            JobId = scheduleTimings.JobId,
            ScheduleDate1 = scheduleTimings.ScheduleDate1,
            ScheduleDate2 = scheduleTimings.ScheduleDate2,
            ScheduleDate3 = scheduleTimings.ScheduleDate3,
            SelectTime1 = scheduleTimings.SelectTime1,
            SelectTime2 = scheduleTimings.SelectTime2,
            SelectTime3 = scheduleTimings.SelectTime3,
            Employee = mapper.Map<Employee, EmployeeDto>(scheduleTimings.Employee),
            Job = mapper.Map<Job,JobsDto>(scheduleTimings.Job),
            CreatedAt = scheduleTimings.CreatedAt,
            CreatedBy = scheduleTimings.CreatedBy,
            UpdatedAt = scheduleTimings.UpdatedAt,
            UpdatedBy = scheduleTimings.UpdatedBy,    
        });
        return Task.FromResult(scheduleTimingss.ToList());
    }

}
