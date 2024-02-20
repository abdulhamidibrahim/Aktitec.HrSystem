using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IDesignationManager
{
    public void Add(DesignationAddDto designationAddDto);
    public void Update(DesignationUpdateDto designationUpdateDto);
    public void Delete(DesignationDeleteDto designationDeleteDto);
    public DesignationReadDto? Get(int id);
    public List<DesignationReadDto> GetAll();
}