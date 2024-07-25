
using System.Collections;
using System.Net;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PoliciesManager:IPoliciesManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PoliciesManager(
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(PoliciesAddDto policiesAddDto)
    {
        var policies = new Policies()
        {
           Name = policiesAddDto.Name,
           Description = policiesAddDto.Description,
           DepartmentId = policiesAddDto.DepartmentId,
           Date = policiesAddDto.Date,
           FileName = policiesAddDto.File?.FileName,
           CreatedAt = DateTime.Now,
           // File = _mapper.Map<IFormFile,File>(policiesAddDto.File),
        };
        
        if (policiesAddDto.File != null)
        {
            // policies.FileContent = new byte[policiesAddDto.File.Length];
            using var ms = new MemoryStream();
            policiesAddDto.File.CopyTo(ms);
            policies.FileContent = ms.ToArray();
        }

        _unitOfWork.Policies.Add(policies);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PoliciesUpdateDto policiesUpdateDto, int id)
    {
        var policies = _unitOfWork.Policies.GetById(id);

        if (policiesUpdateDto.Name!=null) policies.Name = policiesUpdateDto.Name;
        if (policiesUpdateDto.Description!=null) policies.Description = policiesUpdateDto.Description;
        if (policiesUpdateDto.DepartmentId != null) policies.DepartmentId = policiesUpdateDto.DepartmentId;
        if (policiesUpdateDto.Date != null) policies.Date = policiesUpdateDto.Date;
        if (policiesUpdateDto.File != null) policies.FileName = policiesUpdateDto.File?.FileName;
        // if (policiesUpdateDto.File != null) policies.File = _mapper.Map<FileDto, File>(policiesUpdateDto.File);
        if (policies == null) return Task.FromResult(0);

        
        
        if (policiesUpdateDto.File != null)
        {
            // policies.FileContent = new byte[policiesAddDto.File.Length];
            using var ms = new MemoryStream();
            policiesUpdateDto.File.CopyTo(ms);
            policies.FileContent = ms.ToArray();
        }
        
        policies.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Policies.Update(policies);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var policies = _unitOfWork.Policies.GetById(id);
        if (policies==null) return Task.FromResult(0);
        policies.IsDeleted = true;
        policies.DeletedAt = DateTime.Now;
        _unitOfWork.Policies.Update(policies);
        return _unitOfWork.SaveChangesAsync();
    }

    public PoliciesReadDto? Get(int id)
    {
        var policies = _unitOfWork.Policies.GetWithDepartments(id).FirstOrDefault();
        if (policies == null) return null;
        
        return new PoliciesReadDto()
        {
            Id = policies.Id,
           Name = policies.Name,
           Description = policies.Description,
           Date = policies.Date,
           DepartmentId = policies.DepartmentId,
           Department = _mapper.Map<Department,DepartmentDto>(policies.Department),
           File = policies.FileName,
        };
    }

    public Task<List<PoliciesReadDto>> GetAll()
    {
        var policies = _unitOfWork.Policies.GetAllWithDepartments();
        return Task.FromResult(policies.Select(p => new PoliciesReadDto()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Date = p.Date,
            DepartmentId = p.DepartmentId,
            Department = _mapper.Map<Department,DepartmentDto>(p.Department),
            File = p.FileName,
        }).ToList());
    }

   

    public async Task<FilteredPoliciesDto> GetFilteredPoliciesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var policiesList =  _unitOfWork.Policies.GetAllWithDepartments();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = policiesList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var policyList = policiesList.ToList();

            var paginatedResults = policyList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<PolicyDto>();
            foreach (var policies in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new PolicyDto()
                {
                    Id = policies.Id,
                   Name = policies.Name,
                   DepartmentId = policies.DepartmentId,
                   Description = policies.Description,
                   Date = policies.Date,
                   Department = (policies.Department)?.Name,
                   File = policies.FileName
                });
            }
            FilteredPoliciesDto filteredBudgetsExpensesDto = new()
            {
                PoliciesDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (policiesList != null)
        {
            IEnumerable<Policies> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(policiesList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(policiesList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var policiess = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(policiess);
            
            var mappedPolicies = new List<PolicyDto>();

            foreach (var policies in paginatedResults)
            {
                
                mappedPolicies.Add(new PolicyDto()
                {
                    Id = policies.Id,
                    Name = policies.Name,
                    DepartmentId = policies.DepartmentId,
                    Description = policies.Description,
                    Date = policies.Date,
                    Department = (policies.Department)?.Name,
                    File = policies.FileName
                });
            }
            FilteredPoliciesDto filteredPoliciesDto = new()
            {
                PoliciesDto = mappedPolicies,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPoliciesDto;
        }

        return new FilteredPoliciesDto();
    }
    private IEnumerable<Policies> ApplyFilter(IEnumerable<Policies> policys, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => policys.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => policys.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => policys.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => policys.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var policiesValue) => ApplyNumericFilter(policys, column, policiesValue, operatorType),
            _ => policys
        };
    }

    private IEnumerable<Policies> ApplyNumericFilter(IEnumerable<Policies> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var policiesValue) && policiesValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var policiesValue) && policiesValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var policiesValue) && policiesValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var policiesValue) && policiesValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var policiesValue) && policiesValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var policiesValue) && policiesValue < value),
        _ => policys
    };
}


    public Task<List<PolicyDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Policies> policy;
            policy = _unitOfWork.Policies.GetAllWithDepartments().AsEnumerable().Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var policies = policy.Select(p => new PolicyDto()
            {
                Id = p.Id,
                Name = p.Name,
                DepartmentId = p.DepartmentId,
                Description = p.Description,
                Date = p.Date,
                Department = (p.Department)?.Name,
                File = p.FileName
            });
            return Task.FromResult(policies.ToList());
        }

        var  policys = _unitOfWork.Policies.GlobalSearch(searchKey);
        var policiess = policys.Select(p => new PolicyDto()
        {
            Id = p.Id,
            Name = p.Name,
            DepartmentId = p.DepartmentId,
            Description = p.Description,
            Date = p.Date,
            Department = p.Department != null ? p.Department.Name : null,
            File = p.FileName
        });
        return Task.FromResult(policiess.ToList());
    }

}
