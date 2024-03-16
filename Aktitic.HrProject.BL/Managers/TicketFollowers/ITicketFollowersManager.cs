using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public interface ITicketFollowersManager
{
    public Task<int> Add(TicketFollowersAddDto ticketFollowersAddDto);
    public Task<int> Update(TicketFollowersUpdateDto ticketFollowersUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<TicketFollowersReadDto>? Get(int id);
    public Task<List<TicketFollowersReadDto>> GetAll();
  
}