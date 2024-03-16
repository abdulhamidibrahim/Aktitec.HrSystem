using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface IShiftManager
{
    public void Add(ShiftAddDto shiftAddDto);
    public void Update(ShiftUpdateDto shiftUpdateDto,int id);
    public void Delete(int id);
    public ShiftReadDto? Get(int id);
    public List<ShiftReadDto> GetAll();
}