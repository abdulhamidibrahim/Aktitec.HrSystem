using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ISchedulingManager
{
    public void Add(SchedulingAddDto schedulingAddDto);
    public void Update(SchedulingUpdateDto schedulingUpdateDto);
    public void Delete(SchedulingDeleteDto schedulingDeleteDto);
    public SchedulingReadDto? Get(int id);
    public List<SchedulingReadDto> GetAll();
}