using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ICategoryManager
{
    public Task<int> Add(CategoryAddDto categoryAddDto);
    public Task<int> Update(CategoryUpdateDto categoryUpdateDto, int id);
    public Task<int> Delete(int id);
    public CategoryReadDto? Get(int id);
    public Task<List<CategoryReadDto>> GetAll();
    public Task<FilteredCategoryDto> GetFilteredCategoriesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<CategoryDto>> GlobalSearch(string searchKey,string? column);
  
}