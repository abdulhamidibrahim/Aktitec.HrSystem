using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PayrollDeductionManager:IPayrollDeductionManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PayrollDeductionManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
       
    }
    
    public Task<int> Add(PayrollDeductionAddDto payrollDeductionAddDto)
    {
        var payrollDeduction = new PayrollDeduction()
        {
           Name = payrollDeductionAddDto.Name,
           UnitCalculation = payrollDeductionAddDto.UnitCalculation,
           UnitAmount = payrollDeductionAddDto.UnitAmount,
           Assignee = payrollDeductionAddDto.Assignee,
           EmployeeId = payrollDeductionAddDto.EmployeeId,
           CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.PayrollDeduction.Add(payrollDeduction);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PayrollDeductionUpdateDto payrollDeductionUpdateDto, int id)
    {
        var payrollDeduction = _unitOfWork.PayrollDeduction.GetById(id);

        if (payrollDeductionUpdateDto.Name!=null) payrollDeduction.Name = payrollDeductionUpdateDto.Name;
        if (payrollDeductionUpdateDto.UnitCalculation!=null) payrollDeduction.UnitCalculation = payrollDeductionUpdateDto.UnitCalculation;
        if (payrollDeductionUpdateDto.UnitAmount != null) payrollDeduction.UnitAmount = payrollDeductionUpdateDto.UnitAmount;
        if (payrollDeductionUpdateDto.Assignee != null) payrollDeduction.Assignee = payrollDeductionUpdateDto.Assignee;
        if (payrollDeductionUpdateDto.EmployeeId != null) payrollDeduction.EmployeeId = payrollDeductionUpdateDto.EmployeeId;
        
        if (payrollDeduction == null) return Task.FromResult(0);

        payrollDeduction.UpdatedAt = DateTime.Now;
        
        _unitOfWork.PayrollDeduction.Update(payrollDeduction);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var payrollDeduction = _unitOfWork.PayrollDeduction.GetById(id);
        if (payrollDeduction==null) return Task.FromResult(0);
        payrollDeduction.IsDeleted = true;
        payrollDeduction.DeletedAt = DateTime.Now;
        _unitOfWork.PayrollDeduction.Update(payrollDeduction);
        return _unitOfWork.SaveChangesAsync();
    }

    public PayrollDeductionReadDto? Get(int id)
    {
        var payrollDeduction = _unitOfWork.PayrollDeduction.GetWithEmployees(id).FirstOrDefault();
        if (payrollDeduction == null) return null;
        
        return new PayrollDeductionReadDto()
        {
            Id = payrollDeduction.Id,
           Name = payrollDeduction.Name,
           UnitAmount = payrollDeduction.UnitAmount,
           UnitCalculation = payrollDeduction.UnitCalculation,
           Assignee = payrollDeduction.Assignee,
           EmployeeId = payrollDeduction.EmployeeId,
           Employee = _mapper.Map<Employee,EmployeeDto>(payrollDeduction.Employee)
        };
    }

    public Task<List<PayrollDeductionReadDto>> GetAll()
    {
        var payrollDeduction = _unitOfWork.PayrollDeduction.GetAllWithEmployees();
        return Task.FromResult(payrollDeduction.Result.Select(p => new PayrollDeductionReadDto()
        {
            Id = p.Id,
            Name = p.Name,
            UnitAmount = p.UnitAmount,
            UnitCalculation = p.UnitCalculation,
            Assignee = p.Assignee,
            EmployeeId = p.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(p.Employee)
        }).ToList());
    }


    public async Task<FilteredPayrollDeductionsDto> GetFilteredPayrollDeductionsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var payrollDeductions = await _unitOfWork.PayrollDeduction.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = payrollDeductions.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var payrollDeductionList = payrollDeductions.ToList();

            var paginatedResults = payrollDeductionList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<PayrollDeductionDto>();
            foreach (var payrollDeduction in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new PayrollDeductionDto()
                {
                    Id = payrollDeduction.Id,
                   Name = payrollDeduction.Name,
                   UnitAmount = payrollDeduction.UnitAmount,
                   UnitCalculation = payrollDeduction.UnitCalculation,
                   Assignee = payrollDeduction.Assignee,
                   EmployeeId = payrollDeduction.EmployeeId,
                   Employee = _mapper.Map<Employee,EmployeeDto>(payrollDeduction.Employee)
                });
            }
            FilteredPayrollDeductionsDto filteredBudgetsExpensesDto = new()
            {
                PayrollDeductionDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (payrollDeductions != null)
        {
            IEnumerable<PayrollDeduction> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(payrollDeductions, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(payrollDeductions, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var payrollDeductions = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(payrollDeductions);
            
            var mappedPayrollDeduction = new List<PayrollDeductionDto>();

            foreach (var payrollDeduction in paginatedResults)
            {
                
                mappedPayrollDeduction.Add(new PayrollDeductionDto()
                {
                    Id = payrollDeduction.Id,
                    Name = payrollDeduction.Name,
                    UnitAmount = payrollDeduction.UnitAmount,
                    UnitCalculation = payrollDeduction.UnitCalculation,
                    Assignee = payrollDeduction.Assignee,
                    EmployeeId = payrollDeduction.EmployeeId,
                    Employee = _mapper.Map<Employee,EmployeeDto>(payrollDeduction.Employee)
                });
            }
            FilteredPayrollDeductionsDto filteredPayrollDeductionDto = new()
            {
                PayrollDeductionDto = mappedPayrollDeduction,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPayrollDeductionDto;
        }

        return new FilteredPayrollDeductionsDto();
    }
    private IEnumerable<PayrollDeduction> ApplyFilter(IEnumerable<PayrollDeduction> payrollDeductions, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => payrollDeductions.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => payrollDeductions.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => payrollDeductions.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => payrollDeductions.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var payrollDeductionValue) => ApplyNumericFilter(payrollDeductions, column, payrollDeductionValue, operatorType),
            _ => payrollDeductions
        };
    }

    private IEnumerable<PayrollDeduction> ApplyNumericFilter(IEnumerable<PayrollDeduction> payrollDeductions, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => payrollDeductions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollDeductionValue) && payrollDeductionValue == value),
        "neq" => payrollDeductions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollDeductionValue) && payrollDeductionValue != value),
        "gte" => payrollDeductions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollDeductionValue) && payrollDeductionValue >= value),
        "gt" => payrollDeductions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollDeductionValue) && payrollDeductionValue > value),
        "lte" => payrollDeductions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollDeductionValue) && payrollDeductionValue <= value),
        "lt" => payrollDeductions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollDeductionValue) && payrollDeductionValue < value),
        _ => payrollDeductions
    };
}


    public Task<List<PayrollDeductionDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<PayrollDeduction> payrollDeductionDto;
            payrollDeductionDto = _unitOfWork.PayrollDeduction.GetAllWithEmployees().Result
                .AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var payrollDeduction = _mapper.Map<IEnumerable<PayrollDeduction>, IEnumerable<PayrollDeductionDto>>(payrollDeductionDto);
            return Task.FromResult(payrollDeduction.ToList());
        }

        var  payrollDeductionDtos = _unitOfWork.PayrollDeduction.GlobalSearch(searchKey);
        var payrollDeductions = _mapper.Map<IEnumerable<PayrollDeduction>, IEnumerable<PayrollDeductionDto>>(payrollDeductionDtos);
        return Task.FromResult(payrollDeductions.ToList());
    }

}
