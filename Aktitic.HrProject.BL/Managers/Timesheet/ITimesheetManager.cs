using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ITimesheetManager
{
    public void Add(TimesheetAddDto timesheetAddDto);
    public void Update(TimesheetUpdateDto timesheetUpdateDto,int id);
    public void Delete(int id);
    public TimesheetReadDto? Get(int id);
    public List<TimesheetReadDto> GetAll();
}