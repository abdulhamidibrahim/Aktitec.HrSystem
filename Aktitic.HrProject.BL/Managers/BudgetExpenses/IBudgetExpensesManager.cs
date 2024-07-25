using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IBudgetExpensesManager
{
    public Task<int> Add(BudgetExpensesAddDto budgetExpensesAddDto);
    public Task<int> Update(BudgetExpensesUpdateDto budgetExpensesUpdateDto, int id);
    public Task<int> Delete(int id);
    public BudgetExpensesReadDto? Get(int id);
    public Task<List<BudgetExpensesReadDto>> GetAll();
    public Task<FilteredBudgetExpensesDto> GetFilteredExpensesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<BudgetExpensesSearchDto>> GlobalSearch(string searchKey,string? column);
  
}