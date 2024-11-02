using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Services.PolicyServices;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailsController(IEmailsManager emailsManager) : ControllerBase
{
    [HttpGet]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<ActionResult<List<EmailsReadDto>>> GetAll(string email)
    {
        var emails = await emailsManager.GetAll(email);
        return Ok(emails);
    }
    
    [HttpGet("{id}")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public ActionResult<EmailsReadDto?> Get(int id)
    {
        var emails = emailsManager.Get(id);
        if (emails == null) return NotFound("Email not found");
        return Ok(emails);
    }
    
    [HttpPost("create")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Add))]
    public ActionResult Add([FromForm] EmailsAddDto emailsAddDto)
    {
        var result =emailsManager.Add(emailsAddDto);
        if (result.Result == 0) return BadRequest("Failed to add");
        return Ok("Email added successfully");
    }
    
    [HttpPut("update/{id}")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Edit))]
    public ActionResult Update([FromForm] EmailsUpdateDto emailsUpdateDto,int id)
    {
        var result =emailsManager.Update(emailsUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Email updated Successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Delete))]
    public ActionResult Delete(int id)
    { 
        var result =emailsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("GlobalSearch")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<IEnumerable<EmailsDto>> GlobalSearch(string search,string? column,string email)
    {
        return await emailsManager.GlobalSearch(search,column,email);
    }
    
    [HttpGet("getFilteredEmails")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public Task<FilteredEmailsDto> GetFilteredEmailsAsync
        (string? column, string? value1,string? operator1,[Optional] string? value2,
            string? operator2, int page, int pageSize,string email)
    {
        
        return emailsManager.GetFilteredEmailsAsync(column, value1, operator1, value2, operator2, page, pageSize, email);
    }
    
    
    [HttpGet("getStarredEmails")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<IEnumerable<EmailsDto>> GetStarredEmails(int? page, int? pageSize,string email)
    {
        return await emailsManager.GetStarredEmails(page, pageSize,email);
    }
    
    
    [HttpGet("getTrashedEmails")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<IEnumerable<EmailsDto>> GetTrashedEmails(int? page, int? pageSize, string email)
    {
        return await emailsManager.GetTrashedEmails(page, pageSize, email);
    }
    
    
    [HttpGet("getDraftedEmails")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<IEnumerable<EmailsDto>> GetUnreadEmails(int? page, int? pageSize,string email)
    {
        return await emailsManager.GetDraftEmails(page, pageSize, email);
    }
    
    
    [HttpGet("getArchivedEmails")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<IEnumerable<EmailsDto>> GetArchivedEmails(int? page, int? pageSize,string email)
    {
        return await emailsManager.GetArchivedEmails(page, pageSize, email);
    }
    
    
    [HttpGet("getSentEmails/{id}")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<IEnumerable<EmailsReadDto>> GetSentEmails(int id)
    {
        return await emailsManager.GetSentEmails(id);
    }
    
    [HttpPost("deleteRange")]
    public async Task<ActionResult> DeleteRange(int[] ids)
    {
        var result = await emailsManager.DeleteRange(ids.ToList());
        if (result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted Successfully!");
    }
    
    [HttpPost("trashRange")]
    public async Task<ActionResult> TrashRange(int[] ids, bool trash)
    {
        var result = await emailsManager.TrashRange(ids.ToList(),trash);
        if (result == 0) return BadRequest("Failed to Trash");
        return Ok("Trashed Successfully!");
    }
    
    [HttpGet("/api/attachments/download/{id}")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))]
    public async Task<ActionResult<IEnumerable<EmailsReadDto>>> GetAttachments(int id)
    {
        var file = await emailsManager.GetAttachments(id);

        // Construct the full file path
        // var fullPath = Path.Combine(webHostEnvironment.WebRootPath, file.Path);

        // Check if the file exists on the server
        if (!System.IO.File.Exists(file.Path))
        {
            return NotFound("Documents Not Found on the Server!");
        }

        // Read the file into a byte array
        var fileBytes = await System.IO.File.ReadAllBytesAsync(file.Path);

        // Return the file for download
        return File(fileBytes, MimeTypes.GetMimeType(file.Path), Path.GetFileName(file.Path));
        
        
        // var file = documentFileManager.DownloadFile(fileId); 
        // if (file == null) return NotFound("Files Not Found!"); 
        // var fullPath = file.Path;
        // if(!System.IO.File.Exists(fullPath)) return NotFound("File Not Found!");
        // var fileBytes = System.IO.File.ReadAllBytes(fullPath);
        // return File(fileBytes, MimeTypes.GetMimeType(fullPath), file.Name);
    }
    
    [HttpGet("searchEmails")]
    [AuthorizeRole(nameof(Pages.Email),nameof(Roles.Read))] 
    public List<EmailsDto> EmailSearch(string searchKey, int? sender, int? receiver, bool? trash, bool? starred, bool? archived, bool?  draft)
    {
        var searchEmailDto = new SearchEmailDto
        {
            SearchKey = searchKey,
            Sender = sender,
            Receiver = receiver,
            Trash = trash,
            Starred = starred,
            Archived = archived,
            Draft = draft
        };
        return emailsManager.EmailSearch(searchEmailDto);
    }
    
}