using Aktitic.HrProject.BL;

namespace Aktitic.HrTaskList.BL;

public interface IAptitudeResultsManager
{
    public Task<int> Add(AptitudeResultsAddDto aptitudeResultsAddDto);
    public Task<int> Update(AptitudeResultsUpdateDto aptitudeResultsUpdateDto, int id);
    public Task<int> Delete(int id);
    public AptitudeResultsReadDto? Get(int id);
    public Task<List<AptitudeResultsReadDto>> GetAll();
    public Task<FilteredAptitudeResultsDto> GetFilteredAptitudeResultsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<AptitudeResultsDto>> GlobalSearch(string searchKey,string? column);
  
}