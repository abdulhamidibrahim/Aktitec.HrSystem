using Aktitic.HrProject.DAL.Pagination.Client;

namespace Aktitic.HrProject.BL;

public interface IClientManager
{
    public Task<int> Add(ClientAddDto clientAddDto);
    public Task<int> Update(ClientUpdateDto clientUpdateDto, int id);
    public Task<int> Delete(int id);
    public ClientReadDto? Get(int id);
    public Task<List<ClientReadDto>> GetAll();
    public Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit);
    public Task<FilteredClientDto> GetFilteredClientsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<ClientDto>> GlobalSearch(string searchKey,string? column);
    bool IsEmailUnique(string email);
}