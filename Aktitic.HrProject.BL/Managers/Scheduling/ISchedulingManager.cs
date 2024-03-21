using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface ISchedulingManager
{
    public Task<int> Add(SchedulingAddDto schedulingAddDto);
    public Task<int> Update(SchedulingUpdateDto schedulingUpdateDto,int id);
    public Task<int> Delete(int id);
    public Task<SchedulingReadDto>? Get(int id);
    public Task<List<SchedulingReadDto>> GetAll();
    public Task<List<FilteredSchedulingDto>> GetAllEmployeesScheduling (int page, int pageSize);
    public Task<List<ScheduleDto>> GlobalSearch(string searchKey,string? column);

}