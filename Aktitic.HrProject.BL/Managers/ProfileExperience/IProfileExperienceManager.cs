using Aktitic.HrProject.BL.Dtos.ProfileExperience;

namespace Aktitic.HrTaskList.BL
{
    public interface IProfileExperienceManager
    {
        Task<int> Add(ProfileExperienceAddDto profileExperienceAddDto);
        Task<int> Update(ProfileExperienceUpdateDto profileExperienceUpdateDto, int id);
        Task<int> Delete(int id);
        Task<List<ProfileExperienceReadDto>> GetByUserId(int userId);
    }
}
