namespace Aktitic.HrProject.BL;

public interface ILeaveSettingsManager
{
    public Task<int> Add(LeaveSettingAddDto leaveSettingAddDto);
    public Task<int> Update(LeaveSettingUpdateDto leaveSettingUpdateDto, int id);
    public Task<int> Delete(int id);
    public LeaveSettingReadDto? Get(int id);
    public List<LeaveSettingReadDto> GetAll();
}