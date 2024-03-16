using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IDesignationManager
{
    public Task<int> Add(DesignationAddDto designationAddDto);
    public Task<int> Update(DesignationUpdateDto designationUpdateDto,int id);
    public Task<int> Delete(int id);
    public DesignationReadDto? Get(int id);
    public List<DesignationReadDto> GetAll();
    public Task<FilteredDesignationDto> GetFilteredDesignationsAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<DesignationDto>> GlobalSearch(string searchKey,string? column);
}