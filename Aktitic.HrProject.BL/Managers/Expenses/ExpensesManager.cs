
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ExpensesManager:IExpensesManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ExpensesManager( IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(ExpensesAddDto expensesAddDto)
    {
        var expenses = new Expenses()
        {
            ItemName = expensesAddDto.ItemName,
            PurchaseFrom = expensesAddDto.PurchaseFrom,
            PurchaseDate = expensesAddDto.PurchaseDate,
            PurchasedById = expensesAddDto.PurchasedById,
            Amount = expensesAddDto.Amount,
            PaidBy = expensesAddDto.PaidBy,
            Status = expensesAddDto.Status,
            Attachments = _mapper.Map<ICollection<FileDto>, ICollection<Document>>(expensesAddDto.Attachments),
            CreatedAt = DateTime.Now,
            
        }; 
        _unitOfWork.Expenses.Add(expenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ExpensesUpdateDto expensesUpdateDto, int id)
    {
        var expenses = _unitOfWork.Expenses.GetById(id);
        
        
        if (expenses == null) return Task.FromResult(0);
        
        if(expensesUpdateDto.ItemName!=null) expenses.ItemName = expensesUpdateDto.ItemName;
        if(expensesUpdateDto.PurchaseFrom!=null) expenses.PurchaseFrom = expensesUpdateDto.PurchaseFrom;
        if(expensesUpdateDto.PurchaseDate!=null) expenses.PurchaseDate = expensesUpdateDto.PurchaseDate;
        if(expensesUpdateDto.PurchasedById!=null) expenses.PurchasedById = expensesUpdateDto.PurchasedById;
        if(expensesUpdateDto.Amount!=null) expenses.Amount = expensesUpdateDto.Amount;
        if(expensesUpdateDto.PaidBy!=null) expenses.PaidBy = expensesUpdateDto.PaidBy;
        if(expensesUpdateDto.Status!=null) expenses.Status = expensesUpdateDto.Status;
        if(expensesUpdateDto.Attachments !=null) expenses.Attachments = _mapper.Map<ICollection<FileDto>, ICollection<Document>>(expensesUpdateDto.Attachments);

        expenses.UpdatedAt = DateTime.Now;
        _unitOfWork.Expenses.Update(expenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var expenses = _unitOfWork.Expenses.GetById(id);
        if (expenses==null) return Task.FromResult(0);
        expenses.IsDeleted = true;
        expenses.DeletedAt = DateTime.Now;
        _unitOfWork.Expenses.Update(expenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<ExpensesReadDto>? Get(int id)
    {
        var expenses = _unitOfWork.Expenses.GetExpensesWithEmployee(id);
        if (expenses == null) return null;
        return Task.FromResult(new ExpensesReadDto()
        {
            Id = expenses.Id,
            ItemName = expenses.ItemName,
            PurchaseFrom = expenses.PurchaseFrom,
            PurchaseDate = expenses.PurchaseDate,
            PurchasedBy = expenses.PurchasedById,
            Amount = expenses.Amount,
            PaidBy = expenses.PaidBy,
            Status = expenses.Status,
            Attachments = _mapper.Map<IEnumerable<Document>,IEnumerable<FileDto>>(expenses.Attachments)
        });
    }

    public Task<List<ExpensesReadDto>> GetAll()
    {
        var expenses = _unitOfWork.Expenses.GetAllExpensesWithEmployees();
        return Task.FromResult(expenses.Result.Select(note => new ExpensesReadDto()
        {
            Id = note.Id,
            ItemName = note.ItemName,
            PurchaseFrom = note.PurchaseFrom,
            PurchaseDate = note.PurchaseDate,
            PurchasedBy = note.PurchasedById,
            Amount = note.Amount,
            PaidBy = note.PaidBy,
            Status = note.Status,
            Attachments = _mapper.Map<IEnumerable<Document>,IEnumerable<FileDto>>(note.Attachments)
            
        }).ToList());
    }
    
     public async Task<FilteredExpensesDto> GetFilteredExpensesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var expensesList = await _unitOfWork.Expenses.GetAllExpensesWithEmployees();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = expensesList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var expenseList = expensesList.ToList();

            var paginatedResults = expenseList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedExpensess = new List<ExpensesDto>();
            foreach (var expenses in paginatedResults)
            {
                mappedExpensess.Add(new ExpensesDto()
                {
                    Id = expenses.Id,
                    ItemName = expenses.ItemName,
                    PurchaseDate = expenses.PurchaseDate,
                    PurchaseFrom = expenses.PurchaseFrom,
                    PurchasedBy = expenses.PurchasedBy?.FullName,
                    Amount = expenses.Amount,
                    PaidBy = expenses.PaidBy,
                    Status = expenses.Status,
                    Attachments = _mapper.Map<IEnumerable<Document>,IEnumerable<FileDto>>(expenses.Attachments) ,
                });
            }
            FilteredExpensesDto filteredExpensesDto = new()
            {
                ExpensesDto = mappedExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredExpensesDto;
        }

        if (expensesList != null)
        {
            IEnumerable<Expenses> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(expensesList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(expensesList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var expensess = paginatedResults.ToList();
            // var mappedExpenses = _mapper.Map<IEnumerable<Expenses>, IEnumerable<ExpensesDto>>(expensess);
            
            var mappedExpensess = new List<ExpensesDto>();

            foreach (var expenses in paginatedResults)
            {
                
                mappedExpensess.Add(new ExpensesDto()
                {
                    ItemName = expenses.ItemName,
                    PurchaseDate = expenses.PurchaseDate,
                    PurchaseFrom = expenses.PurchaseFrom,
                    PurchasedBy = expenses.PurchasedBy?.FullName,
                    Amount = expenses.Amount,
                    PaidBy = expenses.PaidBy,
                    Status = expenses.Status,
                    Attachments = _mapper.Map<IEnumerable<Document>,IEnumerable<FileDto>>(expenses.Attachments)
                    
                });
            
            }
            FilteredExpensesDto filteredExpensesDto = new()
            {
                ExpensesDto = mappedExpensess,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredExpensesDto;
        }

        return new FilteredExpensesDto();
    }
    private IEnumerable<Expenses> ApplyFilter(IEnumerable<Expenses> expenses, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => expenses.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => expenses.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => expenses.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => expenses.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var expensesValue) => ApplyNumericFilter(expenses, column, expensesValue, operatorType),
            _ => expenses
        };
    }

    private IEnumerable<Expenses> ApplyNumericFilter(IEnumerable<Expenses> expenses, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => expenses.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue == value),
        "neq" => expenses.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue != value),
        "gte" => expenses.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue >= value),
        "gt" => expenses.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue > value),
        "lte" => expenses.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue <= value),
        "lt" => expenses.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue < value),
        _ => expenses
    };
}


    public Task<List<ExpensesDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Expenses> expense;
            expense = _unitOfWork.Expenses.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var expenses = _mapper.Map<IEnumerable<Expenses>, IEnumerable<ExpensesDto>>(expense);
            return Task.FromResult(expenses.ToList());
        }

        var  expenseDtos = _unitOfWork.Expenses.GlobalSearch(searchKey);
        var expensess = _mapper.Map<IEnumerable<Expenses>, IEnumerable<ExpensesDto>>(expenseDtos);
        return Task.FromResult(expensess.ToList());
    }

}
