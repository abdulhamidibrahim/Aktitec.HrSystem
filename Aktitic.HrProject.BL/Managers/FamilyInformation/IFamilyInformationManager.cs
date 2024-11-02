using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.EducationInformation;
using Aktitic.HrProject.BL.Dtos.EmergencyContact;

namespace Aktitic.HrTaskList.BL;

public interface IFamilyInformationManager
{
    public Task<int> Add(FamilyInformationAddDto educationContactAddDto);
    public Task<int> Update(FamilyInformationAddDto educationContactDto,int id);
    public Task<int> Delete(int id);
    public Task<List<FamilyInformationReadDto>> GetAll(int userId);
   
}