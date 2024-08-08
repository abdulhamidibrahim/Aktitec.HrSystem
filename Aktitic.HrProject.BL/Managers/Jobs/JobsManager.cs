
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
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class JobsManager(
    UserUtility userUtility,
    IUnitOfWork unitOfWork) : IJobsManager
{
    public Task<int> Add(JobsAddDto jobsAddDto)
    {
        var jobs = new Job()
        {
            JobTitle = jobsAddDto.JobTitle,
            DepartmentId = jobsAddDto.DepartmentId,
            JobLocation = jobsAddDto.JobLocation,
            Status = jobsAddDto.Status,
            NoOfVacancies = jobsAddDto.NoOfVacancies,
            Age = jobsAddDto.Age,
            Experience = jobsAddDto.Experience,
            SalaryFrom = jobsAddDto.SalaryFrom,
            SalaryTo = jobsAddDto.SalaryTo,
            JobType = jobsAddDto.JobType,
            StartDate = jobsAddDto.StartDate,
            ExpiredDate = jobsAddDto.ExpiredDate,
            Description = jobsAddDto.Description,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserName(),
        };
        
        unitOfWork.Jobs.Add(jobs);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(JobsUpdateDto jobsUpdateDto, int id)
    {
        var jobs = unitOfWork.Jobs.GetById(id);
        
        if (jobs == null) return Task.FromResult(0);

        if (jobsUpdateDto.JobTitle.IsNullOrEmpty()) jobs.JobTitle = jobsUpdateDto.JobTitle;
        if (jobsUpdateDto.JobLocation.IsNullOrEmpty()) jobs.JobLocation = jobsUpdateDto.JobLocation;
        if (jobsUpdateDto.DepartmentId != 0) jobs.DepartmentId = jobsUpdateDto.DepartmentId;
        if (jobsUpdateDto.Status.IsNullOrEmpty()) jobs.Status = jobsUpdateDto.Status;
        if (jobsUpdateDto.StartDate != jobs.StartDate )                  
            jobs.StartDate = jobsUpdateDto.StartDate;
        if (jobsUpdateDto.ExpiredDate != jobs.ExpiredDate ) jobs.ExpiredDate = jobsUpdateDto.ExpiredDate;
        if (jobsUpdateDto.JobType.IsNullOrEmpty() ) jobs.JobType= jobsUpdateDto.JobType;
        if (jobsUpdateDto.NoOfVacancies.IsNullOrEmpty()) jobs.NoOfVacancies = jobsUpdateDto.NoOfVacancies;
        if (jobsUpdateDto.Experience.IsNullOrEmpty() ) jobs.Experience = jobsUpdateDto.Experience;
        if (jobsUpdateDto.Description.IsNullOrEmpty() ) jobs.Description = jobsUpdateDto.Description;
        if (jobsUpdateDto.SalaryFrom != jobs.SalaryFrom && jobsUpdateDto.SalaryFrom != 0 )
            jobs.SalaryFrom = jobsUpdateDto.SalaryFrom;
        if (jobsUpdateDto.SalaryTo != jobs.SalaryTo && jobsUpdateDto.SalaryTo != 0) 
            jobs.SalaryTo = jobsUpdateDto.SalaryTo;
        if (jobsUpdateDto.Age != jobs.Age && jobsUpdateDto.Age != 0) 
            jobs.SalaryTo = jobsUpdateDto.SalaryTo;

        
        jobs.UpdatedAt = DateTime.Now;
        jobs.UpdatedBy = userUtility.GetUserName();
        
        unitOfWork.Jobs.Update(jobs);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var jobs = unitOfWork.Jobs.GetById(id);
        if (jobs==null) return Task.FromResult(0);
        jobs.IsDeleted = true;
        jobs.DeletedAt = DateTime.Now;
        jobs.DeletedBy = userUtility.GetUserName();
        unitOfWork.Jobs.Update(jobs);
        return unitOfWork.SaveChangesAsync();
    }

    public JobsReadDto? Get(int id)
    {
        var jobs = unitOfWork.Jobs.GetById(id);
        if (jobs == null) return null;
        
        return new JobsReadDto()
        {
            Id = jobs.Id,
            JobTitle = jobs.JobTitle,
            DepartmentId = jobs.DepartmentId,
            JobLocation = jobs.JobLocation,
            NoOfVacancies = jobs.NoOfVacancies,
            Age = jobs.Age,
            Experience = jobs.Experience,
            SalaryFrom = jobs.SalaryFrom,
            SalaryTo = jobs.SalaryTo,
            JobType = jobs.JobType,
            Status = jobs.Status,
            StartDate = jobs.StartDate,
            ExpiredDate = jobs.ExpiredDate,
            Description = jobs.Description,
            CreatedAt = jobs.CreatedAt,
            CreatedBy = jobs.CreatedBy,
            UpdatedAt = jobs.UpdatedAt,
            UpdatedBy = jobs.UpdatedBy,    
        };
    }

    public async Task<List<JobsReadDto>> GetAll()
    {
        var jobs = await unitOfWork.Jobs.GetAll();
        return jobs.Select(jobs => new JobsReadDto()
        {
            Id = jobs.Id,
            JobTitle = jobs.JobTitle,
            DepartmentId = jobs.DepartmentId,
            JobLocation = jobs.JobLocation,
            NoOfVacancies = jobs.NoOfVacancies,
            Age = jobs.Age,
            Experience = jobs.Experience,
            SalaryFrom = jobs.SalaryFrom,
            SalaryTo = jobs.SalaryTo,
            JobType = jobs.JobType,
            Status = jobs.Status,
            StartDate = jobs.StartDate,
            ExpiredDate = jobs.ExpiredDate,
            Description = jobs.Description,
            CreatedAt = jobs.CreatedAt,
            CreatedBy = jobs.CreatedBy,
            UpdatedAt = jobs.UpdatedAt,
            UpdatedBy = jobs.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredJobsDto> GetFilteredJobsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var jobsList = await unitOfWork.Jobs.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = jobsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = jobsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var jobsDto = new List<JobsDto>();
            foreach (var jobs in paginatedResults)
            {
                jobsDto.Add(new JobsDto()
                {
                    Id = jobs.Id,
                    JobTitle = jobs.JobTitle,
                    DepartmentId = jobs.DepartmentId,
                    JobLocation = jobs.JobLocation,
                    NoOfVacancies = jobs.NoOfVacancies,
                    Age = jobs.Age,
                    Experience = jobs.Experience,
                    SalaryFrom = jobs.SalaryFrom,
                    SalaryTo = jobs.SalaryTo,
                    JobType = jobs.JobType,
                    Status = jobs.Status,
                    StartDate = jobs.StartDate,
                    ExpiredDate = jobs.ExpiredDate,
                    Description = jobs.Description,
                    CreatedAt = jobs.CreatedAt,
                    CreatedBy = jobs.CreatedBy,
                    UpdatedAt = jobs.UpdatedAt,
                    UpdatedBy = jobs.UpdatedBy,    
                });
            }
            FilteredJobsDto filteredBudgetsExpensesDto = new()
            {
                JobsDto = jobsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (jobsList != null)
        {
            IEnumerable<Job> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(jobsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(jobsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var jobss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(jobss);
            
            var mappedJobs = new List<JobsDto>();

            foreach (var jobs in paginatedResults)
            {
                
                mappedJobs.Add(new JobsDto()
                {
                    Id = jobs.Id,
                    JobTitle = jobs.JobTitle,
                    DepartmentId = jobs.DepartmentId,
                    JobLocation = jobs.JobLocation,
                    NoOfVacancies = jobs.NoOfVacancies,
                    Age = jobs.Age,
                    Experience = jobs.Experience,
                    SalaryFrom = jobs.SalaryFrom,
                    SalaryTo = jobs.SalaryTo,
                    JobType = jobs.JobType,
                    Status = jobs.Status,
                    StartDate = jobs.StartDate,
                    ExpiredDate = jobs.ExpiredDate,
                    Description = jobs.Description,
                    CreatedAt = jobs.CreatedAt,
                    CreatedBy = jobs.CreatedBy,
                    UpdatedAt = jobs.UpdatedAt,
                    UpdatedBy = jobs.UpdatedBy,    
                });
            }
            FilteredJobsDto filteredJobsDto = new()
            {
                JobsDto = mappedJobs,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredJobsDto;
        }

        return new FilteredJobsDto();
    }
    private IEnumerable<Job> ApplyFilter(IEnumerable<Job> jobs, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => jobs.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => jobs.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => jobs.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => jobs.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var jobsValue) => ApplyNumericFilter(jobs, column, jobsValue, operatorType),
            _ => jobs
        };
    }

    private IEnumerable<Job> ApplyNumericFilter(IEnumerable<Job> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobsValue) && jobsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobsValue) && jobsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobsValue) && jobsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobsValue) && jobsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobsValue) && jobsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobsValue) && jobsValue < value),
        _ => policys
    };
}


    public Task<List<JobsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Job> enumerable = unitOfWork.Jobs.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var jobs = enumerable.Select(jobs => new JobsDto()
            {
                Id = jobs.Id,
                JobTitle = jobs.JobTitle,
                DepartmentId = jobs.DepartmentId,
                JobLocation = jobs.JobLocation,
                NoOfVacancies = jobs.NoOfVacancies,
                Age = jobs.Age,
                Experience = jobs.Experience,
                SalaryFrom = jobs.SalaryFrom,
                SalaryTo = jobs.SalaryTo,
                JobType = jobs.JobType,
                Status = jobs.Status,
                StartDate = jobs.StartDate,
                ExpiredDate = jobs.ExpiredDate,
                Description = jobs.Description,
                CreatedAt = jobs.CreatedAt,
                CreatedBy = jobs.CreatedBy,
                UpdatedAt = jobs.UpdatedAt,
                UpdatedBy = jobs.UpdatedBy,    
            });
            return Task.FromResult(jobs.ToList());
        }

        var  queryable = unitOfWork.Jobs.GlobalSearch(searchKey);
        var jobss = queryable.Select(jobs => new JobsDto()
        {
            Id = jobs.Id,
            JobTitle = jobs.JobTitle,
            DepartmentId = jobs.DepartmentId,
            JobLocation = jobs.JobLocation,
            NoOfVacancies = jobs.NoOfVacancies,
            Age = jobs.Age,
            Experience = jobs.Experience,
            SalaryFrom = jobs.SalaryFrom,
            SalaryTo = jobs.SalaryTo,
            JobType = jobs.JobType,
            Status = jobs.Status,
            StartDate = jobs.StartDate,
            ExpiredDate = jobs.ExpiredDate,
            Description = jobs.Description,
            CreatedAt = jobs.CreatedAt,
            CreatedBy = jobs.CreatedBy,
            UpdatedAt = jobs.UpdatedAt,
            UpdatedBy = jobs.UpdatedBy,    
        });
        return Task.FromResult(jobss.ToList());
    }

}
