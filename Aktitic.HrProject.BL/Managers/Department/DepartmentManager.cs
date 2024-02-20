
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;

namespace Aktitic.HrProject.BL;

public class DepartmentManager:IDepartmentManager
{
    private readonly IDepartmentRepo _departmentRepo;

    public DepartmentManager(IDepartmentRepo departmentRepo)
    {
        _departmentRepo = departmentRepo;
    }
    
    public void Add(DepartmentAddDto departmentAddDto)
    {
        var department = new Department()
        {
            Name = departmentAddDto.Name,
        };
        _departmentRepo.Add(department);
    }

    public void Update(DepartmentUpdateDto departmentUpdateDto)
    {
        var department = _departmentRepo.GetById(departmentUpdateDto.Id);
        
        if (department.Result == null) return;
        department.Result.Name = departmentUpdateDto.Name;


        _departmentRepo.Update(department.Result);
    }

    public void Delete(DepartmentDeleteDto departmentDeleteDto)
    {
        var department = _departmentRepo.GetById(departmentDeleteDto.Id);
        if (department.Result != null) _departmentRepo.Delete(department.Result);
    }

    public DepartmentReadDto? Get(int id)
    {
        var department = _departmentRepo.GetById(id);
        if (department.Result == null) return null;
        return new DepartmentReadDto()
        {
            Name = department.Result.Name,
        };
    }

    public List<DepartmentReadDto> GetAll()
    {
        var departments = _departmentRepo.GetAll();
        return departments.Result.Select(department => new DepartmentReadDto()
        {
            Name = department.Name,
            
        }).ToList();
    }
}
