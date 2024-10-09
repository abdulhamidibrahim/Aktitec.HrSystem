using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface ITerminationManager
{
    public Task<int> Add(TerminationAddDto terminationAddDto);
    public Task<int> Update(TerminationUpdateDto terminationUpdateDto, int id);
    public Task<int> Delete(int id);
    public TerminationReadDto? Get(int id);
    public Task<List<TerminationReadDto>> GetAll();
    public Task<FilteredTerminationsDto> GetFilteredTerminationAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TerminationDto>> GlobalSearch(string searchKey,string? column);
  
}