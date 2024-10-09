using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class PayrollAdditionManager:IPayrollAdditionManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PayrollAdditionManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
       
    }
    
    public Task<int> Add(PayrollAdditionAddDto payrollAdditionAddDto)
    {
        var payrollAddition = new PayrollAddition()
        {
           Name = payrollAdditionAddDto.Name,
           UnitCalculation = payrollAdditionAddDto.UnitCalculation,
           UnitAmount = payrollAdditionAddDto.UnitAmount,
           Assignee = payrollAdditionAddDto.Assignee,
           EmployeeId = payrollAdditionAddDto.EmployeeId,
           CategoryId = payrollAdditionAddDto.CategoryId,
           CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.PayrollAddition.Add(payrollAddition);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(PayrollAdditionUpdateDto payrollAdditionUpdateDto, int id)
    {
        var payrollAddition = _unitOfWork.PayrollAddition.GetById(id);

        if (payrollAdditionUpdateDto.Name!=null) payrollAddition.Name = payrollAdditionUpdateDto.Name;
        if (payrollAdditionUpdateDto.UnitCalculation!=null) payrollAddition.UnitCalculation = payrollAdditionUpdateDto.UnitCalculation;
        if (payrollAdditionUpdateDto.UnitAmount != null) payrollAddition.UnitAmount = payrollAdditionUpdateDto.UnitAmount;
        if (payrollAdditionUpdateDto.Assignee != null) payrollAddition.Assignee = payrollAdditionUpdateDto.Assignee;
        if (payrollAdditionUpdateDto.EmployeeId != null) payrollAddition.EmployeeId = payrollAdditionUpdateDto.EmployeeId;
        if (payrollAdditionUpdateDto.CategoryId != null) payrollAddition.CategoryId = payrollAdditionUpdateDto.CategoryId;
        
        if (payrollAddition == null) return Task.FromResult(0);
        
        
        payrollAddition.UpdatedAt = DateTime.Now;
        _unitOfWork.PayrollAddition.Update(payrollAddition);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var payrollAddition = _unitOfWork.PayrollAddition.GetById(id);
        if (payrollAddition==null) return Task.FromResult(0);
        payrollAddition.IsDeleted = true;
        payrollAddition.DeletedAt = DateTime.Now;
        _unitOfWork.PayrollAddition.Update(payrollAddition);
        return _unitOfWork.SaveChangesAsync();
    }

    public PayrollAdditionReadDto? Get(int id)
    {
        var payrollAddition = _unitOfWork.PayrollAddition.GetWithEmployees(id).FirstOrDefault();
        if (payrollAddition == null) return null;
        
        return new PayrollAdditionReadDto()
        {
            Id = payrollAddition.Id,
           Name = payrollAddition.Name,
           UnitAmount = payrollAddition.UnitAmount,
           UnitCalculation = payrollAddition.UnitCalculation,
           Assignee = payrollAddition.Assignee,
           EmployeeId = payrollAddition.EmployeeId,
           Employee = _mapper.Map<Employee,EmployeeDto>(payrollAddition.Employee),
           CategoryId = payrollAddition.CategoryId,
           Category = _mapper.Map<Category,CategoryDto>(payrollAddition.Category),
        };
    }

    public Task<List<PayrollAdditionReadDto>> GetAll()
    {
        var payrollAddition = _unitOfWork.PayrollAddition.GetAllWithEmployees();
        return Task.FromResult(payrollAddition.Result.Select(p => new PayrollAdditionReadDto()
        {
            Id = p.Id,
            Name = p.Name,
            UnitAmount = p.UnitAmount,
            UnitCalculation = p.UnitCalculation,
            Assignee = p.Assignee,
            EmployeeId = p.EmployeeId,
            Employee = _mapper.Map<Employee,EmployeeDto>(p.Employee),
            CategoryId = p.CategoryId,
            Category = _mapper.Map<Category,CategoryDto>(p.Category),
        }).ToList());
    }


    public async Task<FilteredPayrollAdditionsDto> GetFilteredPayrollAdditionsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var payrollAdditions = await _unitOfWork.PayrollAddition.GetAllWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = payrollAdditions.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var payrollAdditionList = payrollAdditions.ToList();

            var paginatedResults = payrollAdditionList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsExpensess = new List<PayrollAdditionDto>();
            foreach (var payrollAddition in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new PayrollAdditionDto()
                {
                    Id = payrollAddition.Id,
                   Name = payrollAddition.Name,
                   UnitAmount = payrollAddition.UnitAmount,
                   UnitCalculation = payrollAddition.UnitCalculation,
                   Assignee = payrollAddition.Assignee,
                   EmployeeId = payrollAddition.EmployeeId,
                   Employee = _mapper.Map<Employee,EmployeeDto>(payrollAddition.Employee),
                   CategoryId = payrollAddition.CategoryId,
                   Category = _mapper.Map<Category,CategoryDto>(payrollAddition.Category)
                });
            }
            FilteredPayrollAdditionsDto filteredBudgetsExpensesDto = new()
            {
                PayrollAdditionDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (payrollAdditions != null)
        {
            IEnumerable<PayrollAddition> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(payrollAdditions, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(payrollAdditions, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var payrollAdditions = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(payrollAdditions);
            
            var mappedPayrollAddition = new List<PayrollAdditionDto>();

            foreach (var payrollAddition in paginatedResults)
            {
                
                mappedPayrollAddition.Add(new PayrollAdditionDto()
                {
                    Id = payrollAddition.Id,
                    Name = payrollAddition.Name,
                    UnitAmount = payrollAddition.UnitAmount,
                    UnitCalculation = payrollAddition.UnitCalculation,
                    Assignee = payrollAddition.Assignee,
                    EmployeeId = payrollAddition.EmployeeId,
                    Employee = _mapper.Map<Employee,EmployeeDto>(payrollAddition.Employee),
                    CategoryId = payrollAddition.CategoryId,
                    Category = _mapper.Map<Category,CategoryDto>(payrollAddition.Category)
                });
            }
            FilteredPayrollAdditionsDto filteredPayrollAdditionDto = new()
            {
                PayrollAdditionDto = mappedPayrollAddition,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredPayrollAdditionDto;
        }

        return new FilteredPayrollAdditionsDto();
    }
    private IEnumerable<PayrollAddition> ApplyFilter(IEnumerable<PayrollAddition> payrollAdditions, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => payrollAdditions.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => payrollAdditions.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => payrollAdditions.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => payrollAdditions.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var payrollAdditionValue) => ApplyNumericFilter(payrollAdditions, column, payrollAdditionValue, operatorType),
            _ => payrollAdditions
        };
    }

    private IEnumerable<PayrollAddition> ApplyNumericFilter(IEnumerable<PayrollAddition> payrollAdditions, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => payrollAdditions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollAdditionValue) && payrollAdditionValue == value),
        "neq" => payrollAdditions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollAdditionValue) && payrollAdditionValue != value),
        "gte" => payrollAdditions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollAdditionValue) && payrollAdditionValue >= value),
        "gt" => payrollAdditions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollAdditionValue) && payrollAdditionValue > value),
        "lte" => payrollAdditions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollAdditionValue) && payrollAdditionValue <= value),
        "lt" => payrollAdditions.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var payrollAdditionValue) && payrollAdditionValue < value),
        _ => payrollAdditions
    };
}


    public Task<List<PayrollAdditionDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<PayrollAddition> payrollAdditionDto;
            payrollAdditionDto = _unitOfWork.PayrollAddition.GetAllWithEmployees().Result
                .AsEnumerable()
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var payrollAddition = _mapper.Map<IEnumerable<PayrollAddition>, IEnumerable<PayrollAdditionDto>>(payrollAdditionDto);
            return Task.FromResult(payrollAddition.ToList());
        }

        var  payrollAdditionDtos = _unitOfWork.PayrollAddition.GlobalSearch(searchKey);
        var payrollAdditions = _mapper.Map<IEnumerable<PayrollAddition>, IEnumerable<PayrollAdditionDto>>(payrollAdditionDtos);
        return Task.FromResult(payrollAdditions.ToList());
    }

}
