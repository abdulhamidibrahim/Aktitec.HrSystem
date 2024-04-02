using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IShiftManager
{
    public Task<int> Add(ShiftAddDto shiftAddDto);
    public Task<int> Update(ShiftUpdateDto shiftUpdateDto, int id);
    public Task<int> Delete(int id);
    public ShiftReadDto? Get(int id);
    public Task<List<ShiftReadDto>> GetAll();
    public Task<FilteredShiftsDto> GetFilteredShiftsAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ShiftDto>> GlobalSearch(string searchKey,string? column);
}