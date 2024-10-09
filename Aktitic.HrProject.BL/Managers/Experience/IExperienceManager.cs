using Aktitic.HrProject.BL;

namespace Aktitic.HrTaskList.BL;

public interface IExperienceManager
{
    public Task<int> Add(ExperienceAddDto experienceAddDto);
    public Task<int> Update(ExperienceUpdateDto experienceUpdateDto, int id);
    public Task<int> Delete(int id);
    public ExperienceReadDto? Get(int id);
    public Task<List<ExperienceReadDto>> GetAll();
    public Task<FilteredExperiencesDto> GetFilteredExperienceAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ExperienceDto>> GlobalSearch(string searchKey,string? column);
  
}