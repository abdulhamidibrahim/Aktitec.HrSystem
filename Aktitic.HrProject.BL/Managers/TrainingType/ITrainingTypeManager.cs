using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrTaskList.BL;

public interface ITrainingTypeManager
{
    public Task<int> Add(TrainingTypeAddDto trainingTypeAddDto);
    public Task<int> Update(TrainingTypeUpdateDto trainingTypeUpdateDto, int id);
    public Task<int> Delete(int id);
    public TrainingTypeReadDto? Get(int id);
    public Task<List<TrainingTypeReadDto>> GetAll();
    public Task<FilteredTrainingTypeDto> GetFilteredTrainingTypesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TrainingTypeDto>> GlobalSearch(string searchKey,string? column);
  
}