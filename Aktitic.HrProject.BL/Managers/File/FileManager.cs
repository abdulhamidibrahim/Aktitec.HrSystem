
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using File = Aktitic.HrProject.DAL.Models.File;

namespace Aktitic.HrProject.BL;

public class FileManager:IFileManager
{
    private readonly IFileRepo _fileRepo;

    public FileManager(IFileRepo fileRepo)
    {
        _fileRepo = fileRepo;
    }
    
    public void Add(FileAddDto fileAddDto)
    {
        var file = new File()
        {
            Name = fileAddDto.Name,
            Content = fileAddDto.Content,
            Extension = fileAddDto.Extension,
        };
        _fileRepo.Add(file);
    }

    public void Update(FileUpdateDto fileUpdateDto)
    {
        var file = _fileRepo.GetById(fileUpdateDto.Id);
        
        if (file.Result == null) return;
        file.Result.Name = fileUpdateDto.Name;
        file.Result.Content = fileUpdateDto.Content;
        file.Result.Extension = fileUpdateDto.Extension;
        file.Result.EmployeeId = fileUpdateDto.EmployeeId;
        
        _fileRepo.Update(file.Result);
    }

    public void Delete(FileDeleteDto fileDeleteDto)
    {
        var file = _fileRepo.GetById(fileDeleteDto.Id);
        if (file.Result != null) _fileRepo.Delete(file.Result);
    }

    public FileReadDto? Get(int id)
    {
        var file = _fileRepo.GetById(id);
        if (file.Result == null) return null;
        return new FileReadDto()
        {
            Name = file.Result.Name,
            Content = file.Result.Content,
            Extension = file.Result.Extension,
            EmployeeId = file.Result.EmployeeId
        };
    }

    public FileReadDto? GetByEmployeeId(int id)
    {
        var file = _fileRepo.GetByEmployeeId(id);
        if (file.Result == null) return null;
        return new FileReadDto()
        {
            Name = file.Result.Name,
            Content = file.Result.Content,
            Extension = file.Result.Extension,
            EmployeeId = file.Result.EmployeeId,
            EmployeeName = file.Result.EmployeeName
        };
    }

    public List<FileReadDto> GetAll()
    {
        var files = _fileRepo.GetAll();
        return files.Result.Select(file => new FileReadDto()
        {
            Name = file.Name,
            Content = file.Content,
            Extension = file.Extension,
            EmployeeId = file.EmployeeId,
        }).ToList();
    }
}
