using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IDepartmentManager
{
    public Task<int> Add(DepartmentAddDto departmentAddDto);
    public Task<int> Update(DepartmentUpdateDto departmentUpdateDto, int id);
    public Task<int> Delete(int id);
    public DepartmentReadDto? Get(int id);
    public List<DepartmentReadDto> GetAll();
    
    public Task<FilteredDepartmentDto> GetFilteredDepartmentsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<DepartmentDto>> GlobalSearch(string searchKey,string? column);
}