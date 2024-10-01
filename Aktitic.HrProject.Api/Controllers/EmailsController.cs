using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailsController(IEmailsManager emailsManager,IWebHostEnvironment webHostEnvironment) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<EmailsReadDto>>> GetAll(string email)
    {
        var emails = await emailsManager.GetAll(email);
        return Ok(emails);
    }
    
    [HttpGet("{id}")]
    public ActionResult<EmailsReadDto?> Get(int id)
    {
        var emails = emailsManager.Get(id);
        if (emails == null) return NotFound("Email not found");
        return Ok(emails);
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] EmailsAddDto emailsAddDto)
    {
        var result =emailsManager.Add(emailsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Email added successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] EmailsUpdateDto emailsUpdateDto,int id)
    {
        var result =emailsManager.Update(emailsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Email updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =emailsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<EmailsDto>> GlobalSearch(string search,string? column,string email)
    {
        return await emailsManager.GlobalSearch(search,column,email);
    }
    
    [HttpGet("getFilteredEmails")]
    public Task<FilteredEmailsDto> GetFilteredEmailsAsync
        (string? column, string? value1,string? operator1,[Optional] string? value2,
            string? operator2, int page, int pageSize,string email)
    {
        
        return emailsManager.GetFilteredEmailsAsync(column, value1, operator1, value2, operator2, page, pageSize, email);
    }
    
    
    [HttpGet("getStarredEmails")]
    public async Task<IEnumerable<EmailsDto>> GetStarredEmails(int? page, int? pageSize,string email)
    {
        return await emailsManager.GetStarredEmails(page, pageSize,email);
    }
    
    [HttpGet("getTrashedEmails")]
    public async Task<IEnumerable<EmailsDto>> GetTrashedEmails(int? page, int? pageSize, string email)
    {
        return await emailsManager.GetTrashedEmails(page, pageSize, email);
    }
    [HttpGet("getDraftedEmails")]
    public async Task<IEnumerable<EmailsDto>> GetUnreadEmails(int? page, int? pageSize,string email)
    {
        return await emailsManager.GetDraftEmails(page, pageSize, email);
    }
    
    [HttpGet("getArchivedEmails")]
    public async Task<IEnumerable<EmailsDto>> GetArchivedEmails(int? page, int? pageSize,string email)
    {
        return await emailsManager.GetArchivedEmails(page, pageSize, email);
    }
    
    [HttpGet("getSentEmails/{id}")]
    public async Task<IEnumerable<EmailsReadDto>> GetSentEmails(int id)
    {
        return await emailsManager.GetSentEmails(id);
    }
    [HttpGet("api/attachments/download/{id}")]
    public async Task<ActionResult<IEnumerable<EmailsReadDto>>> GetAttachments(int id)
    {
        var file = await emailsManager.GetAttachments(id);
        if (file == null) return NotFound("Documents Not Found!");

        // Construct the full file path
        var fullPath = Path.Combine(webHostEnvironment.WebRootPath, file.Path);

        // Check if the file exists on the server
        if (!System.IO.File.Exists(fullPath))
        {
            return NotFound("Documents Not Found on the Server!");
        }

        // Read the file into a byte array
        var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);

        // Return the file for download
        return File(fileBytes, MimeTypes.GetMimeType(fullPath), Path.GetFileName(fullPath));
    }
    
}