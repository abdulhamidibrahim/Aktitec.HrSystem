
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTicket.BL;

public class TicketFollowersManager:ITicketFollowersManager
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketFollowersManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(TicketFollowersAddDto ticketFollowersAddDto)
    {
        var ticket = new TicketFollowers()
        {
            EmployeeId = ticketFollowersAddDto.EmployeeId,
            TicketId = ticketFollowersAddDto.TicketId,
            CreatedAt = DateTime.Now,
            
        };
         _unitOfWork.TicketFollowers.Add(ticket);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TicketFollowersUpdateDto ticketFollowersUpdateDto, int id)
    {
        var ticketFollowers = _unitOfWork.TicketFollowers.GetById(id);

        if (ticketFollowers == null) return Task.FromResult(0);
        
        if(ticketFollowersUpdateDto.EmployeeId != null) 
            ticketFollowers.EmployeeId = ticketFollowersUpdateDto.EmployeeId;
        if(ticketFollowersUpdateDto.TicketId != null) 
            ticketFollowers.TicketId = ticketFollowersUpdateDto.TicketId;

        ticketFollowers.UpdatedAt = DateTime.Now;
        _unitOfWork.TicketFollowers.Update(ticketFollowers);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var ticketFollowers = _unitOfWork.TicketFollowers.GetById(id);
        if (ticketFollowers==null) return Task.FromResult(0);
        ticketFollowers.IsDeleted = true;
        ticketFollowers.DeletedAt = DateTime.Now;
        _unitOfWork.TicketFollowers.Update(ticketFollowers);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<TicketFollowersReadDto>? Get(int id)
    {
        var ticketFollowers = _unitOfWork.TicketFollowers.GetById(id);
        if (ticketFollowers == null) return null;
        return Task.FromResult(new TicketFollowersReadDto()
        {
            Id = ticketFollowers.Id,
            EmployeeId = ticketFollowers.EmployeeId,
            TicketId = ticketFollowers.TicketId,
            
        });
    }

    public Task<List<TicketFollowersReadDto>> GetAll()
    {
        var ticketFollowers = _unitOfWork.TicketFollowers.GetAll();
        return Task.FromResult(ticketFollowers.Result.Select(note => new TicketFollowersReadDto()
        {
            Id = note.Id,
            EmployeeId = note.EmployeeId,
            TicketId = note.TicketId,
            
        }).ToList());
    }
}
