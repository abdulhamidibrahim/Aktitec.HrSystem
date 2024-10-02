
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
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PayrollOvertimeManager:IPayrollOvertimeManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PayrollOvertimeManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
       
    }
    
    public Task<int> Add(PayrollOvertimeAddDto payrollOvertimeAddDto)
    {
        var payrollOvertime = new PayrollOvertime()
        {
           Name = payrollOvertimeAddDto.Name,
           RateType = payrollOvertimeAddDto.RateType,
           Rate = payrollOvertimeAddDto.Rate,
           CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.PayrollOvertime.Add(payrollOvertime);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PayrollOvertimeUpdateDto payrollOvertimeUpdateDto, int id)
    {
        var payrollOvertime = _unitOfWork.PayrollOvertime.GetById(id);

        if (payrollOvertime == null) return Task.FromResult(0);

        if (payrollOvertimeUpdateDto.Name!=null) payrollOvertime.Name = payrollOvertimeUpdateDto.Name;
        if (payrollOvertimeUpdateDto.RateType!=null) payrollOvertime.RateType = payrollOvertimeUpdateDto.RateType;
        if (payrollOvertimeUpdateDto.Rate != null) payrollOvertime.Rate = payrollOvertimeUpdateDto.Rate;
        
        
        payrollOvertime.UpdatedAt = DateTime.Now;
        _unitOfWork.PayrollOvertime.Update(payrollOvertime);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var payroll = _unitOfWork.PayrollOvertime.GetById(id);
        if (payroll==null) return Task.FromResult(0);
        payroll.IsDeleted = true;
        payroll.DeletedAt = DateTime.Now;
        _unitOfWork.PayrollOvertime.Update(payroll);
        return _unitOfWork.SaveChangesAsync();
    }

    public PayrollOvertimeReadDto? Get(int id)
    {
        var payrollOvertime = _unitOfWork.PayrollOvertime.GetById(id);
        if (payrollOvertime == null) return null;
        return new PayrollOvertimeReadDto()
        {
            Id = payrollOvertime.Id,
           Name = payrollOvertime.Name,
           RateType = payrollOvertime.RateType,
           Rate = payrollOvertime.Rate,
        };
    }

    public Task<List<PayrollOvertimeReadDto>> GetAll()
    {
        var payrollOvertime = _unitOfWork.PayrollOvertime.GetAll();
        return Task.FromResult(payrollOvertime.Result.Select(p => new PayrollOvertimeReadDto()
        {
            Id = p.Id,
            Name = p.Name,
            RateType = p.RateType,
            Rate = p.Rate,
        }).ToList());
    }


    public async Task<FilteredPayrollOvertimesDto> GetFilteredPayrollOvertimesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var payrollOvertimes = await _unitOfWork.PayrollOvertime.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = payrollOvertimes.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var payrollOvertimeList = payrollOvertimes.ToList();

            var paginatedResults = payrollOvertimeList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<PayrollOvertimeDto>();
            foreach (var payrollOvertime in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new PayrollOvertimeDto()
                {
                    Id = payrollOvertime.Id,
                   Name = payrollOvertime.Name,
                   RateType = payrollOvertime.RateType,
                   Rate = payrollOvertime.Rate,
                });
            }
            FilteredPayrollOvertimesDto filteredBudgetsExpensesDto = new()
            {
                PayrollOvertimeDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (payrollOvertimes != null)
        {
            IEnumerable<PayrollOvertime> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(payrollOvertimes, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(payrollOvertimes, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var payrollOvertimes = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(payrollOvertimes);
            
            var mappedPayrollOvertime = new List<PayrollOvertimeDto>();

            foreach (var payrollOvertime in paginatedResults)
            {
                
                mappedPayrollOvertime.Add(new PayrollOvertimeDto()
                {
                    Id = payrollOvertime.Id,
                    Name = payrollOvertime.Name,
                    RateType = payrollOvertime.RateType,
                    Rate = payrollOvertime.Rate,
                });
            }
            FilteredPayrollOvertimesDto filteredPayrollOvertimeDto = new()
            {
                PayrollOvertimeDto = mappedPayrollOvertime,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPayrollOvertimeDto;
        }

        return new FilteredPayrollOvertimesDto();
    }
    private IEnumerable<PayrollOvertime> ApplyFilter(IEnumerable<PayrollOvertime> payrollOvertimes, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => payrollOvertimes.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => payrollOvertimes.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => payrollOvertimes.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => payrollOvertimes.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var payrollOvertimeValue) => ApplyNumericFilter(payrollOvertimes, column, payrollOvertimeValue, operatorType),
            _ => payrollOvertimes
        };
    }

    private IEnumerable<PayrollOvertime> ApplyNumericFilter(IEnumerable<PayrollOvertime> payrollOvertimes, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => payrollOvertimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollOvertimeValue) && payrollOvertimeValue == value),
        "neq" => payrollOvertimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollOvertimeValue) && payrollOvertimeValue != value),
        "gte" => payrollOvertimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollOvertimeValue) && payrollOvertimeValue >= value),
        "gt" => payrollOvertimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollOvertimeValue) && payrollOvertimeValue > value),
        "lte" => payrollOvertimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollOvertimeValue) && payrollOvertimeValue <= value),
        "lt" => payrollOvertimes.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollOvertimeValue) && payrollOvertimeValue < value),
        _ => payrollOvertimes
    };
}


    public Task<List<PayrollOvertimeDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<PayrollOvertime> payrollOvertimeDto;
            payrollOvertimeDto = _unitOfWork.PayrollOvertime.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var payrollOvertime = _mapper.Map<IEnumerable<PayrollOvertime>, IEnumerable<PayrollOvertimeDto>>(payrollOvertimeDto);
            return Task.FromResult(payrollOvertime.ToList());
        }

        var  payrollOvertimeDtos = _unitOfWork.PayrollOvertime.GlobalSearch(searchKey);
        var payrollOvertimes = _mapper.Map<IEnumerable<PayrollOvertime>, IEnumerable<PayrollOvertimeDto>>(payrollOvertimeDtos);
        return Task.FromResult(payrollOvertimes.ToList());
    }

}
