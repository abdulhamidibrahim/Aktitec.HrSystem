
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

public class CategoryManager:ICategoryManager
{
    private readonly ICategoryRepo _categoryRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryManager(ICategoryRepo categoryRepo, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<int> Add(CategoryAddDto categoryAddDto)
    {
        var category = new Category()
        {
            CategoryName = categoryAddDto.CategoryName,
            SubcategoryName = categoryAddDto.SubcategoryName,
        }; 
        _categoryRepo.Add(category);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(CategoryUpdateDto categoryUpdateDto, int id)
    {
        var category = _categoryRepo.GetById(id);
        
        
        if (category == null) return Task.FromResult(0);
        
        if(categoryUpdateDto.CategoryName != null) category.CategoryName = categoryUpdateDto.CategoryName;
        if(categoryUpdateDto.SubcategoryName != null) category.SubcategoryName = categoryUpdateDto.SubcategoryName;
        
        
        _categoryRepo.Update(category);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _categoryRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public CategoryReadDto? Get(int id)
    {
        var category = _categoryRepo.GetById(id);
        if (category == null) return null;
        return new CategoryReadDto()
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            SubcategoryName = category.SubcategoryName,
           
        };
    }

    public Task<List<CategoryReadDto>> GetAll()
    {
        var category = _categoryRepo.GetAll();
        return Task.FromResult(category.Result.Select(p => new CategoryReadDto()
        {
            Id = p.Id,
            CategoryName = p.CategoryName,
            SubcategoryName = p.SubcategoryName,
            

        }).ToList());
    }
    
     public async Task<FilteredCategoryDto> GetFilteredCategoriesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _categoryRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
    
            var mappedCategorys = new List<CategoryDto>();
            foreach (var category in paginatedResults)
            {
                mappedCategorys.Add(new CategoryDto()
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    SubcategoryName = category.SubcategoryName,
                   
                });
            }
            FilteredCategoryDto filteredCategoryDto = new()
            {
                CategoryDto = mappedCategorys,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredCategoryDto;
        }

        if (users != null)
        {
            IEnumerable<Category> filteredResults;
        
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

            // var categorys = paginatedResults.ToList();
            // var mappedCategory = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categorys);
            
            var mappedCategorys = new List<CategoryDto>();

            foreach (var category in paginatedResults)
            {
                
                mappedCategorys.Add(new CategoryDto()
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    SubcategoryName = category.SubcategoryName,
                });
            }
            FilteredCategoryDto filteredCategoryDto = new()
            {
                CategoryDto = mappedCategorys,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredCategoryDto;
        }

        return new FilteredCategoryDto();
    }
    private IEnumerable<Category> ApplyFilter(IEnumerable<Category> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var categoryValue) => ApplyNumericFilter(users, column, categoryValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Category> ApplyNumericFilter(IEnumerable<Category> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue < value),
        _ => users
    };
}


    public Task<List<CategoryDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Category> user;
            user = _categoryRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var category = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(user);
            return Task.FromResult(category.ToList());
        }

        var  users = _categoryRepo.GlobalSearch(searchKey);
        var categorys = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(users);
        return Task.FromResult(categorys.ToList());
    }

}
