using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface ILeavesManager
{
    public Task<int> Add(LeavesAddDto leavesAddDto);
    public Task<int> Update(LeavesUpdateDto leavesUpdateDto, int id);
    public Task<int> Delete(int id);
    public LeavesReadDto? Get(int id);
    public List<LeavesReadDto> GetAll();
    
    public Task<FilteredLeavesDto> GetFilteredLeavesAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<LeavesDto>> GlobalSearch(string searchKey,string? column);
}