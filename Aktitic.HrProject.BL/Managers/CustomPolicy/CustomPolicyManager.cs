using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Employee;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class CustomPolicyManager:ICustomPolicyManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CustomPolicyManager( IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(CustomPolicyAddDto customPolicyAddDto)
    {
        var customPolicy = new CustomPolicy()
        {
            Name = customPolicyAddDto.Name,
            EmployeeId = customPolicyAddDto.EmployeeId,
            Days = customPolicyAddDto.Days,
            Type = customPolicyAddDto.Type,
            CreatedAt = DateTime.Now,
        };
        _unitOfWork.CustomPolicy.Add(customPolicy);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(CustomPolicyUpdateDto customPolicyUpdateDto,int id)
    {
        var customPolicy = _unitOfWork.CustomPolicy.GetById(id);
        
        if (customPolicy == null) return Task.FromResult(0);
        if(customPolicyUpdateDto.EmployeeId != null) customPolicy.EmployeeId = customPolicyUpdateDto.EmployeeId;
        if(customPolicyUpdateDto.Days != null) customPolicy.Days = customPolicyUpdateDto.Days;
        if(customPolicyUpdateDto.Name != null) customPolicy.Name = customPolicyUpdateDto.Name;
        if(customPolicyUpdateDto.Type != null) customPolicy.Type = customPolicyUpdateDto.Type;

        customPolicy.UpdatedAt = DateTime.Now;
         _unitOfWork.CustomPolicy.Update(customPolicy);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var customPolicy = _unitOfWork.CustomPolicy.GetById(id);
        if (customPolicy == null) return Task.FromResult(0);
        customPolicy.IsDeleted = true;
        customPolicy.DeletedAt = DateTime.Now;
        _unitOfWork.CustomPolicy.Update(customPolicy);
        return _unitOfWork.SaveChangesAsync();
    }

    public  CustomPolicyReadDto Get(int id)
    {
        var customPolicy =  _unitOfWork.CustomPolicy.GetWithEmployee(id);
        if (customPolicy == null) return new CustomPolicyReadDto();
        var employee =  _unitOfWork.Employee.GetById(customPolicy.EmployeeId);
        var employeeMapped = _mapper.Map<Employee,EmployeeDto>(employee!);
        return new CustomPolicyReadDto()
        {
            Id = customPolicy.Id,
            Name = customPolicy.Name,
            EmployeeId = customPolicy.EmployeeId,
            Days = customPolicy.Days,
            Type = customPolicy.Type,
            Employee = employeeMapped.FullName
        };
    }

    public List<CustomPolicyReadDto>? GetByType(string type)
    {
        var customPolicys = _unitOfWork.CustomPolicy.GetByType(type);
        if (customPolicys != null)
        {
            return customPolicys.Select(customPolicy => new CustomPolicyReadDto()
            {
                Id = customPolicy.Id,
                Name = customPolicy.Name,
                EmployeeId = customPolicy.EmployeeId,
                Days = customPolicy.Days,
                Type = customPolicy.Type,
                Employee = _unitOfWork.Employee.GetById(customPolicy.EmployeeId)?.FullName,
            }).ToList();
        }
        return null;
    }

    public async Task<List<CustomPolicyReadDto>> GetAll()
    {
        var customPolicys =await  _unitOfWork.CustomPolicy.GetAllWithEmployee();
        return customPolicys.Select(customPolicy => new CustomPolicyReadDto()
        {
            Id = customPolicy.Id,
            Name = customPolicy.Name,
            EmployeeId = customPolicy.EmployeeId,
            Days = customPolicy.Days,
            Type = customPolicy.Type,
            Employee = customPolicy.Employee?.FullName,
        }).ToList();
    }
}
