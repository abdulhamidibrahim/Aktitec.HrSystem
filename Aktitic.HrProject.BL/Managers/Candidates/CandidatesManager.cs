
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

public class CandidatesManager(
    UserUtility userUtility,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICandidatesManager
{
    public Task<int> Add(CandidatesAddDto candidatesAddDto)
    {
        var candidates = new Candidate
        {
            FirstName = candidatesAddDto.FirstName,
            LastName = candidatesAddDto.LastName,
            Email = candidatesAddDto.Email,
            EmployeeId = candidatesAddDto.EmployeeId,
            Phone = candidatesAddDto.Phone,
            CreatedAt = DateTime.Now,
            CreatedBy = userUtility.GetUserId(),
        };
        
        unitOfWork.Candidates.Add(candidates);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(CandidatesUpdateDto candidatesUpdateDto, int id)
    {
        var candidates = unitOfWork.Candidates.GetById(id);
        
        if (candidates == null) return Task.FromResult(0);

        if (candidatesUpdateDto.FirstName.IsNullOrEmpty()) candidates.FirstName = candidatesUpdateDto.FirstName;
        if (candidatesUpdateDto.LastName.IsNullOrEmpty()) candidates.LastName = candidatesUpdateDto.LastName;
        if (candidatesUpdateDto.Email.IsNullOrEmpty()) candidates.Email = candidatesUpdateDto.Email;
        if (candidatesUpdateDto.Phone.IsNullOrEmpty()) candidates.Phone = candidatesUpdateDto.Phone;
        if (candidatesUpdateDto.EmployeeId != 0) candidates.EmployeeId = candidatesUpdateDto.EmployeeId;
       
        
        candidates.UpdatedAt = DateTime.Now;
        candidates.UpdatedBy = userUtility.GetUserId();
        
        unitOfWork.Candidates.Update(candidates);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var candidates = unitOfWork.Candidates.GetById(id);
        if (candidates==null) return Task.FromResult(0);
        candidates.IsDeleted = true;
        candidates.DeletedAt = DateTime.Now;
        candidates.DeletedBy = userUtility.GetUserId();
        unitOfWork.Candidates.Update(candidates);
        return unitOfWork.SaveChangesAsync();
    }

    public CandidatesReadDto? Get(int id)
    {
        var candidates = unitOfWork.Candidates.GetById(id);
        if (candidates == null) return null;
        
        return new CandidatesReadDto()
        {
            Id = candidates.Id,
            FirstName = candidates.FirstName,
            LastName = candidates.LastName,
            Phone = candidates.Phone,
            Email = candidates.Email,
            EmployeeId = candidates.EmployeeId,
           
            CreatedAt = candidates.CreatedAt,
            CreatedBy = candidates.CreatedBy,
            UpdatedAt = candidates.UpdatedAt,
            UpdatedBy = candidates.UpdatedBy,    
        };
    }

    public async Task<List<CandidatesReadDto>> GetAll()
    {
        var candidates = await unitOfWork.Candidates.GetAll();
        return candidates.Select(candidates => new CandidatesReadDto()
        {
            Id = candidates.Id,
            FirstName = candidates.FirstName,
            LastName = candidates.LastName,
            Phone = candidates.Phone,
            Email = candidates.Email,
            EmployeeId = candidates.EmployeeId,
           
            CreatedAt = candidates.CreatedAt,
            CreatedBy = candidates.CreatedBy,
            UpdatedAt = candidates.UpdatedAt,
            UpdatedBy = candidates.UpdatedBy,    
            
        }).ToList();
    }

   

    public async Task<FilteredCandidatesDto> GetFilteredCandidatesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var candidatesList = await unitOfWork.Candidates.GetAllCandidates();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = candidatesList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = candidatesList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var candidatesDto = new List<CandidatesDto>();
            foreach (var candidates in paginatedResults)
            {
                    candidatesDto.Add(new CandidatesDto()
                    {
                        Id = candidates.Id,
                        FirstName = candidates.FirstName,
                        LastName = candidates.LastName,
                        Phone = candidates.Phone,
                        Email = candidates.Email,
                        EmployeeId = candidates.EmployeeId,
                        Employee = mapper.Map<Employee, EmployeeDto>(candidates.Employee),
                        CreatedAt = candidates.CreatedAt,
                        CreatedBy = candidates.CreatedBy,
                        UpdatedAt = candidates.UpdatedAt,
                        UpdatedBy = candidates.UpdatedBy,
                    });
            }
            FilteredCandidatesDto filteredBudgetsExpensesDto = new()
            {
                CandidatesDto = candidatesDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (candidatesList != null)
        {
            IEnumerable<Candidate> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(candidatesList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(candidatesList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var candidatess = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(candidatess);
            
            var mappedCandidates = new List<CandidatesDto>();

            foreach (var candidates in paginatedResults)
            {
                
                mappedCandidates.Add(new CandidatesDto()
                {
                    Id = candidates.Id,
                    FirstName = candidates.FirstName,
                    LastName = candidates.LastName,
                    Phone = candidates.Phone,
                    Email = candidates.Email,
                    EmployeeId = candidates.EmployeeId,
                    Employee = mapper.Map<Employee, EmployeeDto>(candidates.Employee),
                    CreatedAt = candidates.CreatedAt,
                    CreatedBy = candidates.CreatedBy,
                    UpdatedAt = candidates.UpdatedAt,
                    UpdatedBy = candidates.UpdatedBy,    
                });
            }
            FilteredCandidatesDto filteredCandidatesDto = new()
            {
                CandidatesDto = mappedCandidates,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredCandidatesDto;
        }

        return new FilteredCandidatesDto();
    }
    private IEnumerable<Candidate> ApplyFilter(IEnumerable<Candidate> candidates, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => candidates.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => candidates.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => candidates.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => candidates.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var candidatesValue) => ApplyNumericFilter(candidates, column, candidatesValue, operatorType),
            _ => candidates
        };
    }

    private IEnumerable<Candidate> ApplyNumericFilter(IEnumerable<Candidate> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var candidatesValue) && candidatesValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var candidatesValue) && candidatesValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var candidatesValue) && candidatesValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var candidatesValue) && candidatesValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var candidatesValue) && candidatesValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var candidatesValue) && candidatesValue < value),
        _ => policys
    };
}


    public Task<List<CandidatesDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Candidate> enumerable = unitOfWork.Candidates.GetAllCandidates().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var candidates = enumerable.Select(candidates => new CandidatesDto()
            {
                Id = candidates.Id,
                FirstName = candidates.FirstName,
                LastName = candidates.LastName,
                Phone = candidates.Phone,
                Email = candidates.Email,
                EmployeeId = candidates.EmployeeId,
                Employee = mapper.Map<Employee, EmployeeDto>(candidates.Employee),
                CreatedAt = candidates.CreatedAt,
                CreatedBy = candidates.CreatedBy,
                UpdatedAt = candidates.UpdatedAt,
                UpdatedBy = candidates.UpdatedBy,    
            });
            return Task.FromResult(candidates.ToList());
        }

        var  queryable = unitOfWork.Candidates.GlobalSearch(searchKey);
        var candidatess = queryable.Select(candidates => new CandidatesDto()
        {
            Id = candidates.Id,
            FirstName = candidates.FirstName,
            LastName = candidates.LastName,
            Phone = candidates.Phone,
            Email = candidates.Email,
            EmployeeId = candidates.EmployeeId,
            Employee = mapper.Map<Employee, EmployeeDto>(candidates.Employee),
            CreatedAt = candidates.CreatedAt,
            CreatedBy = candidates.CreatedBy,
            UpdatedAt = candidates.UpdatedAt,
            UpdatedBy = candidates.UpdatedBy,    
        });
        return Task.FromResult(candidatess.ToList());
    }

}
