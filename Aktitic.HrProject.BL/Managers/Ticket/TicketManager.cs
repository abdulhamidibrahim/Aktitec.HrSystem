
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrTicket.BL;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTicket.BL;

public class TicketManager:ITicketManager
{
    private readonly ITicketRepo _ticketRepo;
    private readonly IMapper _mapper;

    public TicketManager(ITicketRepo ticketRepo, IMapper mapper)
    {
        _ticketRepo = ticketRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(TicketAddDto ticketAddDto)
    {
        var ticket = new Ticket()
        {
           Subject = ticketAddDto.Subject,
           Description = ticketAddDto.Description,
           AssignedToEmployeeId = ticketAddDto.AssignedToEmployeeId,
           Cc = ticketAddDto.Cc,
           ClientId = ticketAddDto.ClientId,
           CreatedByEmployeeId = ticketAddDto.CreatedByEmployeeId,
           Priority = ticketAddDto.Priority,
           Status = ticketAddDto.Status,
            
        };
        return _ticketRepo.Add(ticket);
    }

    public Task<int> Update(TicketUpdateDto ticketUpdateDto, int id)
    {
        var ticket = _ticketRepo.GetById(id);
        
        if (ticket.Result == null) return Task.FromResult(0);
        
        if(ticketUpdateDto.Subject != null) ticket.Result.Subject = ticketUpdateDto.Subject;
        if(ticketUpdateDto.Description != null) ticket.Result.Description = ticketUpdateDto.Description;
        if(ticketUpdateDto.AssignedToEmployeeId != null) ticket.Result.AssignedToEmployeeId = ticketUpdateDto.AssignedToEmployeeId;
        if(ticketUpdateDto.Cc != null) ticket.Result.Cc = ticketUpdateDto.Cc;
        if(ticketUpdateDto.ClientId != null) ticket.Result.ClientId = ticketUpdateDto.ClientId;
        if(ticketUpdateDto.CreatedByEmployeeId != null) ticket.Result.CreatedByEmployeeId = ticketUpdateDto.CreatedByEmployeeId;
        if(ticketUpdateDto.Priority != null) ticket.Result.Priority = ticketUpdateDto.Priority;
        if(ticketUpdateDto.Status != null) ticket.Result.Status = ticketUpdateDto.Status;
        
        return _ticketRepo.Update(ticket.Result);
    }

    public Task<int> Delete(int id)
    {
        var ticket = _ticketRepo.GetById(id);
        if (ticket.Result != null) return _ticketRepo.Delete(ticket.Result);
        return Task.FromResult(0);
    }

    public Task<TicketReadDto>? Get(int id)
    {
        var ticket = _ticketRepo.GetById(id);
        if (ticket.Result == null) return null;
        return Task.FromResult(new TicketReadDto()
        {
            Id = ticket.Result.Id,
            Subject = ticket.Result.Subject,
            Description = ticket.Result.Description,
            AssignedToEmployeeId = ticket.Result.AssignedToEmployeeId,
            Cc = ticket.Result.Cc,
            ClientId = ticket.Result.ClientId,
            CreatedByEmployeeId = ticket.Result.CreatedByEmployeeId,
            Priority = ticket.Result.Priority,
            Status = ticket.Result.Status,
            
        });
    }

    public Task<List<TicketReadDto>> GetAll()
    {
        var ticket = _ticketRepo.GetAll();
        return Task.FromResult(ticket.Result.Select(note => new TicketReadDto()
        {
            Id = note.Id,
            Subject = note.Subject,
            Description = note.Description,
            AssignedToEmployeeId = note.AssignedToEmployeeId,
            Cc = note.Cc,
            ClientId = note.ClientId,
            CreatedByEmployeeId = note.CreatedByEmployeeId,
            Priority = note.Priority,
            Status = note.Status,
            
            
        }).ToList());
    }
    
     
     public async Task<FilteredTicketDto> GetFilteredTicketsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await _ticketRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(paginatedResults);
            FilteredTicketDto result = new()
            {
                TicketDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Ticket> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(users, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedTicket = _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(paginatedResults);

            FilteredTicketDto filteredTicketDto = new()
            {
                TicketDto = mappedTicket,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredTicketDto;
        }

        return new FilteredTicketDto();
    }
    private IEnumerable<Ticket> ApplyFilter(IEnumerable<Ticket> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var projectValue) => ApplyNumericFilter(users, column, projectValue, operatorType),
            _ => users
        };
    }

    private IEnumerable<Ticket> ApplyNumericFilter(IEnumerable<Ticket> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var projectValue) && projectValue < value),
        _ => users
    };
}


    public Task<List<TicketDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Ticket> user;
            user = _ticketRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var project = _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(user);
            return Task.FromResult(project.ToList());
        }

        var  users = _ticketRepo.GlobalSearch(searchKey);
        var projects = _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDto>>(users);
        return Task.FromResult(projects.ToList());
    }


}
