using Aktitic.HrProject.BL;

namespace Aktitic.HrProject.BL;

public interface ILeaveSettingsManager
{
    public void Add(LeaveSettingAddDto leaveSettingAddDto);
    public void Update(LeaveSettingUpdateDto leaveSettingUpdateDto,int id);
    public void Delete(int id);
    public LeaveSettingReadDto? Get(int id);
    public List<LeaveSettingReadDto> GetAll();
}