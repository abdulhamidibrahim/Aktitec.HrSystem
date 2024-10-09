using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class BudgetExpensesManager : IBudgetExpensesManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BudgetExpensesManager( IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }

    public Task<int> Add(BudgetExpensesAddDto budgetExpensesAddDto)
    {
        var budgetExpenses = new BudgetExpenses()
        {
            Amount = budgetExpensesAddDto.Amount,
            Currency = budgetExpensesAddDto.Currency,
            Note = budgetExpensesAddDto.Note,
            Date = budgetExpensesAddDto.Date,
            CategoryId = budgetExpensesAddDto.CategoryId,
            Subcategory = budgetExpensesAddDto.Subcategory,

        };
        
        budgetExpenses.CreatedAt=DateTime.Now;
        
        _unitOfWork.BudgetExpenses.Add(budgetExpenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(BudgetExpensesUpdateDto budgetExpensesUpdateDto, int id)
    {
        var budgetExpenses = _unitOfWork.BudgetExpenses.GetById(id);


        if (budgetExpenses == null) return Task.FromResult(0);

        if (budgetExpensesUpdateDto.Amount != null) budgetExpenses.Amount = budgetExpensesUpdateDto.Amount;
        if (budgetExpensesUpdateDto.Currency != null) budgetExpenses.Currency = budgetExpensesUpdateDto.Currency;
        if (budgetExpensesUpdateDto.Note != null) budgetExpenses.Note = budgetExpensesUpdateDto.Note;
        if (budgetExpensesUpdateDto.Date != null) budgetExpenses.Date = budgetExpensesUpdateDto.Date;
        if (budgetExpensesUpdateDto.CategoryId != null) budgetExpenses.CategoryId = budgetExpensesUpdateDto.CategoryId;
        if (budgetExpensesUpdateDto.Subcategory != null)
            budgetExpenses.Subcategory = budgetExpensesUpdateDto.Subcategory;

        
        budgetExpenses.UpdatedAt = DateTime.Now;
        _unitOfWork.BudgetExpenses.Update(budgetExpenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var budgetExpenses = _unitOfWork.BudgetExpenses.GetById(id);
        if (budgetExpenses == null) return Task.FromResult(0);
        budgetExpenses.IsDeleted = true;
        
        budgetExpenses.DeletedAt = DateTime.Now;
        _unitOfWork.BudgetExpenses.Update(budgetExpenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public BudgetExpensesReadDto? Get(int id)
    {
        var budgetExpenses = _unitOfWork.BudgetExpenses.GetWithCategory(id);
        if (budgetExpenses == null) return null;
        return new BudgetExpensesReadDto()
        {
            Id = budgetExpenses.Id,
            Amount = budgetExpenses.Amount,
            Currency = budgetExpenses.Currency,
            Note = budgetExpenses.Note,
            Date = budgetExpenses.Date,
            CategoryId = budgetExpenses.CategoryId,
            Subcategory = budgetExpenses.Subcategory,
            Category = _mapper.Map<Category, CategoryDto>(budgetExpenses.Category).CategoryName ?? "",

        };
    }

    public Task<List<BudgetExpensesReadDto>> GetAll()
    {
        var budgetExpenses = _unitOfWork.BudgetExpenses.GetAllWithCategoryAsync();
        return Task.FromResult(budgetExpenses.Result.Select(p => new BudgetExpensesReadDto()
        {
            Id = p.Id,
            Amount = p.Amount,
            Currency = p.Currency,
            Note = p.Note,
            Date = p.Date,
            CategoryId = p.CategoryId,
            Subcategory = p.Subcategory,
            Category = _mapper.Map<Category, CategoryDto>(p.Category).CategoryName ?? "",

        }).ToList());
    }



    public async Task<FilteredBudgetExpensesDto> GetFilteredExpensesAsync(string? column, string? value1,
        string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var budgetExpensess = await _unitOfWork.BudgetExpenses.GetAllWithCategoryAsync();


        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = budgetExpensess.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var budgetExpensesList = budgetExpensess.ToList();

            var paginatedResults = budgetExpensesList.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedBudgetsExpensess = new List<BudgetExpensesDto>();
            foreach (var budgetExpenses in paginatedResults)
            {
                mappedBudgetsExpensess.Add(new BudgetExpensesDto()
                {
                    Id = budgetExpenses.Id,
                    Amount = budgetExpenses.Amount,
                    Currency = budgetExpenses.Currency,
                    Note = budgetExpenses.Note,
                    Date = budgetExpenses.Date,
                    CategoryId = budgetExpenses.CategoryId,
                    Subcategory = budgetExpenses.Subcategory,
                    Category = _mapper.Map<Category, CategoryDto>(budgetExpenses.Category).CategoryName ?? "",

                });
            }

            FilteredBudgetExpensesDto filteredBudgetsExpensesDto = new()
            {
                BudgetExpensesDto = mappedBudgetsExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (budgetExpensess != null)
        {
            IEnumerable<BudgetExpenses> filteredResults;

            // Apply the first filter
            filteredResults = ApplyFilter(budgetExpensess, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(budgetExpensess, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList(); // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var budgetExpensess = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(budgetExpensess);

            var mappedBudgetExpenses = new List<BudgetExpensesDto>();

            foreach (var budgetExpenses in paginatedResults)
            {

                mappedBudgetExpenses.Add(new BudgetExpensesDto()
                {
                    Id = budgetExpenses.Id,
                    Amount = budgetExpenses.Amount,
                    Currency = budgetExpenses.Currency,
                    Note = budgetExpenses.Note,
                    Date = budgetExpenses.Date,
                    CategoryId = budgetExpenses.CategoryId,
                    Subcategory = budgetExpenses.Subcategory,
                    Category = _mapper.Map<Category, CategoryDto>(budgetExpenses.Category).CategoryName ?? "",

                });
            }

            FilteredBudgetExpensesDto filteredBudgetExpensesDto = new()
            {
                BudgetExpensesDto = mappedBudgetExpenses,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredBudgetExpensesDto;
        }

        return new FilteredBudgetExpensesDto();
    }

    private IEnumerable<BudgetExpenses> ApplyFilter(IEnumerable<BudgetExpenses> budgetExpensess, string? column, string? value,
        string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => budgetExpensess.Where(e =>
                value != null && column != null &&
                e.GetPropertyValue(column).Contains(value, StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => budgetExpensess.SkipWhile(e =>
                value != null && column != null &&
                e.GetPropertyValue(column).Contains(value, StringComparison.OrdinalIgnoreCase)),
            "startswith" => budgetExpensess.Where(e =>
                value != null && column != null &&
                e.GetPropertyValue(column).StartsWith(value, StringComparison.OrdinalIgnoreCase)),
            "endswith" => budgetExpensess.Where(e =>
                value != null && column != null &&
                e.GetPropertyValue(column).EndsWith(value, StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var budgetExpensesValue) => ApplyNumericFilter(budgetExpensess, column,
                budgetExpensesValue, operatorType),
            _ => budgetExpensess
        };
    }

    private IEnumerable<BudgetExpenses> ApplyNumericFilter(IEnumerable<BudgetExpenses> budgetExpensess, string? column,
        decimal? value, string? operatorType)
    {
        return operatorType?.ToLower() switch
        {
            "eq" => budgetExpensess.Where(e =>
                column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetExpensesValue) &&
                budgetExpensesValue == value),
            "neq" => budgetExpensess.Where(e =>
                column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetExpensesValue) &&
                budgetExpensesValue != value),
            "gte" => budgetExpensess.Where(e =>
                column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetExpensesValue) &&
                budgetExpensesValue >= value),
            "gt" => budgetExpensess.Where(e =>
                column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetExpensesValue) &&
                budgetExpensesValue > value),
            "lte" => budgetExpensess.Where(e =>
                column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetExpensesValue) &&
                budgetExpensesValue <= value),
            "lt" => budgetExpensess.Where(e =>
                column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetExpensesValue) &&
                budgetExpensesValue < value),
            _ => budgetExpensess
        };
    }


    public Task<List<BudgetExpensesSearchDto>> GlobalSearch(string searchKey, string? column)
    {
        if (column != null)
        {
            IEnumerable<BudgetExpenses> budgetExpensesDto;
            budgetExpensesDto = _unitOfWork.BudgetExpenses.GetAllWithCategoryAsync().Result
                .Where(e => e.GetPropertyValue(column)
                    .ToLower()
                    .Contains(searchKey, StringComparison.OrdinalIgnoreCase));
            ICollection<BudgetExpensesSearchDto> budgetExpensesSearch = new List<BudgetExpensesSearchDto>();
            foreach (var budgetExpenses in budgetExpensesDto)
            {

                budgetExpensesSearch.Add(new BudgetExpensesSearchDto()
                {
                    Id = budgetExpenses.Id,
                    Amount = budgetExpenses.Amount,
                    Currency = budgetExpenses.Currency,
                    Note = budgetExpenses.Note,
                    Date = budgetExpenses.Date,
                    CategoryId = budgetExpenses.CategoryId,
                    Subcategory = budgetExpenses.Subcategory,
                    Category = _mapper.Map<Category, CategoryDto>(budgetExpenses.Category).CategoryName ?? "",
                });
            }

            return Task.FromResult(budgetExpensesSearch.ToList());

        }

        var budgetExpensess = _unitOfWork.BudgetExpenses.GlobalSearch(searchKey);
        ICollection<BudgetExpensesSearchDto> budgetExpensesSearchDto = new List<BudgetExpensesSearchDto>();
        foreach (var budgetExpenses in budgetExpensess)
        {

            budgetExpensesSearchDto.Add(new BudgetExpensesSearchDto()
            {
                Id = budgetExpenses.Id,
                Amount = budgetExpenses.Amount,
                Currency = budgetExpenses.Currency,
                Note = budgetExpenses.Note,
                Date = budgetExpenses.Date,
                CategoryId = budgetExpenses.CategoryId,
                Subcategory = budgetExpenses.Subcategory,
                Category = _mapper.Map<Category, CategoryDto>(budgetExpenses.Category).CategoryName ?? "",
            });
        }

        return Task.FromResult(budgetExpensesSearchDto.ToList());

    }
}
