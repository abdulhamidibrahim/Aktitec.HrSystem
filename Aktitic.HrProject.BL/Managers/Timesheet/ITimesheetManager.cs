using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ITimesheetManager
{
    public void Add(TimesheetAddDto timesheetAddDto);
    public void Update(TimesheetUpdateDto timesheetUpdateDto);
    public void Delete(TimesheetDeleteDto timesheetDeleteDto);
    public TimesheetReadDto? Get(int id);
    public List<TimesheetReadDto> GetAll();
}