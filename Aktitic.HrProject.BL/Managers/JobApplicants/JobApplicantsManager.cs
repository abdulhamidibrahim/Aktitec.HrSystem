using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Utilities;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using File = System.IO.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class JobApplicantsManager(
    UserUtility userUtility,
    IWebHostEnvironment webHostEnvironment,
    IUnitOfWork unitOfWork) : IJobApplicantsManager
{
    public Task<int> Add(JobApplicantsAddDto jobApplicantsAddDto)
    {
        var jobApplicants = new JobApplicant
        {
            Name = jobApplicantsAddDto.Name,
            Email = jobApplicantsAddDto.Email,
            Phone = jobApplicantsAddDto.Phone,
            Status = jobApplicantsAddDto.Status,
            Date = jobApplicantsAddDto.Date,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
        };

        if (jobApplicantsAddDto.Resume is not null)
        {
            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads","resume", jobApplicantsAddDto.Resume.FileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var stream = new FileStream(path, FileMode.Create);
            jobApplicantsAddDto.Resume.CopyTo(stream);
            
            jobApplicants.Resume = Path.Combine(webHostEnvironment.WebRootPath, "uploads","resume", jobApplicantsAddDto.Resume.FileName);
        }
        
        unitOfWork.JobApplicants.Add(jobApplicants);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(JobApplicantsUpdateDto jobApplicantsUpdateDto, int id)
    {
        var jobApplicants = unitOfWork.JobApplicants.GetById(id);
        
        if (jobApplicants == null) return Task.FromResult(0);

        if (jobApplicantsUpdateDto.Name.IsNullOrEmpty()) jobApplicants.Name = jobApplicantsUpdateDto.Name;
        if (jobApplicantsUpdateDto.Email.IsNullOrEmpty()) jobApplicants.Email = jobApplicantsUpdateDto.Email;
        if (jobApplicantsUpdateDto.Status.IsNullOrEmpty()) jobApplicants.Status = jobApplicantsUpdateDto.Status;
        if (jobApplicantsUpdateDto.Phone.IsNullOrEmpty() ) jobApplicants.Phone= jobApplicantsUpdateDto.Phone;
        if (jobApplicantsUpdateDto.Date != jobApplicants.Date ) jobApplicants.Date = jobApplicantsUpdateDto.Date;

        
        var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, jobApplicants.Resume);

        // Delete the old image file if it exists
        if (File.Exists(oldImagePath))
        {
            try
            {
                File.Delete(oldImagePath);
            }
            catch (Exception ex)
            {
                // Log the exception (you might want to use a logging framework)
                Console.WriteLine($"Failed to delete old image: {ex.Message}");
            }
        }

        // Use the same path for the new image
        var unique = Path.GetDirectoryName(jobApplicants.Resume);
        var path = Path.Combine(webHostEnvironment.WebRootPath, unique);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var filePath = Path.Combine(path, jobApplicantsUpdateDto.Resume.FileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        jobApplicantsUpdateDto.Resume.CopyTo(fileStream);

        jobApplicants.Resume = Path.Combine(unique, jobApplicantsUpdateDto.Resume.FileName);
        
        
        jobApplicants.UpdatedAt = DateTime.Now;
        jobApplicants.UpdatedBy = userUtility.GetUserId();
        
        unitOfWork.JobApplicants.Update(jobApplicants);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var jobApplicants = unitOfWork.JobApplicants.GetById(id);
        if (jobApplicants==null) return Task.FromResult(0);
        jobApplicants.IsDeleted = true;
        jobApplicants.DeletedAt = DateTime.Now;
        jobApplicants.DeletedBy = userUtility.GetUserId();
        unitOfWork.JobApplicants.Update(jobApplicants);
        return unitOfWork.SaveChangesAsync();
    }

    public JobApplicantsReadDto? Get(int id)
    {
        var jobApplicants = unitOfWork.JobApplicants.GetById(id);
        if (jobApplicants == null) return null;
        
        return new JobApplicantsReadDto()
        {
            Id = jobApplicants.Id,
            Name = jobApplicants.Name,
            Email = jobApplicants.Email,
            Phone = jobApplicants.Phone,
            Status = jobApplicants.Status,
            Date = jobApplicants.Date,
            Resume = jobApplicants.Resume,
            CreatedAt = jobApplicants.CreatedAt,
            CreatedBy = jobApplicants.CreatedBy,
            UpdatedAt = jobApplicants.UpdatedAt,
            UpdatedBy = jobApplicants.UpdatedBy,    
        };
    }

    public async Task<List<JobApplicantsReadDto>> GetAll()
    {
        var jobApplicants = await unitOfWork.JobApplicants.GetAll();
        return jobApplicants.Select(jobApplicants => new JobApplicantsReadDto()
        {
            Id = jobApplicants.Id,
            Name = jobApplicants.Name,
            Email = jobApplicants.Email,
            Phone = jobApplicants.Phone,
            Status = jobApplicants.Status,
            Date = jobApplicants.Date,
            Resume = jobApplicants.Resume,
            CreatedAt = jobApplicants.CreatedAt,
            CreatedBy = jobApplicants.CreatedBy,
            UpdatedAt = jobApplicants.UpdatedAt,
            UpdatedBy = jobApplicants.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredJobApplicantsDto> GetFilteredJobApplicantsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var jobApplicantsList = await unitOfWork.JobApplicants.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = jobApplicantsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = jobApplicantsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var jobApplicantsDto = new List<JobApplicantsDto>();
            foreach (var jobApplicants in paginatedResults)
            {
                jobApplicantsDto.Add(new JobApplicantsDto()
                {
                    Id = jobApplicants.Id,
                    Name = jobApplicants.Name,
                    Email = jobApplicants.Email,
                    Phone = jobApplicants.Phone,
                    Status = jobApplicants.Status,
                    Date = jobApplicants.Date,
                    Resume = jobApplicants.Resume,
                    CreatedAt = jobApplicants.CreatedAt,
                    CreatedBy = jobApplicants.CreatedBy,
                    UpdatedAt = jobApplicants.UpdatedAt,
                    UpdatedBy = jobApplicants.UpdatedBy,      
                });
            }
            FilteredJobApplicantsDto filteredBudgetsExpensesDto = new()
            {
                JobApplicantsDto = jobApplicantsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (jobApplicantsList != null)
        {
            IEnumerable<JobApplicant> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(jobApplicantsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(jobApplicantsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var jobApplicantss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(jobApplicantss);
            
            var mappedJobApplicants = new List<JobApplicantsDto>();

            foreach (var jobApplicants in paginatedResults)
            {
                
                mappedJobApplicants.Add(new JobApplicantsDto()
                {
                    Id = jobApplicants.Id,
                    Name = jobApplicants.Name,
                    Email = jobApplicants.Email,
                    Phone = jobApplicants.Phone,
                    Status = jobApplicants.Status,
                    Date = jobApplicants.Date,
                    Resume = jobApplicants.Resume,
                    CreatedAt = jobApplicants.CreatedAt,
                    CreatedBy = jobApplicants.CreatedBy,
                    UpdatedAt = jobApplicants.UpdatedAt,
                    UpdatedBy = jobApplicants.UpdatedBy,    
                });
            }
            FilteredJobApplicantsDto filteredJobApplicantsDto = new()
            {
                JobApplicantsDto = mappedJobApplicants,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredJobApplicantsDto;
        }

        return new FilteredJobApplicantsDto();
    }
    private IEnumerable<JobApplicant> ApplyFilter(IEnumerable<JobApplicant> jobApplicants, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => jobApplicants.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => jobApplicants.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => jobApplicants.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => jobApplicants.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var jobApplicantsValue) => ApplyNumericFilter(jobApplicants, column, jobApplicantsValue, operatorType),
            _ => jobApplicants
        };
    }

    private IEnumerable<JobApplicant> ApplyNumericFilter(IEnumerable<JobApplicant> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobApplicantsValue) && jobApplicantsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobApplicantsValue) && jobApplicantsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobApplicantsValue) && jobApplicantsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobApplicantsValue) && jobApplicantsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobApplicantsValue) && jobApplicantsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var jobApplicantsValue) && jobApplicantsValue < value),
        _ => policys
    };
}


    public Task<List<JobApplicantsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<JobApplicant> enumerable = unitOfWork.JobApplicants.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var jobApplicants = enumerable.Select(jobApplicants => new JobApplicantsDto()
            {
                Id = jobApplicants.Id,
                Name = jobApplicants.Name,
                Email = jobApplicants.Email,
                Phone = jobApplicants.Phone,
                Status = jobApplicants.Status,
                Date = jobApplicants.Date,
                Resume = jobApplicants.Resume,
                CreatedAt = jobApplicants.CreatedAt,
                CreatedBy = jobApplicants.CreatedBy,
                UpdatedAt = jobApplicants.UpdatedAt,
                UpdatedBy = jobApplicants.UpdatedBy,    
            });
            return Task.FromResult(jobApplicants.ToList());
        }

        var  queryable = unitOfWork.JobApplicants.GlobalSearch(searchKey);
        var jobApplicantss = queryable.Select(jobApplicants => new JobApplicantsDto()
        {
            Id = jobApplicants.Id,
            Name = jobApplicants.Name,
            Email = jobApplicants.Email,
            Phone = jobApplicants.Phone,
            Status = jobApplicants.Status,
            Date = jobApplicants.Date,
            Resume = jobApplicants.Resume,
            CreatedAt = jobApplicants.CreatedAt,
            CreatedBy = jobApplicants.CreatedBy,
            UpdatedAt = jobApplicants.UpdatedAt,
            UpdatedBy = jobApplicants.UpdatedBy,    
        });
        return Task.FromResult(jobApplicantss.ToList());
    }

}
