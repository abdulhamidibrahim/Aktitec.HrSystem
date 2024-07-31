using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController :ControllerBase
{
    private readonly IFileManager _fileManager;

    public FilesController(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<FileReadDto>>> GetAll()
    {
        return await _fileManager.GetAll();
    }
    
    [HttpGet("getByProjectId/{id:int}")]
    public async Task<ActionResult<List<FileReadDto>>> Get(int id)
    {
        var result = await _fileManager.GetByProjectId(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    
    [HttpPost("create")]
    public ActionResult Add( FileAddDto fileAddDto)
    {
        var result = _fileManager.Add(fileAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Added Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update( FileUpdateDto fileUpdateDto,int id)
    {
        var result  =_fileManager.Update(fileUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = _fileManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<FileReadDto>> GlobalSearch(string search,string? column)
    {
        return await _fileManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredFiles")]
    public Task<FilteredFilesDto> GetFilteredFilesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _fileManager.GetFilteredFilesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
}