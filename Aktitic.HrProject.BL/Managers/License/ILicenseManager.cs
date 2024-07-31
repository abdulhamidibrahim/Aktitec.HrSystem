using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrTaskList.BL;

public interface ILicenseManager
{
    public Task<int> Add(LicenseAddDto licenseAddDto);
    public Task<int> Update(LicenseUpdateDto licenseUpdateDto, int id);
    public Task<int> Delete(int id);
    public LicenseReadDto? Get(int id);
    public Task<List<LicenseReadDto>> GetAll();
    public Task<FilteredLicensesDto> GetFilteredLicenseAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<LicenseDto>> GlobalSearch(string searchKey,string? column);
  
}