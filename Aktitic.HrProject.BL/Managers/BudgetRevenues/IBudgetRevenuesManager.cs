using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IBudgetRevenuesManager
{
    public Task<int> Add(BudgetRevenuesAddDto budgetRevenuesAddDto);
    public Task<int> Update(BudgetRevenuesUpdateDto budgetRevenuesUpdateDto, int id);
    public Task<int> Delete(int id);
    public BudgetRevenuesReadDto? Get(int id);
    public Task<List<BudgetRevenuesReadDto>> GetAll();
    public Task<FilteredBudgetRevenuesDto> GetFilteredRevenuesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<BudgetRevenuesSearchDto>> GlobalSearch(string searchKey,string? column);
  
}