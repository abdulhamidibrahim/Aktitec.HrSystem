
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrTicket.BL;

using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTicket.BL;

public class TicketFollowersManager:ITicketFollowersManager
{
    private readonly ITicketFollowersRepo _ticketFollowersRepo;

    public TicketFollowersManager(ITicketFollowersRepo ticketFollowersRepo)
    {
        _ticketFollowersRepo = ticketFollowersRepo;
    }
    
    public Task<int> Add(TicketFollowersAddDto ticketFollowersAddDto)
    {
        var ticket = new TicketFollowers()
        {
            EmployeeId = ticketFollowersAddDto.EmployeeId,
            TicketId = ticketFollowersAddDto.TicketId,
            
        };
        return _ticketFollowersRepo.Add(ticket);
    }

    public Task<int> Update(TicketFollowersUpdateDto ticketFollowersUpdateDto, int id)
    {
        var ticketFollowers = _ticketFollowersRepo.GetById(id);
        
        if (ticketFollowers.Result == null) return Task.FromResult(0);
        
        if(ticketFollowersUpdateDto.EmployeeId != null) 
            ticketFollowers.Result.EmployeeId = ticketFollowersUpdateDto.EmployeeId;
        if(ticketFollowersUpdateDto.TicketId != null) 
            ticketFollowers.Result.TicketId = ticketFollowersUpdateDto.TicketId;

        return _ticketFollowersRepo.Update(ticketFollowers.Result);
    }

    public Task<int> Delete(int id)
    {
        var ticketFollowers = _ticketFollowersRepo.GetById(id);
        if (ticketFollowers.Result != null) return _ticketFollowersRepo.Delete(ticketFollowers.Result);
        return Task.FromResult(0);
    }

    public Task<TicketFollowersReadDto>? Get(int id)
    {
        var ticketFollowers = _ticketFollowersRepo.GetById(id);
        if (ticketFollowers.Result == null) return null;
        return Task.FromResult(new TicketFollowersReadDto()
        {
            Id = ticketFollowers.Result.Id,
            EmployeeId = ticketFollowers.Result.EmployeeId,
            TicketId = ticketFollowers.Result.TicketId,
            
        });
    }

    public Task<List<TicketFollowersReadDto>> GetAll()
    {
        var ticketFollowers = _ticketFollowersRepo.GetAll();
        return Task.FromResult(ticketFollowers.Result.Select(note => new TicketFollowersReadDto()
        {
            Id = note.Id,
            EmployeeId = note.EmployeeId,
            TicketId = note.TicketId,
            
        }).ToList());
    }
}
