using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface IAssetsManager
{
    public Task<int> Add(AssetsAddDto assetsAddDto);
    public Task<int> Update(AssetsUpdateDto assetsUpdateDto, int id);
    public Task<int> Delete(int id);
    public AssetsReadDto? Get(int id);
    public Task<List<AssetsReadDto>> GetAll();
    public Task<FilteredAssetsDto> GetFilteredAssetsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<AssetsDto>> GlobalSearch(string searchKey,string? column);
  
}