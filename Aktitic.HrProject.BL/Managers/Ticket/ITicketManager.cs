using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public interface ITicketManager
{
    public Task<int> Add(TicketAddDto ticketAddDto);
    public Task<int> Update(TicketUpdateDto ticketUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<TicketReadDto>? Get(int id);
    public Task<List<TicketReadDto>> GetAll();
    
    public Task<FilteredTicketDto> GetFilteredTicketsAsync(string column, string value1, string? operator1, string? value2, string? operator2, int page, int pageSize);

    public Task<List<TicketDto>> GlobalSearch(string searchKey,string? column);
}