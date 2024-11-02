using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.EmergencyContact;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class EmergencyContactManager(IUnitOfWork unitOfWork) : IEmergencyContactManager
{
    public async Task<int> Add(EmergencyContactAddDto emergencyContactAddDto)
    {
        var emergencyContact = new EmergencyContact
        {
            PrimaryName = emergencyContactAddDto.PrimaryName,
            PrimaryPhone = emergencyContactAddDto.PrimaryPhone,
            PrimaryRelationship = emergencyContactAddDto.PrimaryPhoneTwo,
            PrimaryPhoneTwo = emergencyContactAddDto.PrimaryRelationship,
            SecondaryName = emergencyContactAddDto.SecondaryName,
            SecondaryPhone = emergencyContactAddDto.SecondaryPhone,
            SecondaryRelationship = emergencyContactAddDto.SecondaryPhoneTwo,
            SecondaryPhoneTwo = emergencyContactAddDto.SecondaryRelationship,
            UserId = emergencyContactAddDto.UserId
        };
        
        unitOfWork.EmergencyContact.Add(emergencyContact);
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(EmergencyContactAddDto emergencyContactDto,int id)
    {
       var emergencyContact = unitOfWork.EmergencyContact.GetById(id);

       if (emergencyContact is null) return Task.FromResult(0);
       
           if(emergencyContactDto.UserId != 0)
               emergencyContact.UserId = emergencyContactDto.UserId;
           if (!emergencyContactDto.PrimaryName.IsNullOrEmpty()) 
               emergencyContact.PrimaryName = emergencyContactDto.PrimaryName;
           if (!emergencyContactDto.PrimaryPhone.IsNullOrEmpty())
               emergencyContact.PrimaryPhone = emergencyContactDto.PrimaryPhone;
           if (!emergencyContactDto.PrimaryPhoneTwo.IsNullOrEmpty())
               emergencyContact.PrimaryPhoneTwo = emergencyContactDto.PrimaryPhoneTwo;
           if (!emergencyContactDto.PrimaryRelationship.IsNullOrEmpty())
               emergencyContact.PrimaryRelationship = emergencyContactDto.PrimaryRelationship;
           
        
           if (!emergencyContactDto.SecondaryName.IsNullOrEmpty()) 
               emergencyContact.SecondaryName = emergencyContactDto.SecondaryName;
           if (!emergencyContactDto.SecondaryPhone.IsNullOrEmpty())
               emergencyContact.SecondaryPhone = emergencyContactDto.SecondaryPhone;
           if (!emergencyContactDto.SecondaryPhoneTwo.IsNullOrEmpty())
               emergencyContact.SecondaryPhoneTwo = emergencyContactDto.SecondaryPhoneTwo;
           if (!emergencyContactDto.SecondaryRelationship.IsNullOrEmpty())
               emergencyContact.SecondaryRelationship = emergencyContactDto.SecondaryRelationship;
           
           unitOfWork.EmergencyContact.Update(emergencyContact);
      

       return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var emergencyContact = unitOfWork.EmergencyContact.GetById(id);
        if (emergencyContact is not null)
        {
            emergencyContact.IsDeleted = true;
            emergencyContact.DeletedAt = DateTime.Now;
            unitOfWork.EmergencyContact.Update(emergencyContact);
            return unitOfWork.SaveChangesAsync();
        }

        return Task.FromResult(0);
    }

    public async Task<EmergencyContactReadDto> GetAll(int userId)
    {
        var emergencyContact = await unitOfWork.EmergencyContact.GetByUserId(userId);
        if (emergencyContact is null) return new EmergencyContactReadDto();
        
        return  new EmergencyContactReadDto
        {
            Id = emergencyContact.Id,
            PrimaryName = emergencyContact.PrimaryName,
            PrimaryPhone = emergencyContact.PrimaryPhone,
            PrimaryPhoneTwo = emergencyContact.PrimaryPhoneTwo,
            PrimaryRelationship = emergencyContact.PrimaryRelationship,
            SecondaryName = emergencyContact.SecondaryName,
            SecondaryPhone = emergencyContact.SecondaryPhone,
            SecondaryPhoneTwo = emergencyContact.SecondaryPhoneTwo,
            SecondaryRelationship = emergencyContact.SecondaryRelationship,
            UserId = emergencyContact.UserId
        };
    }
}
