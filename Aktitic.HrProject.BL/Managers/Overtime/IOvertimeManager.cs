using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IOvertimeManager
{
    public Task<int> Add(OvertimeAddDto overtimeAddDto);
    public Task<int> Update(OvertimeUpdateDto overtimeUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<OvertimeReadDto>? Get(int id);
    public Task<List<OvertimeReadDto>> GetAll();
    public Task<FilteredOvertimeDto> GetFilteredOvertimesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<OvertimeDto>> GlobalSearch(string searchKey,string? column);
}