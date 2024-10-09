using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrTaskList.BL;

public interface ITrainingListManager
{
    public Task<int> Add(TrainingListAddDto trainingListAddDto);
    public Task<int> Update(TrainingListUpdateDto trainingListUpdateDto, int id);
    public Task<int> Delete(int id);
    public TrainingListReadDto? Get(int id);
    public Task<List<TrainingListReadDto>> GetAll();
    public Task<FilteredTrainingListDto> GetFilteredTrainingListsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TrainingListDto>> GlobalSearch(string searchKey,string? column);
  
}