
using Aktitic.HrProject.BL.Dtos.Employee;
using Aktitic.HrProject.BL.Helpers;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class ClientManager(
    IClientRepo clientRepo,
    IWebHostEnvironment webHostEnvironment,
    IFileRepo fileRepo,
    IMapper mapper)
    : IClientManager
{
    private readonly IFileRepo _fileRepo = fileRepo;
    // private readonly UserManager<Client> _userManager;

    // _userManager = userManager;

    public Task<int> Add(ClientAddDto clientAddDto, IFormFile? image)
    {
        var client = new Client()
        {
            Phone = clientAddDto.Phone,
            Email = clientAddDto.Email,
            FirstName = clientAddDto.FirstName,
            FileName = image?.FileName,
            FileExtension = image?.ContentType,
            CompanyName = clientAddDto.CompanyName,
            LastName = clientAddDto.LastName,
            
            UserName = clientAddDto.Email?.Substring(0,clientAddDto.Email.IndexOf('@'))
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
        
        var imgPath = Path.Combine(path, client.FileName!);
        using FileStream fileStream = new(imgPath, FileMode.Create);
        image?.CopyToAsync(fileStream);
        client.ImgUrl = Path.Combine("uploads/clients", client.UserName + unique, client.FileName!);
        // _fileRepo.Add(file);
        

       
        
        return  clientRepo.Add(client);
        
         
    }
    

    public async Task<int> Update(ClientUpdateDto clientUpdateDto,int id, IFormFile? image)
    {
        var client = clientRepo.GetById(id);
        
        if (client.Result == null) return 0;
        if(clientUpdateDto.FirstName != null) client.Result.FirstName = clientUpdateDto.FirstName;
        if(clientUpdateDto.Phone != null) client.Result.Phone = clientUpdateDto.Phone;
        if (clientUpdateDto.Email != null) client.Result.Email = clientUpdateDto.Email;
        if (clientUpdateDto.CompanyName != null) client.Result.CompanyName = clientUpdateDto.CompanyName;
        if (clientUpdateDto.ImgUrl != null) client.Result.ImgUrl = clientUpdateDto.ImgUrl;
      
        // client.Result.FileContent = clientUpdateDto.Image.ContentType;
        if (image != null)
        {
            client.Result.FileExtension = image?.ContentType;
            client.Result.FileName = image?.FileName;
            client.Result.UserName = client.Result.Email?.Substring(0, client.Result.Email.IndexOf('@'));

            var unique = Guid.NewGuid();
            var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads/clients", client.Result.UserName+unique);
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            
           
            var imgPath = Path.Combine(path, client.Result.FileName!);
            await using FileStream fileStream = new(imgPath, FileMode.Create);
            image?.CopyToAsync(fileStream);
            client.Result.ImgUrl = Path.Combine("uploads/clients", client.Result.UserName+unique, client.Result.FileName!);
        }

        return await clientRepo.Update(client.Result);
    }

    public async Task<int> Delete(int id)
    {
        var client = clientRepo.GetById(id);
        if (client.Result != null) return await clientRepo.Delete(client.Result.Id);
        return 0;
    }

    public ClientReadDto? Get(int id)
    {
        var client = clientRepo.GetById(id);
        if (client.Result == null) return null;
        return new ClientReadDto()
        {
            Id = client.Id,
            FirstName = client.Result.FirstName,
            LastName = client.Result.LastName,
            Phone = client.Result.Phone,
            Email = client.Result.Email,
            CompanyName = client.Result.CompanyName,
            ImgUrl = client.Result.ImgUrl
        };
    }

    public Task<List<ClientReadDto>> GetAll()
    {
        var clients = clientRepo.GetAll();
        
        return  Task.FromResult(clients.Result.Select(client => new ClientReadDto()
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Phone = client.Phone,
            Email = client.Email,
            CompanyName = client.CompanyName,
            ImgUrl = client.ImgUrl
            
        }).ToList());
    }

    public Task<PagedClientResult> GetClientsAsync(string? term, string? sort, int page, int limit)
    {
        var clients = clientRepo.GetClientsAsync(term, sort, page, limit);
        return clients;

        #region commented

        // Task.FromResult(clients.Result.Clients);
        // .Clients.Select(client => new PagedClientResult()
        // {
        //     Id = client.Id,
        //     FullName = client.FullName,
        //     Phone = client.Phone,
        //     Email = client.Email,
        //     Age = client.Age,
        //     JobPosition = client.JobPosition,
        //     JoiningDate = client.JoiningDate,
        //     YearsOfExperience = client.YearsOfExperience,
        //     Salary = client.Salary,
        //     DepartmentId = client.DepartmentId,
        //     ManagerId = client.ManagerId,
        //     ImgUrl = client.ImgUrl
        // }));

        #endregion
    }

   

    public async Task<FilteredClientDto> GetFilteredClientsAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
    {
        var users = await clientRepo.GetAll();
        

        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = users.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var userList = users.ToList();

            var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);

            var map = mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(paginatedResults);
            FilteredClientDto result = new()
            {
                ClientDto = map,
                TotalCount = count,
                TotalPages = pages
            };
            return result;
        }

        if (users != null)
        {
            IEnumerable<Client> filteredResults;
        
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

            var mappedClient = mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(paginatedResults);

            FilteredClientDto filteredClientDto = new()
            {
                ClientDto = mappedClient,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredClientDto;
        }

        return new FilteredClientDto();
    }
    private IEnumerable<Client> ApplyFilter(IEnumerable<Client> users, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var clientValue) => ApplyNumericFilter(users, column, clientValue, operatorType),
            _ => users
        };
    }

private IEnumerable<Client> ApplyNumericFilter(IEnumerable<Client> users, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue == value),
        "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue != value),
        "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue >= value),
        "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue > value),
        "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue <= value),
        "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var clientValue) && clientValue < value),
        _ => users
    };
}


    public Task<List<ClientDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Client> user;
            user = clientRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var client = mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(user);
            return Task.FromResult(client.ToList());
        }

        var  users = clientRepo.GlobalSearch(searchKey);
        var clients = mapper.Map<IEnumerable<Client>, IEnumerable<ClientDto>>(users);
        return Task.FromResult(clients.ToList());
    }
    
    
    public bool IsEmailUnique(string email)
    {
        var client = clientRepo.GetByEmail(email);
        return client.Result == null;
    }
    
}

