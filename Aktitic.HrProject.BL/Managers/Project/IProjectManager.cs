using Aktitic.HrProject.DAL.Dtos;

namespace Aktitic.HrProject.BL;

public interface IProjectManager
{
    public Task<int> Add(ProjectAddDto projectAddDto);
    public Task<int> Update(ProjectUpdateDto projectUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<ProjectReadDto?> Get(int id);
    public Task<List<ProjectReadDto>> GetAll();
  
    public FilteredProjectDto GetFilteredProjectsAsync(string? column, string? value1, string? operator1, string? value2,
        string? operator2, int page, int pageSize);

    public Task<List<ProjectDto>> GlobalSearch(string searchKey,string? column);
}