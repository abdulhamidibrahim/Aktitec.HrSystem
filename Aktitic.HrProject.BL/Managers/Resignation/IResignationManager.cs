using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrTaskList.BL;

public interface IResignationManager
{
    public Task<int> Add(ResignationAddDto resignationAddDto);
    public Task<int> Update(ResignationUpdateDto resignationUpdateDto, int id);
    public Task<int> Delete(int id);
    public ResignationReadDto? Get(int id);
    public Task<List<ResignationReadDto>> GetAll();
    public Task<FilteredResignationsDto> GetFilteredResignationAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ResignationDto>> GlobalSearch(string searchKey,string? column);
  
}