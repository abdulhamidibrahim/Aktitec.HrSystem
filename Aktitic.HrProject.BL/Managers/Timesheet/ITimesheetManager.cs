using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface ITimesheetManager
{
    public Task<int> Add(TimesheetAddDto timesheetAddDto);
    public Task<int> Update(TimesheetUpdateDto timesheetUpdateDto,int id);
    public Task<int> Delete(int id);
    public Task<TimesheetReadDto?> Get(int id);
    public Task<List<TimesheetReadDto>> GetAll();
    public Task<FilteredTimeSheetDto> GetFilteredTimeSheetsAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TimeSheetDto>> GlobalSearch(string searchKey,string? column);
}