
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos.AttendanceRepo;

namespace Aktitic.HrProject.BL;

public class DesignationManager:IDesignationManager
{
    private readonly IDesignationRepo _designationRepo;

    public DesignationManager(IDesignationRepo designationRepo)
    {
        _designationRepo = designationRepo;
    }
    
    public void Add(DesignationAddDto designationAddDto)
    {
        var designation = new Designation()
        {
            Name = designationAddDto.Name,
            DepartmentId = designationAddDto.DepartmentId
        };
        _designationRepo.Add(designation);
    }

    public void Update(DesignationUpdateDto designationUpdateDto)
    {
        var designation = _designationRepo.GetById(designationUpdateDto.Id);
        
        if (designation.Result == null) return;
        designation.Result.Name = designationUpdateDto.Name;
        designation.Result.DepartmentId = designationUpdateDto.DepartmentId;

        _designationRepo.Update(designation.Result);
    }

    public void Delete(DesignationDeleteDto designationDeleteDto)
    {
        var designation = _designationRepo.GetById(designationDeleteDto.Id);
        if (designation.Result != null) _designationRepo.Delete(designation.Result);
    }

    public DesignationReadDto? Get(int id)
    {
        var designation = _designationRepo.GetById(id);
        if (designation.Result == null) return null;
        return new DesignationReadDto()
        {
            Id = designation.Result.Id,
            Name = designation.Result.Name,
            DepartmentId = designation.Result.DepartmentId,
        };
    }

    public List<DesignationReadDto> GetAll()
    {
        var designations = _designationRepo.GetAll();
        return designations.Result.Select(designation => new DesignationReadDto()
        {
            Id = designation.Id,
            Name = designation.Name,
            DepartmentId = designation.DepartmentId
            
        }).ToList();
    }
}
