using Aktitic.HrProject.BL;

namespace Aktitic.HrTaskList.BL;

public interface IScheduleTimingsManager
{
    public Task<int> Add(ScheduleTimingsAddDto scheduleTimingsAddDto);
    public Task<int> Update(ScheduleTimingsUpdateDto scheduleTimingsUpdateDto, int id);
    public Task<int> Delete(int id);
    public ScheduleTimingsReadDto? Get(int id);
    public Task<List<ScheduleTimingsReadDto>> GetAll();
    public Task<FilteredScheduleTimingsDto> GetFilteredScheduleTimingsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ScheduleTimingsDto>> GlobalSearch(string searchKey,string? column);
  
}