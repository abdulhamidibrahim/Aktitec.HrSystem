using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Dtos.EducationInformation;
using Aktitic.HrProject.BL.Dtos.EmergencyContact;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class EducationInformationManager(IUnitOfWork unitOfWork) : IEducationInformationManager
{
    public async Task<int> Add(EducationInformationAddDto educationContactAddDto)
    {
        var educationContact = new EducationInformation()
        {
            Institution = educationContactAddDto.Institution,
            Subject = educationContactAddDto.Subject,
            Degree = educationContactAddDto.Degree,
            Grade = educationContactAddDto.Grade,
            CompleteDate = educationContactAddDto.CompleteDate,
            StartingDate = educationContactAddDto.StartingDate,
            UserId = educationContactAddDto.UserId
        };
        
        unitOfWork.EducationInformation.Add(educationContact);
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(EducationInformationAddDto educationContactDto, int id)
    {
            
       var eduInfo = unitOfWork.EducationInformation.GetById(id);
       if (eduInfo is not null)
       {
           if (educationContactDto.UserId != 0)
               eduInfo.UserId = educationContactDto.UserId;
           if (!educationContactDto.Institution.IsNullOrEmpty())
               eduInfo.Institution = educationContactDto.Institution;
           if (!educationContactDto.Subject.IsNullOrEmpty())
               eduInfo.Subject = educationContactDto.Subject;
           if (!educationContactDto.Degree.IsNullOrEmpty())
               eduInfo.Degree = educationContactDto.Degree;
           if (!educationContactDto.Grade.IsNullOrEmpty())
               eduInfo.Grade = educationContactDto.Grade;
           if (!educationContactDto.StartingDate.Equals(DateOnly.MinValue))
               eduInfo.StartingDate = educationContactDto.StartingDate;
           if (!educationContactDto.CompleteDate.Equals(DateOnly.MinValue))
               eduInfo.CompleteDate = educationContactDto.CompleteDate;

           unitOfWork.EducationInformation.Update(eduInfo);
       }

       return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var educationContact = unitOfWork.EducationInformation.GetById(id);
        if (educationContact is not null)
        {
            educationContact.IsDeleted = true;
            educationContact.DeletedAt = DateTime.Now;
            unitOfWork.EducationInformation.Update(educationContact);
            return unitOfWork.SaveChangesAsync();
        }

        return Task.FromResult(0);
    }

    public async Task<List<EducationInformationReadDto>> GetAll(int userId)
    {
        var educationContacts = await unitOfWork.EducationInformation.GetByUserId(userId);
        educationContacts = educationContacts.ToList();    
        return (educationContacts.Select(x => new EducationInformationReadDto()
        {
            Id = x.Id,
            Institution = x.Institution,
            Subject = x.Subject,
            Grade = x.Grade,
            Degree = x.Degree,
            StartingDate = x.StartingDate,
            CompleteDate = x.CompleteDate,
            UserId = x.UserId
        }).ToList());
    }
}
