using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrTaskList.BL;

public interface IGoalListManager
{
    public Task<int> Add(GoalListAddDto goalListAddDto);
    public Task<int> Update(GoalListUpdateDto goalListUpdateDto, int id);
    public Task<int> Delete(int id);
    public GoalListReadDto? Get(int id);
    public Task<List<GoalListReadDto>> GetAll();
    public Task<FilteredGoalListDto> GetFilteredGoalListsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<GoalListDto>> GlobalSearch(string searchKey,string? column);
  
}