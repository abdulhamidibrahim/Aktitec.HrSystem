using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IBudgetManager
{
    public Task<int> Add(BudgetAddDto budgetAddDto);
    public Task<int> Update(BudgetUpdateDto budgetUpdateDto, int id);
    public Task<int> Delete(int id);
    public BudgetReadDto? Get(int id);
    public Task<List<BudgetReadDto>> GetAll();
    public Task<FilteredBudgetDto> GetFilteredBudgetsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<BudgetDto>> GlobalSearch(string searchKey,string? column);
  
}