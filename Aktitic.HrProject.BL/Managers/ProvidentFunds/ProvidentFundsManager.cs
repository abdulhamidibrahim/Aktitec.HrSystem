
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ProvidentFundsManager:IProvidentFundsManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProvidentFundsManager(
        IUnitOfWork unitOfWork,
        IMapper mapper) 
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(ProvidentFundsAddDto providentFundsAddDto)
    {
        var providentFunds = new ProvidentFunds()
        {
            ProvidentType = providentFundsAddDto.ProvidentType,
            EmployeeShareAmount = providentFundsAddDto.EmployeeShareAmount,
            OrganizationShareAmount = providentFundsAddDto.OrganizationShareAmount,
            EmployeeSharePercentage = providentFundsAddDto.EmployeeSharePercentage,
            OrganizationSharePercentage = providentFundsAddDto.OrganizationSharePercentage,
            Description = providentFundsAddDto.Description,
            Status = providentFundsAddDto.Status,
            EmployeeId = providentFundsAddDto.EmployeeId,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.ProvidentFunds.Add(providentFunds);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ProvidentFundsUpdateDto providentFundsUpdateDto, int id)
    {
        var providentFunds = _unitOfWork.ProvidentFunds.GetById(id);
        
        
        if (providentFunds == null) return Task.FromResult(0);
        
        if(providentFundsUpdateDto.ProvidentType != null) providentFunds.ProvidentType = providentFundsUpdateDto.ProvidentType;
        if(providentFundsUpdateDto.EmployeeShareAmount != null) providentFunds.EmployeeShareAmount = providentFundsUpdateDto.EmployeeShareAmount;
        if(providentFundsUpdateDto.EmployeeSharePercentage != null) providentFunds.EmployeeSharePercentage = providentFundsUpdateDto.EmployeeSharePercentage;
        if(providentFundsUpdateDto.OrganizationShareAmount != null) providentFunds.OrganizationShareAmount = providentFundsUpdateDto.OrganizationShareAmount;
        if(providentFundsUpdateDto.OrganizationSharePercentage != null) providentFunds.OrganizationSharePercentage = providentFundsUpdateDto.OrganizationSharePercentage;
        if(providentFundsUpdateDto.Description != null) providentFunds.Description = providentFundsUpdateDto.Description;
        if(providentFundsUpdateDto.EmployeeId != null) providentFunds.EmployeeId = providentFundsUpdateDto.EmployeeId;
        if(providentFundsUpdateDto.Status != null) providentFunds.Status = providentFundsUpdateDto.Status;
       
        
        _unitOfWork.ProvidentFunds.Update(providentFunds);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var provident = _unitOfWork.ProvidentFunds.GetById(id);
        if (provident==null) return Task.FromResult(0);
        provident.IsDeleted = true;
        provident.DeletedAt = DateTime.Now;
        _unitOfWork.ProvidentFunds.Update(provident);
        return _unitOfWork.SaveChangesAsync();
    }

    public ProvidentFundsReadDto? Get(int id)
    {
        var providentFunds = _unitOfWork.ProvidentFunds.GetWithEmployees(id);
        if (providentFunds == null) return null;
        return new ProvidentFundsReadDto()
        {
            Id = providentFunds.Id,
            ProvidentType = providentFunds.ProvidentType,
            EmployeeShareAmount = providentFunds.EmployeeShareAmount,
            EmployeeSharePercentage = providentFunds.EmployeeSharePercentage,
            OrganizationShareAmount = providentFunds.OrganizationShareAmount,
            OrganizationSharePercentage = providentFunds.OrganizationSharePercentage,
            Description = providentFunds.Description,
            EmployeeId = providentFunds.EmployeeId,
            Employee = _mapper.Map<Employee, EmployeeDto>(providentFunds.Employee),
            Status = providentFunds.Status,
        };
    }

    public Task<List<ProvidentFundsReadDto>> GetAll()
    {
        var providentFunds = _unitOfWork.ProvidentFunds.GetAllWithEmployeesAsync();
        return Task.FromResult(providentFunds.Result.Select(p => new ProvidentFundsReadDto()
        {
            Id = p.Id,
            ProvidentType = p.ProvidentType,
            EmployeeShareAmount = p.EmployeeShareAmount,
            EmployeeSharePercentage = p.EmployeeSharePercentage,
            OrganizationShareAmount = p.OrganizationShareAmount,
            OrganizationSharePercentage = p.OrganizationSharePercentage,
            Description = p.Description,
            Employee = _mapper.Map<Employee, EmployeeDto>(p.Employee),
            Status = p.Status,

        }).ToList());
    }
    
     public async Task<FilteredProvidentFundsDto> GetFilteredProvidentFundsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var providentFundss = await _unitOfWork.ProvidentFunds.GetAllWithEmployeesAsync();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = providentFundss.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var providentFundsList = providentFundss.ToList();

            var paginatedResults = providentFundsList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedProvidentFundss = new List<ProvidentFundsDto>();
            foreach (var providentFunds in paginatedResults)
            {
                mappedProvidentFundss.Add(new ProvidentFundsDto()
                {
                    Id = providentFunds.Id,
                    ProvidentType = providentFunds.ProvidentType,
                    EmployeeShareAmount = providentFunds.EmployeeShareAmount,
                    EmployeeSharePercentage = providentFunds.EmployeeSharePercentage,
                    OrganizationShareAmount = providentFunds.OrganizationShareAmount,
                    OrganizationSharePercentage = providentFunds.OrganizationSharePercentage,
                    Description = providentFunds.Description,
                    Employee = _mapper.Map<Employee, EmployeeDto>(providentFunds.Employee),
                    Status = providentFunds.Status,
                });
            }
            FilteredProvidentFundsDto filteredProvidentFundsDto = new()
            {
                ProvidentFundsDto = mappedProvidentFundss,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredProvidentFundsDto;
        }

        if (providentFundss != null)
        {
            IEnumerable<ProvidentFunds> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(providentFundss, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(providentFundss, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var providentFundss = paginatedResults.ToList();
            // var mappedProvidentFunds = _mapper.Map<IEnumerable<ProvidentFunds>, IEnumerable<ProvidentFundsDto>>(providentFundss);
            
            var mappedProvidentFundss = new List<ProvidentFundsDto>();

            foreach (var providentFunds in paginatedResults)
            {
                
                mappedProvidentFundss.Add(new ProvidentFundsDto()
                {
                    Id = providentFunds.Id,
                    ProvidentType = providentFunds.ProvidentType,
                    EmployeeShareAmount = providentFunds.EmployeeShareAmount,
                    EmployeeSharePercentage = providentFunds.EmployeeSharePercentage,
                    OrganizationShareAmount = providentFunds.OrganizationShareAmount,
                    OrganizationSharePercentage = providentFunds.OrganizationSharePercentage,
                    Description = providentFunds.Description,
                    Employee = _mapper.Map<Employee, EmployeeDto>(providentFunds.Employee),
                    Status = providentFunds.Status,
                    
                });
            }
            FilteredProvidentFundsDto filteredProvidentFundsDto = new()
            {
                ProvidentFundsDto = mappedProvidentFundss,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredProvidentFundsDto;
        }

        return new FilteredProvidentFundsDto();
    }
    private IEnumerable<ProvidentFunds> ApplyFilter(IEnumerable<ProvidentFunds> providentFundss, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => providentFundss.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => providentFundss.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => providentFundss.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => providentFundss.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var providentFundsValue) => ApplyNumericFilter(providentFundss, column, providentFundsValue, operatorType),
            _ => providentFundss
        };
    }

    private IEnumerable<ProvidentFunds> ApplyNumericFilter(IEnumerable<ProvidentFunds> providentFundss, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => providentFundss.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var providentFundsValue) && providentFundsValue == value),
        "neq" => providentFundss.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var providentFundsValue) && providentFundsValue != value),
        "gte" => providentFundss.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var providentFundsValue) && providentFundsValue >= value),
        "gt" => providentFundss.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var providentFundsValue) && providentFundsValue > value),
        "lte" => providentFundss.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var providentFundsValue) && providentFundsValue <= value),
        "lt" => providentFundss.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var providentFundsValue) && providentFundsValue < value),
        _ => providentFundss
    };
}


    public Task<List<ProvidentFundsDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<ProvidentFunds> providentFundsDto;
            providentFundsDto = _unitOfWork.ProvidentFunds.GetAllWithEmployeesAsync().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var providentFunds = _mapper.Map<IEnumerable<ProvidentFunds>, IEnumerable<ProvidentFundsDto>>(providentFundsDto);
            return Task.FromResult(providentFunds.ToList());
        }

        var  providentFundsDtos = _unitOfWork.ProvidentFunds.GlobalSearch(searchKey);
        var providentFundss = _mapper.Map<IEnumerable<ProvidentFunds>, IEnumerable<ProvidentFundsDto>>(providentFundsDtos);
        return Task.FromResult(providentFundss.ToList());
    }

}
