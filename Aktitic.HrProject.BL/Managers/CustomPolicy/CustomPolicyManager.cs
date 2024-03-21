
using System.Xml.Serialization;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.Repos.EmployeeRepo;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class CustomPolicyManager:ICustomPolicyManager
{
    private readonly ICustomPolicyRepo _customPolicyRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IMapper _mapper;
    public CustomPolicyManager(ICustomPolicyRepo customPolicyRepo, IEmployeeRepo employeeRepo, IMapper mapper)
    {
        _customPolicyRepo = customPolicyRepo;
        _employeeRepo = employeeRepo;
        _mapper = mapper;
    }
    
    public Task<int> Add(CustomPolicyAddDto customPolicyAddDto)
    {
        var customPolicy = new CustomPolicy()
        {
            Name = customPolicyAddDto.Name,
            EmployeeId = customPolicyAddDto.EmployeeId,
            Days = customPolicyAddDto.Days,
        };
       return _customPolicyRepo.Add(customPolicy);
    }

    public Task<int> Update(CustomPolicyUpdateDto customPolicyUpdateDto,int id)
    {
        var customPolicy = _customPolicyRepo.GetById(id);
        
        if (customPolicy.Result == null) return Task.FromResult(0);
        if(customPolicyUpdateDto.EmployeeId != null) customPolicy.Result.EmployeeId = customPolicyUpdateDto.EmployeeId;
        if(customPolicyUpdateDto.Days != null) customPolicy.Result.Days = customPolicyUpdateDto.Days;
        if(customPolicyUpdateDto.Name != null) customPolicy.Result.Name = customPolicyUpdateDto.Name;
       
        return _customPolicyRepo.Update(customPolicy.Result);
    }

    public Task<int> Delete(int id)
    {
        var customPolicy = _customPolicyRepo.GetById(id);
        if (customPolicy.Result != null) return _customPolicyRepo.Delete(customPolicy.Result);
        return Task.FromResult(0);
    }

    public async Task<CustomPolicyReadDto>? Get(int id)
    {
        var customPolicy = await _customPolicyRepo.GetById(id);
        if (customPolicy == null) return new CustomPolicyReadDto();
        var employee = await _employeeRepo.GetById(customPolicy.EmployeeId);
        var employeeMapped = _mapper.Map<Employee,EmployeeDto>(employee!);
        return new CustomPolicyReadDto()
        {
            Id = customPolicy.Id,
            Name = customPolicy.Name,
            EmployeeId = customPolicy.EmployeeId,
            Days = customPolicy.Days,
            Employee = employeeMapped
        };
    }

    public async Task<List<CustomPolicyReadDto>> GetAll()
    {
        var customPolicys = await _customPolicyRepo.GetAll();
        return customPolicys.Select(customPolicy => new CustomPolicyReadDto()
        {
            Id = customPolicy.Id,
            Name = customPolicy.Name,
            EmployeeId = customPolicy.EmployeeId,
            Days = customPolicy.Days,
            
        }).ToList();
    }
}
