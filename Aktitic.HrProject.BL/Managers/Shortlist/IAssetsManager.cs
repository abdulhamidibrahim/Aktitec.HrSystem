using Aktitic.HrProject.BL;

namespace Aktitic.HrTaskList.BL;

public interface IShortlistsManager
{
    public Task<int> Add(ShortlistAddDto shortlistsAddDto);
    public Task<int> Update(ShortlistUpdateDto shortlistsUpdateDto, int id);
    public Task<int> Delete(int id);
    public ShortlistReadDto? Get(int id);
    public Task<List<ShortlistReadDto>> GetAll();
    public Task<FilteredShortlistsDto> GetFilteredShortlistsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ShortlistDto>> GlobalSearch(string searchKey,string? column);
  
}