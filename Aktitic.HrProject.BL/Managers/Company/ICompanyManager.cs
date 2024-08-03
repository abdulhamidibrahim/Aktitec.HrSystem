namespace Aktitic.HrProject.BL.Managers.Company;

public interface ICompanyManager
{
    public Task<int> Add(CompanyAddDto companyAddDto);
    public Task<int> AddAdmin(CompanyAddDto companyAddDto);
    public Task<int> Update(CompanyUpdateDto companyUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<CompanyReadDto>? Get(int id);
    public Task<IEnumerable<CompanyReadDto>> GetAll();
    public Task<FilteredCompanyDto> GetFilteredCompaniesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);
    public Task<List<CompanyReadDto>> GlobalSearch(string searchKey,string? column);
}