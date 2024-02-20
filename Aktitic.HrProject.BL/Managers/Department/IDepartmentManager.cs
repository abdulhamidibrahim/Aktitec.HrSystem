using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IDepartmentManager
{
    public void Add(DepartmentAddDto departmentAddDto);
    public void Update(DepartmentUpdateDto departmentUpdateDto);
    public void Delete(DepartmentDeleteDto departmentDeleteDto);
    public DepartmentReadDto? Get(int id);
    public List<DepartmentReadDto> GetAll();
}