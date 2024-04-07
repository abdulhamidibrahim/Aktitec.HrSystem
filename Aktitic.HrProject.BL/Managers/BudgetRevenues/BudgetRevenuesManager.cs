
using System.Collections;
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

public class BudgetRevenuesManager:IBudgetRevenuesManager
{
    private readonly IBudgetRevenuesRepo _budgetRevenuesRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BudgetRevenuesManager(IBudgetRevenuesRepo budgetRevenuesRepo, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _budgetRevenuesRepo = budgetRevenuesRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
       
    }
    
    public Task<int> Add(BudgetRevenuesAddDto budgetRevenuesAddDto)
    {
        var budgetRevenues = new BudgetRevenue()
        {
            Amount = budgetRevenuesAddDto.Amount,
            Currency = budgetRevenuesAddDto.Currency,
            Note = budgetRevenuesAddDto.Note,
            Date = budgetRevenuesAddDto.Date,
            CategoryId = budgetRevenuesAddDto.CategoryId,
            Subcategory = budgetRevenuesAddDto.Subcategory,
           
        }; 
        _budgetRevenuesRepo.Add(budgetRevenues);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(BudgetRevenuesUpdateDto budgetRevenuesUpdateDto, int id)
    {
        var budgetRevenues = _budgetRevenuesRepo.GetById(id);
        
        
        if (budgetRevenues == null) return Task.FromResult(0);
        
        if(budgetRevenuesUpdateDto.Amount != null) budgetRevenues.Amount = budgetRevenuesUpdateDto.Amount;
        if(budgetRevenuesUpdateDto.Currency != null) budgetRevenues.Currency = budgetRevenuesUpdateDto.Currency;
        if(budgetRevenuesUpdateDto.Note != null) budgetRevenues.Note = budgetRevenuesUpdateDto.Note;
        if(budgetRevenuesUpdateDto.Date != null) budgetRevenues.Date = budgetRevenuesUpdateDto.Date;
        if(budgetRevenuesUpdateDto.CategoryId != null) budgetRevenues.CategoryId = budgetRevenuesUpdateDto.CategoryId;
        if(budgetRevenuesUpdateDto.Subcategory != null) budgetRevenues.Subcategory = budgetRevenuesUpdateDto.Subcategory;
        
        
        _budgetRevenuesRepo.Update(budgetRevenues);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _budgetRevenuesRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public BudgetRevenuesReadDto? Get(int id)
    {
        var budgetRevenues = _budgetRevenuesRepo.GetWithCategory(id);
        if (budgetRevenues == null) return null;
        return new BudgetRevenuesReadDto()
        {
            Id = budgetRevenues.Id,
            Amount = budgetRevenues.Amount,
            Currency = budgetRevenues.Currency,
            Note = budgetRevenues.Note,
            Date = budgetRevenues.Date,
            CategoryId = budgetRevenues.CategoryId,
            Subcategory = budgetRevenues.Subcategory,
            Category = _mapper.Map<Category,CategoryDto>(budgetRevenues.Category).CategoryName ?? "",
           
        };
    }

    public Task<List<BudgetRevenuesReadDto>> GetAll()
    {
        var budgetRevenues = _budgetRevenuesRepo.GetAllWithCategoryAsync();
        return Task.FromResult(budgetRevenues.Result.Select(p => new BudgetRevenuesReadDto()
        {
            Id = p.Id,
            Amount = p.Amount,
            Currency = p.Currency,
            Note = p.Note,
            Date = p.Date,
            CategoryId = p.CategoryId,
            Subcategory = p.Subcategory,
            Category = _mapper.Map<Category,CategoryDto>(p.Category).CategoryName ?? "",
            
        }).ToList());
    }
    
     public async Task<FilteredBudgetRevenuesDto> GetFilteredCategoriesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _budgetRevenuesRepo.GetAllWithCategoryAsync();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedBudgetsRevenuess = new List<BudgetRevenuesDto>();
            foreach (var budgetRevenues in paginatedResults)
            {
                mappedBudgetsRevenuess.Add(new BudgetRevenuesDto()
                {
                    Id = budgetRevenues.Id,
                    Amount  = budgetRevenues.Amount,
                    Currency = budgetRevenues.Currency,
                    Note = budgetRevenues.Note,
                    Date = budgetRevenues.Date,
                    CategoryId = budgetRevenues.CategoryId,
                    Subcategory = budgetRevenues.Subcategory,
                    Category = _mapper.Map<Category,CategoryDto>(budgetRevenues.Category).CategoryName ?? "",
                    
                });
            }
            FilteredBudgetRevenuesDto filteredBudgetsRevenuesDto = new()
            {
                BudgetRevenuesDto = mappedBudgetsRevenuess,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsRevenuesDto;
        }

        if (users != null)
        {
            IEnumerable<BudgetRevenue> filteredResults;
        
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

            // var budgetRevenuess = paginatedResults.ToList();
            // var mappedBudgetsRevenues = _mapper.Map<IEnumerable<BudgetsRevenues>, IEnumerable<BudgetsRevenuesDto>>(budgetRevenuess);
            
            var mappedBudgetRevenues = new List<BudgetRevenuesDto>();

            foreach (var budgetRevenues in paginatedResults)
            {
                
                mappedBudgetRevenues.Add(new BudgetRevenuesDto()
                {
                    Id = budgetRevenues.Id,
                    Amount  = budgetRevenues.Amount,
                    Currency = budgetRevenues.Currency,
                    Note = budgetRevenues.Note,
                    Date = budgetRevenues.Date,
                    CategoryId = budgetRevenues.CategoryId,
                    Subcategory = budgetRevenues.Subcategory,
                    Category = _mapper.Map<Category,CategoryDto>(budgetRevenues.Category).CategoryName ?? "",
                    
                });
            }
            FilteredBudgetRevenuesDto filteredBudgetRevenuesDto = new()
            {
                BudgetRevenuesDto = mappedBudgetRevenues,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredBudgetRevenuesDto;
        }

        return new FilteredBudgetRevenuesDto();
    }
    private IEnumerable<BudgetRevenue> ApplyFilter(IEnumerable<BudgetRevenue> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var budgetRevenuesValue) => ApplyNumericFilter(users, column, budgetRevenuesValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<BudgetRevenue> ApplyNumericFilter(IEnumerable<BudgetRevenue> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetRevenuesValue) && budgetRevenuesValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetRevenuesValue) && budgetRevenuesValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetRevenuesValue) && budgetRevenuesValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetRevenuesValue) && budgetRevenuesValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetRevenuesValue) && budgetRevenuesValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var budgetRevenuesValue) && budgetRevenuesValue < value),
        _ => users
    };
}


    public Task<List<BudgetRevenuesDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<BudgetRevenue> user;
            user = _budgetRevenuesRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var budgetRevenues = _mapper.Map<IEnumerable<BudgetRevenue>, IEnumerable<BudgetRevenuesDto>>(user);
            return Task.FromResult(budgetRevenues.ToList());
        }

        var  users = _budgetRevenuesRepo.GlobalSearch(searchKey);
        var budgetRevenuess = _mapper.Map<IEnumerable<BudgetRevenue>, IEnumerable<BudgetRevenuesDto>>(users);
        return Task.FromResult(budgetRevenuess.ToList());
    }

}
