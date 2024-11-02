using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController(INoteManager noteManager) : ControllerBase
{
    [HttpGet]
    public async Task<List<NotesReadDto>> GetAll()
    {
        return await noteManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public Task<NotesReadDto?> Get(int id)
    {
        var result = noteManager.Get(id);
        if (result == null) return Task.FromResult<NotesReadDto?>(null);
        return result;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> Add( NotesAddDto noteAddDto)
    {
        if (noteAddDto.SenderId != noteAddDto.ReceiverId)
        {
            try
            {
                var result =await noteManager.Add(noteAddDto);
                return Ok("Note added successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.InnerException?.Message);
            }
            
        }

        return BadRequest("Sender and receiver cannot be the same person.");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update( NotesUpdateDto noteUpdateDto,int id)
    {
        var result =noteManager.Update(noteUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Note updated successfully.");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result= noteManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Note deleted successfully.");
    }
    
    [HttpGet("getReceivedNotes/{userId}")]
    public  List<NotesReadDto> GetByReceiver(int userId)
    {
        return  noteManager.GetByReceiver(userId);
    }
    
    [HttpGet("getSentNotes/{userId}")]
    public async Task<List<NotesReadDto>> GetBySender(int userId)
    {
        return await noteManager.GetBySender(userId);
    }
    
    [HttpGet("getStarredNotes/{userId}")]
    public async Task<List<NotesReadDto>> GetStarred(int userId)
    {
        return await noteManager.GetStarred(userId);
    }
}