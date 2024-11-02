using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.EducationInformation;
using Aktitic.HrProject.BL.Dtos.EmergencyContact;

namespace Aktitic.HrTaskList.BL;

public interface IEducationInformationManager
{
    public Task<int> Add(EducationInformationAddDto emergencyContactAddDto);
    public Task<int> Update(EducationInformationAddDto emergencyContactDto,int id);
    public Task<int> Delete(int id);
    public Task<List<EducationInformationReadDto>> GetAll(int userId);
   
}