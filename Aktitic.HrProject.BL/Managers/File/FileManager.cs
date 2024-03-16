
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

    public void Update(FileUpdateDto fileUpdateDto,int id)
    {
        var file = _fileRepo.GetById(id);
        
        if (file.Result == null) return;
        if(fileUpdateDto.Name != null) file.Result.Name = fileUpdateDto.Name;
        if(fileUpdateDto.Content != null) file.Result.Content = fileUpdateDto.Content;
        if(fileUpdateDto.Extension != null) file.Result.Extension = fileUpdateDto.Extension;
        
        
        _fileRepo.Update(file.Result);
    }

    public void Delete(int id)
    {
        var file = _fileRepo.GetById(id);
        if (file.Result != null) _fileRepo.Delete(file.Result);
    }

    public FileReadDto? Get(int id)
    {
        var file = _fileRepo.GetById(id);
        if (file.Result == null) return null;
        return new FileReadDto()
        {
            Id = file.Result.Id,
            Name = file.Result.Name,
            Content = file.Result.Content,
            Extension = file.Result.Extension,
            
        };
    }

    // public FileReadDto? GetByEmployeeId(int id)
    // {
    //     var file = _fileRepo.GetByEmployeeId(id);
    //     if (file.Result == null) return null;
    //     return new FileReadDto()
    //     {
    //         Name = file.Result.Name,
    //         Content = file.Result.Content,
    //         Extension = file.Result.Extension,
    //     };
    // }

    public List<FileReadDto> GetAll()
    {
        var files = _fileRepo.GetAll();
        return files.Result.Select(file => new FileReadDto()
        {
            Id = file.Id,
            Name = file.Name,
            Content = file.Content,
            Extension = file.Extension,
           
        }).ToList();
    }
}
