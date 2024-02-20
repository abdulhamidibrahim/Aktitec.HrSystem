using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ILeavesManager
{
    public void Add(LeavesAddDto leavesAddDto);
    public void Update(LeavesUpdateDto leavesUpdateDto);
    public void Delete(LeavesDeleteDto leavesDeleteDto);
    public LeavesReadDto? Get(int id);
    public List<LeavesReadDto> GetAll();
}