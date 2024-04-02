
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using File = Aktitic.HrProject.DAL.Models.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ExpensesManager:IExpensesManager
{
    private readonly IExpensesRepo _expensesRepo;
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ExpensesManager(IExpensesRepo expensesRepo, IUnitOfWork unitOfWork, IMapper mapper, IItemRepo itemRepo)
    {
        _expensesRepo = expensesRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _itemRepo = itemRepo;
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
            Attachments = _mapper.Map<IEnumerable<FileDto>, IEnumerable<File>>(expensesAddDto.Attachments)
            
            
        }; 
        _expensesRepo.Add(expenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ExpensesUpdateDto expensesUpdateDto, int id)
    {
        var expenses = _expensesRepo.GetById(id);
        
        
        if (expenses == null) return Task.FromResult(0);
        
        if(expensesUpdateDto.ItemName!=null) expenses.ItemName = expensesUpdateDto.ItemName;
        if(expensesUpdateDto.PurchaseFrom!=null) expenses.PurchaseFrom = expensesUpdateDto.PurchaseFrom;
        if(expensesUpdateDto.PurchaseDate!=null) expenses.PurchaseDate = expensesUpdateDto.PurchaseDate;
        if(expensesUpdateDto.PurchasedById!=null) expenses.PurchasedById = expensesUpdateDto.PurchasedById;
        if(expensesUpdateDto.Amount!=null) expenses.Amount = expensesUpdateDto.Amount;
        if(expensesUpdateDto.PaidBy!=null) expenses.PaidBy = expensesUpdateDto.PaidBy;
        if(expensesUpdateDto.Status!=null) expenses.Status = expensesUpdateDto.Status;
        if(expensesUpdateDto.Attachments !=null) expenses.Attachments = _mapper.Map<IEnumerable<FileDto>, IEnumerable<File>>(expensesUpdateDto.Attachments);
        
        _expensesRepo.Update(expenses);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _expensesRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<ExpensesReadDto>? Get(int id)
    {
        var expenses = _expensesRepo.GetEstimateWithEmployee(id);
        if (expenses == null) return null;
        return Task.FromResult(new ExpensesReadDto()
        {
            Id = expenses.Id,
            ItemName = expenses.ItemName,
            PurchaseFrom = expenses.PurchaseFrom,
            PurchaseDate = expenses.PurchaseDate,
            PurchasedBy = expenses.PurchasedBy?.FullName,
            Amount = expenses.Amount,
            PaidBy = expenses.PaidBy,
            Status = expenses.Status,
            Attachments = _mapper.Map<IEnumerable<File>,IEnumerable<FileDto>>(expenses.Attachments)
        });
    }

    public Task<List<ExpensesReadDto>> GetAll()
    {
        var expenses = _expensesRepo.GetAllEstimateWithEmployees();
        return Task.FromResult(expenses.Result.Select(note => new ExpensesReadDto()
        {
            Id = note.Id,
            ItemName = note.ItemName,
            PurchaseFrom = note.PurchaseFrom,
            PurchaseDate = note.PurchaseDate,
            PurchasedBy = note.PurchasedBy?.FullName,
            Amount = note.Amount,
            PaidBy = note.PaidBy,
            Status = note.Status,
            Attachments = _mapper.Map<IEnumerable<File>,IEnumerable<FileDto>>(note.Attachments)
            
        }).ToList());
    }
    
     public async Task<FilteredExpensesDto> GetFilteredExpensesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _expensesRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
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
                    Attachments = _mapper.Map<IEnumerable<File>,IEnumerable<FileDto>>(expenses.Attachments)                });
            }
            FilteredExpensesDto filteredExpensesDto = new()
            {
                ExpensesDto = mappedExpensess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredExpensesDto;
        }

        if (users != null)
        {
            IEnumerable<Expenses> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(users, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
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
                    Attachments = _mapper.Map<IEnumerable<File>,IEnumerable<FileDto>>(expenses.Attachments)
                    
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
    private IEnumerable<Expenses> ApplyFilter(IEnumerable<Expenses> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var expensesValue) => ApplyNumericFilter(users, column, expensesValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Expenses> ApplyNumericFilter(IEnumerable<Expenses> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var expensesValue) && expensesValue < value),
        _ => users
    };
}


    public Task<List<ExpensesDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Expenses> user;
            user = _expensesRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var expenses = _mapper.Map<IEnumerable<Expenses>, IEnumerable<ExpensesDto>>(user);
            return Task.FromResult(expenses.ToList());
        }

        var  users = _expensesRepo.GlobalSearch(searchKey);
        var expensess = _mapper.Map<IEnumerable<Expenses>, IEnumerable<ExpensesDto>>(users);
        return Task.FromResult(expensess.ToList());
    }

}
