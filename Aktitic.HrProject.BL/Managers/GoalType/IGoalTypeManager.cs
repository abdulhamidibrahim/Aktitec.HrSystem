using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrTaskList.BL;

public interface IGoalTypeManager
{
    public Task<int> Add(GoalTypeAddDto goalTypeAddDto);
    public Task<int> Update(GoalTypeUpdateDto goalTypeUpdateDto, int id);
    public Task<int> Delete(int id);
    public GoalTypeReadDto? Get(int id);
    public Task<List<GoalTypeReadDto>> GetAll();
    public Task<FilteredGoalTypeDto> GetFilteredGoalTypesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<GoalTypeDto>> GlobalSearch(string searchKey,string? column);
  
}