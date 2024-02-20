using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IHolidayManager
{
    public void Add(HolidayAddDto holidayAddDto);
    public void Update(HolidayUpdateDto holidayUpdateDto);
    public void Delete(HolidayDeleteDto holidayDeleteDto);
    public HolidayReadDto? Get(int id);
    public List<HolidayReadDto> GetAll();
}