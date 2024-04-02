using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IHolidayManager
{
    public Task<int> Add(HolidayAddDto holidayAddDto);
    public Task<int> Update(HolidayUpdateDto holidayUpdateDto, int id);
    public Task<int> Delete(int id);
    public HolidayReadDto? Get(int id);
    public List<HolidayReadDto> GetAll();
    
    public Task<FilteredHolidayDto> GetFilteredHolidaysAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<HolidayDto>> GlobalSearch(string searchKey,string? column);
}