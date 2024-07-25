
using System.Collections;
using System.Net;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class TerminationManager:ITerminationManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TerminationManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(TerminationAddDto terminationAddDto)
    {
        var termination = new Termination()
        {
            EmployeeId = terminationAddDto.EmployeeId,
            Type = terminationAddDto.Type,
            Reason = terminationAddDto.Reason,
            Date = terminationAddDto.Date,
            NoticeDate = terminationAddDto.NoticeDate,
            CreatedAt = DateTime.Now,
        };
        

        _unitOfWork.Termination.Add(termination);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TerminationUpdateDto terminationUpdateDto, int id)
    {
        var termination = _unitOfWork.Termination.GetById(id);

        if (terminationUpdateDto.EmployeeId!=null) termination.EmployeeId = terminationUpdateDto.EmployeeId;
        if (terminationUpdateDto.Type!=null) termination.Type = terminationUpdateDto.Type;
        if (terminationUpdateDto.Reason != null) termination.Reason = terminationUpdateDto.Reason;
        if (terminationUpdateDto.Date != null) termination.Date = terminationUpdateDto.Date;
        if (terminationUpdateDto.NoticeDate != null) termination.NoticeDate = terminationUpdateDto.NoticeDate;
        if (termination == null) return Task.FromResult(0);

        termination.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Termination.Update(termination);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var termination = _unitOfWork.Termination.GetById(id);
        if (termination==null) return Task.FromResult(0);
        termination.IsDeleted = true;
        termination.DeletedAt = DateTime.Now;
        _unitOfWork.Termination.Update(termination);
        return _unitOfWork.SaveChangesAsync();
    }

    public TerminationReadDto? Get(int id)
    {
        var termination = _unitOfWork.Termination.GetWithEmployees(id).FirstOrDefault();
        if (termination == null) return null;
        
        return new TerminationReadDto()
        {
            Id = termination.Id,
           EmployeeId = termination.EmployeeId,
           Type = termination.Type,
           Date = termination.Date,
           Reason = termination.Reason,
           Employee = _mapper.Map<Employee,EmployeeDto>(termination.Employee),
           NoticeDate = termination.NoticeDate,
        };
    }

    public Task<List<TerminationReadDto>> GetAll()
    {
        var termination = _unitOfWork.Termination.GetAllWithEmployees();
        return Task.FromResult(termination.Select(p => new TerminationReadDto()
        {
            Id = p.Id,
            Type = p.Type,
            Reason = p.Reason,
            Date = p.Date,
            EmployeeId = p.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(p.Employee),
        }).ToList());
    }

   

    public async Task<FilteredTerminationsDto> GetFilteredTerminationAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var terminations =  _unitOfWork.Termination.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = terminations.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var terminationList = terminations.ToList();

            var paginatedResults = terminationList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<TerminationDto>();
            foreach (var termination in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new TerminationDto()
                {
                    Id = termination.Id,
                   Type = termination.Type,
                   Reason = termination.Reason,
                   NoticeDate = termination.NoticeDate,
                   Date = termination.Date,
                   EmployeeId = _mapper.Map<Employee,EmployeeDto>(termination.Employee),
                });
            }
            FilteredTerminationsDto filteredBudgetsExpensesDto = new()
            {
                TerminationDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (terminations != null)
        {
            IEnumerable<Termination> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(terminations, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(terminations, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var terminations = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(terminations);
            
            var mappedTermination = new List<TerminationDto>();

            foreach (var termination in paginatedResults)
            {
                
                mappedTermination.Add(new TerminationDto()
                {
                    Id = termination.Id,
                    Type = termination.Type,
                    Reason = termination.Reason,
                    NoticeDate = termination.NoticeDate,
                    Date = termination.Date,
                    EmployeeId = _mapper.Map<Employee,EmployeeDto>(termination.Employee),
                });
            }
            FilteredTerminationsDto filteredTerminationDto = new()
            {
                TerminationDto = mappedTermination,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTerminationDto;
        }

        return new FilteredTerminationsDto();
    }
    private IEnumerable<Termination> ApplyFilter(IEnumerable<Termination> terminations, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => terminations.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => terminations.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => terminations.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => terminations.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var terminationValue) => ApplyNumericFilter(terminations, column, terminationValue, operatorType),
            _ => terminations
        };
    }

    private IEnumerable<Termination> ApplyNumericFilter(IEnumerable<Termination> terminations, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => terminations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var terminationValue) && terminationValue == value),
        "neq" => terminations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var terminationValue) && terminationValue != value),
        "gte" => terminations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var terminationValue) && terminationValue >= value),
        "gt" => terminations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var terminationValue) && terminationValue > value),
        "lte" => terminations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var terminationValue) && terminationValue <= value),
        "lt" => terminations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var terminationValue) && terminationValue < value),
        _ => terminations
    };
}


    public Task<List<TerminationDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Termination> terminationDto;
            terminationDto = _unitOfWork.Termination.GetAllWithEmployees()
                .AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            // map from termination to terminationDto manually 
            
            var termination = terminationDto.Select(p => new TerminationDto()
            {
                Id = p.Id,
                Type = p.Type,
                Reason = p.Reason,
                NoticeDate = p.NoticeDate,
                Date = p.Date,
                EmployeeId = _mapper.Map<Employee,EmployeeDto>(p.Employee),
            });    
            return Task.FromResult(termination.ToList());
        }

        var  terminationDtos = _unitOfWork.Termination.GlobalSearch(searchKey);
        var terminations = terminationDtos.Select(p => new TerminationDto()
        {
            Id = p.Id,
            Type = p.Type,
            Reason = p.Reason,
            NoticeDate = p.NoticeDate,
            Date = p.Date,
            EmployeeId = _mapper.Map<Employee,EmployeeDto>(p.Employee),
        });    
        return Task.FromResult(terminations.ToList());
    }

}
