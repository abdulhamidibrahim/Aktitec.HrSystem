using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IExpensesManager
{
    public Task<int> Add(ExpensesAddDto expensesAddDto);
    public Task<int> Update(ExpensesUpdateDto expensesUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<ExpensesReadDto>? Get(int id);
    public Task<List<ExpensesReadDto>> GetAll();
    public Task<FilteredExpensesDto> GetFilteredExpensesAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ExpensesDto>> GlobalSearch(string searchKey,string? column);
  
}