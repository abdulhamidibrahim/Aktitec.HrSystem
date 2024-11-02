using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.EmergencyContact;

namespace Aktitic.HrTaskList.BL;

public interface IEmergencyContactManager
{
    public Task<int> Add(EmergencyContactAddDto emergencyContactAddDto);
    public Task<int> Update(EmergencyContactAddDto emergencyContactDto,int id);
    public Task<int> Delete(int id);
    public Task<EmergencyContactReadDto> GetAll(int userId);
   
}