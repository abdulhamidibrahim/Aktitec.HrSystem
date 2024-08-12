using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ICandidatesManager
{
    public Task<int> Add(CandidatesAddDto assetsAddDto);
    public Task<int> Update(CandidatesUpdateDto assetsUpdateDto, int id);
    public Task<int> Delete(int id);
    public CandidatesReadDto? Get(int id);
    public Task<List<CandidatesReadDto>> GetAll();
    public Task<FilteredCandidatesDto> GetFilteredCandidatesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<CandidatesDto>> GlobalSearch(string searchKey,string? column);
  
}