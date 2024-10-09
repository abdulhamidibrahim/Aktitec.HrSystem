using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class BudgetManager:IBudgetManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BudgetManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(BudgetAddDto budgetAddDto)
    {
        var budget = new Budget()
        {
            Title = budgetAddDto.Title,
            Type = budgetAddDto.Type,
            BudgetAmount = budgetAddDto.BudgetAmount,
            StartDate = budgetAddDto.StartDate,
            EndDate = budgetAddDto.EndDate,
            ExpectedProfit = budgetAddDto.ExpectedProfit,
            Tax = budgetAddDto.Tax,
            
            OverallExpense = budgetAddDto.OverallExpense,
            OverallRevenue = budgetAddDto.OverallRevenue,
            Expenses = _mapper.Map<List<ExpensesCreateDto>,List<ExpensesOfBudget>>(budgetAddDto.Expenses),
            Revenues = _mapper.Map<List<RevenuesCreateDto>,List<Revenue>>(budgetAddDto.Revenue),
        }; 
        _unitOfWork.Budget.Add(budget);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(BudgetUpdateDto budgetUpdateDto, int id)
    {
        var budget = _unitOfWork.Budget.GetById(id);
        
        
        if (budget == null) return Task.FromResult(0);
        
        if(budgetUpdateDto.Title != null) budget.Title = budgetUpdateDto.Title;
        if(budgetUpdateDto.Type != null) budget.Type = budgetUpdateDto.Type;
        if(budgetUpdateDto.BudgetAmount != null) budget.BudgetAmount = budgetUpdateDto.BudgetAmount;
        if(budgetUpdateDto.StartDate != null) budget.StartDate = budgetUpdateDto.StartDate;
        if(budgetUpdateDto.EndDate != null) budget.EndDate = budgetUpdateDto.EndDate;
        if(budgetUpdateDto.ExpectedProfit != null) budget.ExpectedProfit = budgetUpdateDto.ExpectedProfit;
        if(budgetUpdateDto.Tax != null) budget.Tax = budgetUpdateDto.Tax;
        if(budgetUpdateDto.OverallExpense != null){  budget.OverallExpense = budgetUpdateDto.OverallExpense;}
        if(budgetUpdateDto.OverallRevenue != null) budget.OverallRevenue = budgetUpdateDto.OverallRevenue;
        
        
        if (budgetUpdateDto.Expenses?.Count > 0)
        {
            for (int i=0; i<budgetUpdateDto.Expenses.Count;i++)
            {
                if (budgetUpdateDto.Expenses[i].Id == 0)
                {
                    _unitOfWork.ExpensesOfBudget.Add(new ExpensesOfBudget(){
                        ExpensesAmount = budgetUpdateDto.Expenses[i].ExpensesAmount,
                        ExpensesTitle = budgetUpdateDto.Expenses[i].ExpensesTitle,
                        BudgetId = id,
                    });
                    continue;
                }
                
                var exp = _unitOfWork.ExpensesOfBudget.GetById(budgetUpdateDto.Expenses[i].Id);
                if (exp == null) continue;
                
                if(exp.ExpensesAmount != null) exp.ExpensesAmount = budgetUpdateDto.Expenses[i].ExpensesAmount;
                if(exp.ExpensesTitle != null) exp.ExpensesTitle = budgetUpdateDto.Expenses[i].ExpensesTitle;

                _unitOfWork.ExpensesOfBudget.Update(exp);

            }
        }
        
        if(budgetUpdateDto.Revenue?.Count > 0 )
        {
            for (int i=0;i<budgetUpdateDto.Revenue.Count;i++)
            {
                if (budgetUpdateDto.Revenue[i].Id == 0)
                {
                    _unitOfWork.Revenues.Add(new Revenue(){
                        RevenueAmount = budgetUpdateDto.Revenue[i].RevenueAmount,
                        RevenueTitle = budgetUpdateDto.Revenue[i].RevenueTitle,
                        BudgetId = id
                    });
                    continue;
                }

                var rev = _unitOfWork.Revenues.GetById(budgetUpdateDto.Revenue[i].Id);
                if (rev == null) continue;
                
                if (rev.RevenueAmount != null) rev.RevenueAmount = budgetUpdateDto.Revenue[i].RevenueAmount;
                if (rev.RevenueTitle != null) rev.RevenueTitle = budgetUpdateDto.Revenue[i].RevenueTitle;
                    
                _unitOfWork.Revenues.Update(rev);
            }
        }
        
        
        _unitOfWork.Budget.Update(budget);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var budget = _unitOfWork.Budget.GetById(id);
        if (budget == null) return Task.FromResult(0);
        budget.IsDeleted = true;
        budget.DeletedAt = DateTime.Now;
        _unitOfWork.Budget.Update(budget); 
        return _unitOfWork.SaveChangesAsync();
    }

    public BudgetReadDto? Get(int id)
    {
        var budget = _unitOfWork.Budget.GetWithDetails(id);
        if (budget == null) return null;
        return new BudgetReadDto()
        {
            Id = budget.Id,
            Title = budget.Title,
            Type = budget.Type,
            BudgetAmount = budget.BudgetAmount,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate,
            ExpectedProfit = budget.ExpectedProfit,
            Tax = budget.Tax,
            OverallExpense = budget.OverallExpense,
            OverallRevenue = budget.OverallRevenue,
            Revenues = _mapper.Map<List<Revenue>,List<RevenuesCreateDto>>(budget.Revenues) ,
            Expenses= _mapper.Map<List<ExpensesOfBudget>,List<ExpensesCreateDto>>(budget.Expenses)
           
        };
    }

    public Task<List<BudgetReadDto>> GetAll()
    {
        var budget = _unitOfWork.Budget.GetAllWithDetails();
        return Task.FromResult(budget.Result.Select(p => new BudgetReadDto()
        {
            Id = p.Id,
            Title = p.Title,
            Type = p.Type,
            BudgetAmount = p.BudgetAmount,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            ExpectedProfit = p.ExpectedProfit,
            Tax = p.Tax,
            OverallExpense = p.OverallExpense,
            OverallRevenue = p.OverallRevenue,
            Revenues = _mapper.Map<List<Revenue>,List<RevenuesCreateDto>>(p.Revenues) ,
            Expenses= _mapper.Map<List<ExpensesOfBudget>,List<ExpensesCreateDto>>(p.Expenses)
        }).ToList());
    }
    
     public async Task<FilteredBudgetDto> GetFilteredBudgetsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var budgets = await _unitOfWork.Budget.GetAllWithDetails();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = budgets.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var budgetList = budgets.ToList();

            var paginatedResults = budgetList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgets = new List<BudgetDto>();
            foreach (var budget in paginatedResults)
            {
                mappedBudgets.Add(new BudgetDto()
                {
                    Id = budget.Id,
                    Title = budget.Title,
                    Type = budget.Type,
                    BudgetAmount = budget.BudgetAmount,
                    ExpectedProfit = budget.ExpectedProfit,
                    StartDate = budget.StartDate,
                    EndDate = budget.EndDate,
                    Tax = budget.Tax,
                    OverallExpense =budget.OverallExpense,
                    OverallRevenue = budget.OverallRevenue,
                    Revenues = _mapper.Map<List<Revenue>,List<RevenuesBudget>>(budget.Revenues) ,
                    
                    Expenses= _mapper.Map<List<ExpensesOfBudget>,List<ExpensesBudget>>(budget.Expenses)
                   
                });
            }
            FilteredBudgetDto filteredBudgetDto = new()
            {
                BudgetDto = mappedBudgets,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetDto;
        }

        if (budgets != null)
        {
            IEnumerable<Budget> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(budgets, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(budgets, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var budgets = paginatedResults.ToList();
            // var mappedBudget = _mapper.Map<IEnumerable<Budget>, IEnumerable<BudgetDto>>(budgets);
            
            var mappedBudgets = new List<BudgetDto>();

            foreach (var budget in paginatedResults)
            {
                
                mappedBudgets.Add(new BudgetDto()
                {
                    Id = budget.Id,
                    Title = budget.Title,
                    Type = budget.Type,
                    BudgetAmount = budget.BudgetAmount,
                    ExpectedProfit = budget.ExpectedProfit,
                    StartDate = budget.StartDate,
                    EndDate = budget.EndDate,
                    Tax = budget.Tax,
                    OverallExpense =budget.OverallExpense,
                    OverallRevenue = budget.OverallRevenue,
                    Revenues = _mapper.Map<List<Revenue>,List<RevenuesBudget>>(budget.Revenues) ,
                    Expenses= _mapper.Map<List<ExpensesOfBudget>,List<ExpensesBudget>>(budget.Expenses)

                });
            }
            FilteredBudgetDto filteredBudgetDto = new()
            {
                BudgetDto = mappedBudgets,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredBudgetDto;
        }

        return new FilteredBudgetDto();
    }
    private IEnumerable<Budget> ApplyFilter(IEnumerable<Budget> budgets, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => budgets.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => budgets.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => budgets.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => budgets.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var budgetValue) => ApplyNumericFilter(budgets, column, budgetValue, operatorType),
            _ => budgets
        };
    }

    private IEnumerable<Budget> ApplyNumericFilter(IEnumerable<Budget> budgets, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => budgets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetValue) && budgetValue == value),
        "neq" => budgets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetValue) && budgetValue != value),
        "gte" => budgets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetValue) && budgetValue >= value),
        "gt" => budgets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetValue) && budgetValue > value),
        "lte" => budgets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetValue) && budgetValue <= value),
        "lt" => budgets.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetValue) && budgetValue < value),
        _ => budgets
    };
}


    public Task<List<BudgetDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Budget> budgetDto;
            budgetDto = _unitOfWork.Budget.GetAllWithDetails().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var budget = _mapper.Map<IEnumerable<Budget>, IEnumerable<BudgetDto>>(budgetDto);
            return Task.FromResult(budget.ToList());
        }

        var  budgetsDto = _unitOfWork.Budget.GlobalSearch(searchKey);
        var budgets = _mapper.Map<IEnumerable<Budget>, IEnumerable<BudgetDto>>(budgetsDto);
        return Task.FromResult(budgets.ToList());
    }

}
