using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IOvertimeManager
{
    public void Add(OvertimeAddDto overtimeAddDto);
    public void Update(OvertimeUpdateDto overtimeUpdateDto,int id);
    public void Delete(int id);
    public OvertimeReadDto? Get(int id);
    public List<OvertimeReadDto> GetAll();
}