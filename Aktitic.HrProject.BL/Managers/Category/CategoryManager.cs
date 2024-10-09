using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class CategoryManager:ICategoryManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryManager( IUnitOfWork unitOfWork, IMapper mapper)
    {
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
        category.CreatedAt = DateTime.Now;
        
        _unitOfWork.Category.Add(category);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(CategoryUpdateDto categoryUpdateDto, int id)
    {
        var category = _unitOfWork.Category.GetById(id);
        
        
        if (category == null) return Task.FromResult(0);
        
        if(categoryUpdateDto.CategoryName != null) category.CategoryName = categoryUpdateDto.CategoryName;
        if(categoryUpdateDto.SubcategoryName != null) category.SubcategoryName = categoryUpdateDto.SubcategoryName;

        category.UpdatedAt = DateTime.Now;
        _unitOfWork.Category.Update(category);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var category = _unitOfWork.Category.GetById(id);
        if (category == null) return Task.FromResult(0);
        category.IsDeleted = true;
        category.DeletedAt = DateTime.Now;
        _unitOfWork.Category.Update(category);
        return _unitOfWork.SaveChangesAsync();
    }

    public CategoryReadDto? Get(int id)
    {
        var category = _unitOfWork.Category.GetById(id);
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
        var category = _unitOfWork.Category.GetAll();
        return Task.FromResult(category.Result.Select(p => new CategoryReadDto()
        {
            Id = p.Id,
            CategoryName = p.CategoryName,
            SubcategoryName = p.SubcategoryName,
            
        }).ToList());
    }
    
     public async Task<FilteredCategoryDto> GetFilteredCategoriesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var Categories = await _unitOfWork.Category.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = Categories.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var CategoryList = Categories.ToList();

            var paginatedResults = CategoryList.Skip((page - 1) * pageSize).Take(pageSize);
    
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

        if (Categories != null)
        {
            IEnumerable<Category> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(Categories, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(Categories, column, value2, operator2));
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
    private IEnumerable<Category> ApplyFilter(IEnumerable<Category> Categorys, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => Categorys.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => Categorys.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => Categorys.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => Categorys.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var categoryValue) => ApplyNumericFilter(Categorys, column, categoryValue, operatorType),
            _ => Categorys
        };
    }

    private IEnumerable<Category> ApplyNumericFilter(IEnumerable<Category> Categorys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => Categorys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue == value),
        "neq" => Categorys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue != value),
        "gte" => Categorys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue >= value),
        "gt" => Categorys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue > value),
        "lte" => Categorys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue <= value),
        "lt" => Categorys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var categoryValue) && categoryValue < value),
        _ => Categorys
    };
}


    public Task<List<CategoryDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Category> Category;
            Category = _unitOfWork.Category.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var category = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(Category);
            return Task.FromResult(category.ToList());
        }

        var  Categorys = _unitOfWork.Category.GlobalSearch(searchKey);
        var categorys = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(Categorys);
        return Task.FromResult(categorys.ToList());
    }

}
