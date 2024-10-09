
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;

namespace Aktitic.HrProject.BL;

public interface IApplicationUserManager
{
    public ApplicationUser Add(ApplicationUserAddDto applicationUserAddDto);
    public ApplicationUser Create(ApplicationUserAddDto applicationUserAddDto);
    public Task<int> Update(ApplicationUserUpdateDto applicationUserUpdateDto, int id);
    public Task<int> Delete(int id);
    public ApplicationUserReadDto? Get(int id);
    public Task<List<ApplicationUserReadDto>> GetAll();
    public Task<FilteredApplicationUserDto> GetFilteredApplicationUsersAsync(int companyId, string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ApplicationUserDto>> GlobalSearch(int companyId, string searchKey,string? column);
    // bool IsEmailUnique(string email);
}