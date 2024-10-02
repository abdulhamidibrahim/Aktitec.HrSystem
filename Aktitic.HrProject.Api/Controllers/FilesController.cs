using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController(
    IRevisorManager revisorManager,
    IDocumentManager documentManager,
    IDocumentFileManager documentFileManager)
    : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<DocumentReadDto>>> GetAll()
    {
        return await documentManager.GetAll();
    }
    
    [HttpGet("getByProjectId/{id:int}")]
    public async Task<ActionResult<List<DocumentReadDto>>> Get(int id)
    {
        var result = await documentManager.GetByProjectId(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DocumentReadDto>> GetById(int id)
    {
        var result = await documentManager.GetById(id);
        if (result == null) return NotFound("Not Found!");
        return result;
    }
    [HttpGet("getFile/{id:int}")]
    public ActionResult GetFile(int id)
    {
        var result = documentFileManager.Get(id);
        if (result == null) return NotFound("Not Found!");
        return Ok(result);
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> Add( [FromForm] DocumentAddDto documentAddDto)
    {
        var result = await documentManager.Add(documentAddDto);
        if (result.Success == false) return BadRequest(result.Data);
        return Ok(result.Data);
    }
    
    [HttpPost("addFiles/{documentId}")]
    public async Task<ActionResult> AddFiles([FromForm] DocumentFileAddDto documentFileAddDto, int documentId)
    {
        var result = await documentFileManager.Add(documentFileAddDto,documentId);
        // if (result == 0) return BadRequest("Failed to add");
        return Ok("Added Successfully!");
    }
   
    
    [HttpPut("updateFile/{fileId}")]    
    public ActionResult UpdateFile([FromForm] DocumentFileUpdateDto documentFileUpdateDto,int fileId)
    {
        var result = documentFileManager.Update(documentFileUpdateDto,fileId);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] DocumentUpdateDto documentUpdateDto,int id)
    {
        var result  =documentManager.Update(documentUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated Successfully!");
    }
    
    [HttpDelete("deleteFile/{id}")]
    public async Task<ActionResult> DeleteFile(int id)
    {
        var result = await documentFileManager.Delete(id);
        if (result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }

    [HttpGet("getDocumentFiles/{documentId}")]
    public async Task<ActionResult> GetDocumentFiles(int documentId,int page, int pageSize)
    {
        var result = await documentFileManager.GetDocumentFiles(documentId,page,pageSize);
        // if (result == null) return BadRequest("Failed to get files");
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result = documentManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<DocumentReadDto>> GlobalSearch(string search,string? column)
    {
        return await documentManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredFiles")]
    public Task<FilteredDocumentsDto> GetFilteredFilesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return documentManager.GetFilteredFilesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    // download file by id
    [HttpGet("download/{fileId}")]
    public IActionResult DownloadFile(int fileId)
    {
        // Retrieve the file metadata from the database
        var file = documentFileManager.DownloadFile(fileId); 
        if (file == null) return NotFound("Files Not Found!"); 
        var fullPath = file.Path;
        if(!System.IO.File.Exists(fullPath)) return NotFound("File Not Found!");
        var fileBytes = System.IO.File.ReadAllBytes(fullPath);
        return File(fileBytes, MimeTypes.GetMimeType(fullPath), file.Name);
    }
    
    [HttpGet("getFiles")]
    public IActionResult GetFiles(string? type, string? status,int page, int pageSize)
    {
        var files =  documentManager.GetFiles(type,status,page,pageSize);
        // if (files == null) return NoContent(); 
        return Ok(files);
    }

    [HttpGet("AddRevision")]
    public ActionResult AddRevision(RevisorAddDto revisorAddDto)
    {
        var result = revisorManager.Add(revisorAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Added Successfully!");
    }
    
    [HttpPut("LogNote/{employeeId}/{documentId}")]
    public ActionResult UpdateRevision(RevisorUpdateDto revisorUpdateDto,int employeeId,int documentId)
    {
        var result = revisorManager.LogNote(revisorUpdateDto,employeeId,documentId);
        if (result.Result == 0) return BadRequest("Failed to LogNote");
        return Ok("Note Logged Successfully!");
    }
    
    [HttpPut("ConfirmRevision/{employeeId}/{documentId}")]
    public ActionResult ConfirmRevision(int employeeId,int documentId)
    {
        var result = revisorManager.ConfirmRevision(employeeId,documentId);
        if (result.Result == 0) return BadRequest("Failed to Confirm Revision");
        return Ok("Confirmed Successfully!");
    }
    
    [HttpDelete("deleteRevision/{id}")]
    public async Task<ActionResult> DeleteRevision(int id)
    {
        var result = await revisorManager.Delete(id);
        if (result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }    
}