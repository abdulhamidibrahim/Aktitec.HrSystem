
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

public class ResignationManager:IResignationManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ResignationManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(ResignationAddDto resignationAddDto)
    {
        var resignation = new Resignation()
        {
            EmployeeId = resignationAddDto.EmployeeId,
            ResignationDate = resignationAddDto.ResignationDate,
            Reason = resignationAddDto.Reason,
            NoticeDate = resignationAddDto.NoticeDate,
            CreatedAt = DateTime.Now,
        };
        

        _unitOfWork.Resignation.Add(resignation);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ResignationUpdateDto resignationUpdateDto, int id)
    {
        var resignation = _unitOfWork.Resignation.GetById(id);

        if (resignationUpdateDto.EmployeeId!=null) resignation.EmployeeId = resignationUpdateDto.EmployeeId;
        if (resignationUpdateDto.ResignationDate!=null) resignation.ResignationDate = resignationUpdateDto.ResignationDate;
        if (resignationUpdateDto.Reason != null) resignation.Reason = resignationUpdateDto.Reason;
        if (resignationUpdateDto.NoticeDate != null) resignation.NoticeDate = resignationUpdateDto.NoticeDate;
        if (resignation == null) return Task.FromResult(0);

        resignation.UpdatedAt = DateTime.Now;
        
        _unitOfWork.Resignation.Update(resignation);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var resignation = _unitOfWork.Resignation.GetById(id);
        if (resignation==null) return Task.FromResult(0);
        resignation.IsDeleted = true;
        resignation.DeletedAt = DateTime.Now;
        _unitOfWork.Resignation.Update(resignation);
        return _unitOfWork.SaveChangesAsync();
    }

    public ResignationReadDto? Get(int id)
    {
        var resignation = _unitOfWork.Resignation.GetWithEmployees(id).FirstOrDefault();
        if (resignation == null) return null;
        
        return new ResignationReadDto()
        {
            Id = resignation.Id,
           EmployeeId = resignation.EmployeeId,
           ResignationDate = resignation.ResignationDate,
           Reason = resignation.Reason,
           Employee = _mapper.Map<Employee,EmployeeDto>(resignation.Employee),
           NoticeDate = resignation.NoticeDate,
        };
    }

    public Task<List<ResignationReadDto>> GetAll()
    {
        var resignation = _unitOfWork.Resignation.GetAllWithEmployees();
        return Task.FromResult(resignation.Select(p => new ResignationReadDto()
        {
            Id = p.Id,
            ResignationDate = p.ResignationDate,
            Reason = p.Reason,
            EmployeeId = p.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(p.Employee),
        }).ToList());
    }

   

    public async Task<FilteredResignationsDto> GetFilteredResignationAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var resignations =  _unitOfWork.Resignation.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = resignations.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var resignationList = resignations.ToList();

            var paginatedResults = resignationList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<ResignationDto>();
            foreach (var resignation in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new ResignationDto()
                {
                    Id = resignation.Id,
                   ResignationDate = resignation.ResignationDate,
                   Reason = resignation.Reason,
                   NoticeDate = resignation.NoticeDate,
                   EmployeeId = _mapper.Map<Employee,EmployeeDto>(resignation.Employee),
                });
            }
            FilteredResignationsDto filteredBudgetsExpensesDto = new()
            {
                ResignationDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (resignations != null)
        {
            IEnumerable<Resignation> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(resignations, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(resignations, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var resignations = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(resignations);
            
            var mappedResignation = new List<ResignationDto>();

            foreach (var resignation in paginatedResults)
            {
                
                mappedResignation.Add(new ResignationDto()
                {
                    Id = resignation.Id,
                    ResignationDate = resignation.ResignationDate,
                    Reason = resignation.Reason,
                    NoticeDate = resignation.NoticeDate,
                    EmployeeId = _mapper.Map<Employee,EmployeeDto>(resignation.Employee),
                });
            }
            FilteredResignationsDto filteredResignationDto = new()
            {
                ResignationDto = mappedResignation,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredResignationDto;
        }

        return new FilteredResignationsDto();
    }
    private IEnumerable<Resignation> ApplyFilter(IEnumerable<Resignation> resignations, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => resignations.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => resignations.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => resignations.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => resignations.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var resignationValue) => ApplyNumericFilter(resignations, column, resignationValue, operatorType),
            _ => resignations
        };
    }

    private IEnumerable<Resignation> ApplyNumericFilter(IEnumerable<Resignation> resignations, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => resignations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var resignationValue) && resignationValue == value),
        "neq" => resignations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var resignationValue) && resignationValue != value),
        "gte" => resignations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var resignationValue) && resignationValue >= value),
        "gt" => resignations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var resignationValue) && resignationValue > value),
        "lte" => resignations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var resignationValue) && resignationValue <= value),
        "lt" => resignations.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var resignationValue) && resignationValue < value),
        _ => resignations
    };
}


    public Task<List<ResignationDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Resignation> resignationDto;
            resignationDto = _unitOfWork.Resignation.GetAllWithEmployees()
                .AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            // map from resignation to resignationDto manually 
            
            var resignation = resignationDto.Select(p => new ResignationDto()
            {
                Id = p.Id,
                ResignationDate = p.ResignationDate,
                Reason = p.Reason,
                NoticeDate = p.NoticeDate,
                EmployeeId = _mapper.Map<Employee,EmployeeDto>(p.Employee),
            });    
            return Task.FromResult(resignation.ToList());
        }

        var  resignationDtos = _unitOfWork.Resignation.GlobalSearch(searchKey);
        var resignations = resignationDtos.Select(p => new ResignationDto()
        {
            Id = p.Id,
            ResignationDate = p.ResignationDate,
            Reason = p.Reason,
            NoticeDate = p.NoticeDate,
            EmployeeId = _mapper.Map<Employee,EmployeeDto>(p.Employee),
        });    
        return Task.FromResult(resignations.ToList());
    }

}
