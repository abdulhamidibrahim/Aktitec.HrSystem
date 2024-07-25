using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ITrainerManager
{
    public Task<int> Add(TrainerAddDto trainerAddDto);
    public Task<int> Update(TrainerUpdateDto trainerUpdateDto, int id);
    public Task<int> Delete(int id);
    public TrainerReadDto? Get(int id);
    public Task<List<TrainerReadDto>> GetAll();
    public Task<FilteredTrainerDto> GetFilteredTrainersAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TrainerDto>> GlobalSearch(string searchKey,string? column);
  
}