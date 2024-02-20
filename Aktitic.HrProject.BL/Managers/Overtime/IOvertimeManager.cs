using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IOvertimeManager
{
    public void Add(OvertimeAddDto overtimeAddDto);
    public void Update(OvertimeUpdateDto overtimeUpdateDto);
    public void Delete(OvertimeDeleteDto overtimeDeleteDto);
    public OvertimeReadDto? Get(int id);
    public List<OvertimeReadDto> GetAll();
}