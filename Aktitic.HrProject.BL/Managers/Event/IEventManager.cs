using Aktitic.HrProject.BL;

namespace Aktitic.HrTaskList.BL;

public interface IEventManager
{
    public EventReadDto Add(EventAddDto eventAddDto);
    public Task<int> Update(EventUpdateDto eventUpdateDto, int id);
    public Task<int> Delete(int id);
    public EventReadDto? Get(int id);
    public Task<List<EventReadDto>> GetAll();
    public Task<List<EventReadDto>> GetByMonth(int month,int year);
   
}