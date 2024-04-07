
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using Aktitic.HrProject.DAL.UnitOfWork;
using Aktitic.HrTicket.BL;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTicket.BL;

public class TicketManager:ITicketManager
{
    private readonly ITicketRepo _ticketRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TicketManager(ITicketRepo ticketRepo, IMapper mapper, IUnitOfWork unitOfWork, IEmployeeRepo employeeRepo)
    {
        _ticketRepo = ticketRepo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _employeeRepo = employeeRepo;
    }
    
    public Task<int> Add(TicketAddDto ticketAddDto)
    {
        var ticket = new Ticket()
        {
           Subject = ticketAddDto.Subject,
           Description = ticketAddDto.Description,
           AssignedToEmployeeId = ticketAddDto.AssignedToEmployeeId,
           Cc = ticketAddDto.Cc,
           Date = ticketAddDto.Date,
           ClientId = ticketAddDto.ClientId,
           CreatedByEmployeeId = ticketAddDto.CreatedByEmployeeId,
           Priority = ticketAddDto.Priority,
           Status = ticketAddDto.Status,
           TicketId = ticketAddDto.TicketId,
           Followers = ticketAddDto.Followers,
           LastReply = ticketAddDto.LastReply,
        };
         _ticketRepo.Add(ticket);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(TicketUpdateDto ticketUpdateDto, int id)
    {
        var ticket = _ticketRepo.GetById(id);

        if (ticket == null) return Task.FromResult(0);
        
        if(ticketUpdateDto.Subject != null) ticket.Subject = ticketUpdateDto.Subject;
        if(ticketUpdateDto.Description != null) ticket.Description = ticketUpdateDto.Description;
        if(ticketUpdateDto.AssignedToEmployeeId != null) ticket.AssignedToEmployeeId = ticketUpdateDto.AssignedToEmployeeId;
        if(ticketUpdateDto.Cc != null) ticket.Cc = ticketUpdateDto.Cc;
        if(ticketUpdateDto.ClientId != null) ticket.ClientId = ticketUpdateDto.ClientId;
        if(ticketUpdateDto.CreatedByEmployeeId != null) ticket.CreatedByEmployeeId = ticketUpdateDto.CreatedByEmployeeId;
        if(ticketUpdateDto.Priority != null) ticket.Priority = ticketUpdateDto.Priority;
        if(ticketUpdateDto.Status != null) ticket.Status = ticketUpdateDto.Status;
        if(ticketUpdateDto.Date != null) ticket.Date = ticketUpdateDto.Date;
        if(ticketUpdateDto.TicketId != null) ticket.TicketId = ticketUpdateDto.TicketId;
        if(ticketUpdateDto.Followers != null) ticket.Followers = ticketUpdateDto.Followers;
        if(ticketUpdateDto.LastReply != null) ticket.LastReply = ticketUpdateDto.LastReply;
        
        _ticketRepo.Update(ticket);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {

        _ticketRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public TicketReadDto Get(int id)
    {
        var ticket = _ticketRepo.GetTicketsWithEmployeesAsync(id);
        List<EmployeeDto> employees = new();

        if (ticket.Result.Followers != null)
            foreach (var employee in ticket.Result.Followers)
            {
                var emp = _employeeRepo.GetById(employee);
                if(emp!=null) employees.Add(_mapper.Map<Employee, EmployeeDto>(emp));
            }

        if (ticket.Result != null)
            return new TicketReadDto()
            {
                Id = ticket.Result.Id,
                Subject = ticket.Result.Subject,
                Description = ticket.Result.Description,
                AssignedToEmployeeId = ticket.Result.AssignedToEmployeeId,
                Cc = ticket.Result.Cc,
                Date = ticket.Result.Date,
                ClientId = ticket.Result.ClientId,
                CreatedByEmployeeId = ticket.Result.CreatedByEmployeeId,
                Priority = ticket.Result.Priority,
                Status = ticket.Result.Status,
                TicketId = ticket.Result.TicketId,
                Followers = employees,
                LastReply = ticket.Result.LastReply,
                AssignedTo = _mapper.Map<Employee, EmployeeDto>(ticket.Result.AssignedTo ?? new Employee()),
                CreatedBy = _mapper.Map<Employee, EmployeeDto>(ticket.Result.CreatedBy ?? new Employee()),
            };
        return new TicketReadDto();
    }

    public Task<List<TicketReadDto>> GetAll()
    {
        var ticket = _ticketRepo.GetAll();
        return Task.FromResult(ticket.Result.Select(t => new TicketReadDto()
        {
            Id = t.Id,
            Subject = t.Subject,
            Description = t.Description,
            AssignedToEmployeeId = t.AssignedToEmployeeId,
            Cc = t.Cc,
            ClientId = t.ClientId,
            CreatedByEmployeeId = t.CreatedByEmployeeId,
            Priority = t.Priority,
            Status = t.Status,
            Date = t.Date,
            
        }).ToList());
    }
    
     
     public async Task<FilteredTicketDto> GetFilteredTicketsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var ticketsWithEmployeesAsync =  _ticketRepo.GetTicketsWithEmployeesAsync();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = ticketsWithEmployeesAsync.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = ticketsWithEmployeesAsync.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            List<TicketDto> ticketDto = new();
            foreach (var ticket in paginatedResults)
            {
                var assignedTo = _mapper.Map<Employee, EmployeeDto>(ticket.AssignedTo ?? new Employee());
                var createdBy = _mapper.Map<Employee, EmployeeDto>(ticket.CreatedBy ?? new Employee());
                ticketDto.Add(new TicketDto()
                {
                    Id = ticket.Id,
                    Subject = ticket.Subject,
                    Description = ticket.Description,
                    AssignedToEmployeeId = ticket.AssignedToEmployeeId,
                    Cc = ticket.Cc,
                    ClientId = ticket.ClientId,
                    CreatedByEmployeeId = ticket.CreatedByEmployeeId,
                    Priority = ticket.Priority,
                    Status = ticket.Status,
                    Date = ticket.Date,
                    TicketId = ticket.TicketId,
                    Followers = ticket.Followers,
                    LastReply = ticket.LastReply,
                    AssignedTo = assignedTo.FullName,
                    CreatedBy = createdBy.FullName,
                   
                });
            }

            return new FilteredTicketDto()
            {
                TicketDto = ticketDto,
                TotalCount = count,
                TotalPages = pages,
            };
        }

        if (ticketsWithEmployeesAsync != null)
        {
            IEnumerable<Ticket> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(ticketsWithEmployeesAsync, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(ticketsWithEmployeesAsync, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);
            List<TicketDto> ticketDto = new();
            foreach (var ticket in paginatedResults)
            {
                var assignedTo = _mapper.Map<Employee, EmployeeDto>(ticket.AssignedTo ?? new Employee());
                var createdBy = _mapper.Map<Employee, EmployeeDto>(ticket.CreatedBy ?? new Employee());
                ticketDto.Add(new TicketDto()
                {
                    Id = ticket.Id,
                    Subject = ticket.Subject,
                    Description = ticket.Description,
                    AssignedToEmployeeId = ticket.AssignedToEmployeeId,
                    Cc = ticket.Cc,
                    ClientId = ticket.ClientId,
                    CreatedByEmployeeId = ticket.CreatedByEmployeeId,
                    Priority = ticket.Priority,
                    Status = ticket.Status,
                    Date = ticket.Date,
                    TicketId = ticket.TicketId,
                    Followers = ticket.Followers,
                    LastReply = ticket.LastReply,
                    AssignedTo = assignedTo.FullName,
                    CreatedBy = createdBy.FullName,
                   
                });
            }

            return new FilteredTicketDto()
            {
                TicketDto = ticketDto,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
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
