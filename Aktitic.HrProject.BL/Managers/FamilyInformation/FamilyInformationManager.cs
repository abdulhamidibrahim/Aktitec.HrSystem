using Aktitic.HrProject.BL.Dtos.EducationInformation;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class FamilyInformationManager(IUnitOfWork unitOfWork) : IFamilyInformationManager
{
    public async Task<int> Add(FamilyInformationAddDto familyInformationAddDto)
    {
        var familyInformation = new FamilyInformation()
        {
            UserId = familyInformationAddDto.UserId,
            Name = familyInformationAddDto.Name,
            Relationship = familyInformationAddDto.Relationship,
            Phone = familyInformationAddDto.Phone,
            DoB = familyInformationAddDto.DoB
        };

        unitOfWork.FamilyInformation.Add(familyInformation);
        return await unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(FamilyInformationAddDto familyInformationDto, int id)
    {
        var familyInfo = unitOfWork.FamilyInformation.GetById(id);
        if (familyInfo is not null)
        {
            if (familyInformationDto.UserId != 0)
                familyInfo.UserId = familyInformationDto.UserId;
            if (!familyInformationDto.Name.IsNullOrEmpty())
                familyInfo.Name = familyInformationDto.Name;
            if (!familyInformationDto.Relationship.IsNullOrEmpty())
                familyInfo.Relationship = familyInformationDto.Relationship;
            if (!familyInformationDto.Phone.IsNullOrEmpty())
                familyInfo.Phone = familyInformationDto.Phone;
            if (!familyInformationDto.DoB.Equals(default))
                familyInfo.DoB = familyInformationDto.DoB;

            unitOfWork.FamilyInformation.Update(familyInfo);
        }

        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var familyInfo = unitOfWork.FamilyInformation.GetById(id);
        if (familyInfo is not null)
        {
            familyInfo.IsDeleted = true;
            familyInfo.DeletedAt = DateTime.Now;
            unitOfWork.FamilyInformation.Update(familyInfo);
            return unitOfWork.SaveChangesAsync();
        }

        return Task.FromResult(0);
    }

    public async Task<List<FamilyInformationReadDto>> GetAll(int userId)
    {
        var familyInfos = await unitOfWork.FamilyInformation.GetByUserId(userId);
        familyInfos = familyInfos.ToList();
        return (familyInfos.Select(x => new FamilyInformationReadDto()
        {
            Id = x.Id,
            Name = x.Name,
            Relationship = x.Relationship,
            Phone = x.Phone,
            DoB = x.DoB,
            UserId = x.UserId
        }).ToList());
    }
}

