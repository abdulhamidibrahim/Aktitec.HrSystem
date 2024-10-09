using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ClientManager(
    IWebHostEnvironment webHostEnvironment,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IClientManager
{

    // private readonly UserManager<Client> _ClientManager;

    // _ClientManager = ClientManager;

    public Task<int> Add(ClientAddDto clientAddDto)
    {
       // var permissions = JsonConvert.DeserializeObject<List<PermissionsDto>>(clientAddDto.Permissions!);
       // var mappedPermissions = mapper.Map<List<PermissionsDto>, List<Permission>>(permissions);
       // mappedPermissions.Select(x => x.CreatedAt = DateTime.Now);
       
        var client = new Client()
        {
            Phone = clientAddDto.Mobile,
            Email = clientAddDto.Email,
            FirstName = clientAddDto.FirstName,
            FileName = clientAddDto.Image?.FileName,
            FileExtension = clientAddDto.Image?.ContentType,
            CompanyName = clientAddDto.CompanyName,
            LastName = clientAddDto.LastName,
            Status = clientAddDto.Status,
            ClientId = clientAddDto.ClientId,
            // Permissions = mappedPermissions,
            Password = clientAddDto.Password,
            ConfirmPassword = clientAddDto.ConfirmPassword,
            UserName = clientAddDto.UserName,
            CreatedAt = DateTime.Now
            // UserName = clientAddDto.Email?.Substring(0, clientAddDto.Email.IndexOf('@'))
        };
        

        var unique = Guid.NewGuid();

        var path = Path.Combine(webHostEnvironment.WebRootPath,"uploads/clients", client.UserName+unique);
        if (Directory.Exists(path))
        {
            Directory.Delete(path,true);
        }else
        {
            Directory.CreateDirectory(path);
        }
        
        var imgPath = Path.Combine(path, clientAddDto.Image?.FileName!);
        using FileStream fileStream = new(imgPath, FileMode.Create);
        clientAddDto.Image?.CopyToAsync(fileStream);
        client.PhotoUrl = Path.Combine("uploads/clients", client.UserName + unique, clientAddDto.Image?.FileName!);
        // _fileRepo.Add(file);
        

       
        
          unitOfWork.Client.Add(client);
          return unitOfWork.SaveChangesAsync();
    }
    

    public async Task<int> Update(ClientUpdateDto clientUpdateDto, int id)
    {
        var client = unitOfWork.Client.GetById(id);
        if (client == null)
            return 0;

        // Deserialize permissions
        // var permissions = JsonConvert.DeserializeObject<List<PermissionsDto>>(clientUpdateDto.Permissions!);

        // LogNote client properties
        client.FirstName = clientUpdateDto.FirstName;
        client.Phone = clientUpdateDto.Mobile;
        if (clientUpdateDto.Email != null)
            client.Email = clientUpdateDto.Email;
        client.LastName = clientUpdateDto.LastName;
        if (clientUpdateDto.Status != null)
            client.Status = clientUpdateDto.Status;
        if (clientUpdateDto.ClientId != null)
            client.ClientId = clientUpdateDto.ClientId;
        if (clientUpdateDto.CompanyName != null)
            client.CompanyName = clientUpdateDto.CompanyName;
        if (clientUpdateDto.ImgUrl != null)
            client.PhotoUrl = clientUpdateDto.ImgUrl;
        if (clientUpdateDto.Password != null)
            client.Password = clientUpdateDto.Password;
        if (clientUpdateDto.UserName != null)
            client.UserName = clientUpdateDto.UserName;
        
         // LogNote image
    if (clientUpdateDto?.Image != null)
    {
        client.FileExtension = clientUpdateDto.Image.ContentType;
        client.FileName = clientUpdateDto.Image.FileName;

        var unique = Guid.NewGuid();
        var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/clients", client.UserName + unique);
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        else
        {
            Directory.CreateDirectory(path);
        }

        var imgPath = Path.Combine(path, client.FileName);
        await using FileStream fileStream = new(imgPath, FileMode.Create);
        await clientUpdateDto.Image.CopyToAsync(fileStream);
        client.PhotoUrl = Path.Combine("uploads/clients", client.UserName + unique, client.FileName);
    }
    
    client.UpdatedAt = DateTime.Now;
    
        unitOfWork.Client.Update(client);
         return await unitOfWork.SaveChangesAsync();
    }


    public Task<int> Delete(int id)
    {
        var client = unitOfWork.Client.GetById(id);
        if (client == null) return Task.FromResult(0);
        client.IsDeleted = true;
        client.DeletedAt=DateTime.Now;
        unitOfWork.Client.Update(client);
        return unitOfWork.SaveChangesAsync();
    }

    public ClientReadDto? Get(int id)
    {
        var client = unitOfWork.Client.GetById(id);
        var permissions = unitOfWork.Permission.GetByClientId(id);
        var mappedPermissions = mapper.Map<List<Permission>, List<PermissionsDto>>(permissions);
        if (client == null) return null;
        return new ClientReadDto()
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Mobile = client.Phone,
            Email = client.Email,
            CompanyName = client.CompanyName,
            PhotoUrl = client.PhotoUrl,
            Status = client.Status,
            UserName = client.UserName,
            ClientId = client.ClientId,
            Permissions = mappedPermissions,
            Password = client.Password,
            ConfirmPassword = client.ConfirmPassword
        };
    }

    public Task<List<ClientReadDto>> GetAll()
    {
        var clients = unitOfWork.Client.GetAll();
        
        return  Task.FromResult(clients.Result.Select(client => new ClientReadDto()
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Mobile = client.Phone,
            Email = client.Email,
            CompanyName = client.CompanyName,
            PhotoUrl = client.PhotoUrl,
            Status = client.Status,
            UserName = client.UserName,
            ClientId = client.ClientId,
            // Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(client.Permissions.ToList())
        }).ToList());
    }

    public Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit)
    {
        var clients = unitOfWork.Client.GetClientsAsync(term, sort, page, limit);
        return clients;
    }

   

    public async Task<FilteredClientDto> GetFilteredClientsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var Clients = await unitOfWork.Client.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = Clients.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var ClientList = Clients.ToList();

            var paginatedResults = ClientList.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedClients = new List<ClientDto>();

            foreach (var client in paginatedResults)
            {
                mappedClients.Add(new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    UserName = client.UserName,
                    Email = client.Email,
                    Phone = client.Phone,
                    CompanyName = client.CompanyName,
                    Status = client.Status,
                    photoUrl = client.PhotoUrl,
                    ClientId = client.ClientId,
                    // Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(client.Permissions.ToList())
                    
                });
            }
            
            FilteredClientDto result = new()
            {
                ClientDto = mappedClients,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (Clients != null)
        {
            IEnumerable<Client> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(Clients, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(Clients, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            var mappedClients = new List<ClientDto>();

            foreach (var client in paginatedResults)
            {
                mappedClients.Add(new ClientDto()
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    UserName = client.UserName,
                    Email = client.Email,
                    Phone = client.Phone,
                    CompanyName = client.CompanyName,
                    Status = client.Status,
                    photoUrl = client.PhotoUrl,
                    ClientId = client.ClientId,
                    Permissions = mapper.Map<List<Permission>, List<PermissionsDto>>(client.Permissions.ToList())
                    
                });
            }
            
            FilteredClientDto result = new()
            {
                ClientDto = mappedClients,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return result;
        }

        return new FilteredClientDto();
    }
    private IEnumerable<Client> ApplyFilter(IEnumerable<Client> Clients, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => Clients.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => Clients.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => Clients.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => Clients.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var clientValue) => ApplyNumericFilter(Clients, column, clientValue, operatorType),
            _ => Clients
        };
    }

private IEnumerable<Client> ApplyNumericFilter(IEnumerable<Client> Clients, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => Clients.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue == value),
        "neq" => Clients.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue != value),
        "gte" => Clients.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue >= value),
        "gt" => Clients.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue > value),
        "lte" => Clients.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue <= value),
        "lt" => Clients.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue < value),
        _ => Clients
    };
}


    public Task<List<ClientDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Client> Client;
            Client = unitOfWork.Client.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var client = mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(Client);
            return Task.FromResult(client.ToList());
        }

        var  Clients = unitOfWork.Client.GlobalSearch(searchKey);
        var clients = mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(Clients);
        return Task.FromResult(clients.ToList());
    }
    
    
    public bool IsEmailUnique(string email)
    {
        var client = unitOfWork.Client.GetByEmail(email);
        return client.Result == null;
    }
    
}

