

using Aktitic.HrProject.BL.Dtos.ProfileExperience;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace Aktitic.HrTaskList.BL
{
    public class ProfileExperienceManager(IUnitOfWork unitOfWork) : IProfileExperienceManager
    {
        public async Task<int> Add(ProfileExperienceAddDto profileExperienceAddDto)
        {
            var profileExperience = new ProfileExperience()
            {
                UserId = profileExperienceAddDto.UserId,
                CompanyName = profileExperienceAddDto.CompanyName,
                JobPosition = profileExperienceAddDto.JobPosition,
                Location = profileExperienceAddDto.Location,
                PeriodFrom = profileExperienceAddDto.PeriodFrom,
                PeriodTo = profileExperienceAddDto.PeriodTo
            };

            unitOfWork.ProfileExperience.Add(profileExperience);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Update(ProfileExperienceUpdateDto profileExperienceUpdateDto, int id)
        {
            var profileExperience = unitOfWork.ProfileExperience.GetById(id);
            if (profileExperience != null)
            {
                if(profileExperience.UserId != 0)
                    profileExperience.UserId = profileExperienceUpdateDto.UserId;
                if(!profileExperience.CompanyName.IsNullOrEmpty())
                    profileExperience.CompanyName = profileExperienceUpdateDto.CompanyName;
                if(!profileExperience.JobPosition.IsNullOrEmpty())
                    profileExperience.JobPosition = profileExperienceUpdateDto.JopPosition;
                if(!profileExperience.Location.IsNullOrEmpty())
                    profileExperience.Location = profileExperienceUpdateDto.Location;
                if(!profileExperience.PeriodFrom.Equals(DateOnly.MinValue))
                    profileExperience.PeriodFrom = profileExperienceUpdateDto.PeriodFrom;
                if(!profileExperience.PeriodTo.Equals(DateOnly.MinValue)) 
                    profileExperience.PeriodTo = profileExperienceUpdateDto.PeriodTo;

                unitOfWork.ProfileExperience.Update(profileExperience);
                return await unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> Delete(int id)
        {
            var profileExperience = unitOfWork.ProfileExperience.GetById(id);
            if (profileExperience != null)
            {
                profileExperience.IsDeleted = true;
                profileExperience.DeletedAt = DateTime.Now;
                unitOfWork.ProfileExperience.Update(profileExperience);
                return await unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<List<ProfileExperienceReadDto>> GetByUserId(int userId)
        {
            var profileExperiences = await unitOfWork.ProfileExperience.GetByUserId(userId);
            return profileExperiences.Select(x => new ProfileExperienceReadDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                CompanyName = x.CompanyName,
                JobPosition = x.JobPosition,
                Location = x.Location,
                PeriodFrom = x.PeriodFrom,
                PeriodTo = x.PeriodTo
            }).ToList();
        }
    }
}