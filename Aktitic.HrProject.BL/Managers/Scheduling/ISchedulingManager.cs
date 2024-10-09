using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface ISchedulingManager
{
    public Task<int> Add(SchedulingAddDto schedulingAddDto);
    public Task<int> Update(SchedulingUpdateDto schedulingUpdateDto, int id);
    public Task<int> Delete(int id);
    public SchedulingReadDto? Get(int id);
    public Task<List<SchedulingReadDto>> GetAll();
    public List<FilteredSchedulingDto> GetAllEmployeesScheduling(int page, int pageSize);
    public List<FilteredSchedulingDto> GetAllEmployeesScheduling(int page, int pageSize, DateOnly? startDate);
    public Task<List<ScheduleDto>> GlobalSearch(string searchKey,string? column);

}