using Aktitic.HrProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController: ControllerBase
{
    private readonly INoteManager _noteManager;

    public NoteController(INoteManager noteManager)
    {
        _noteManager = noteManager;
    }
    
    [HttpGet]
    public async Task<List<NotesReadDto>> GetAll()
    {
        return await _noteManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public Task<NotesReadDto?> Get(int id)
    {
        var result = _noteManager.Get(id);
        if (result == null) return Task.FromResult<NotesReadDto?>(null);
        return result;
    }
    
    [HttpPost("create")]
    public ActionResult Add( NotesAddDto noteAddDto)
    {
        if (noteAddDto.SenderId != noteAddDto.ReceiverId)
        {
            try
            {
                var result = _noteManager.Add(noteAddDto);
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
        var result =_noteManager.Update(noteUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Note updated successfully.");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        var result= _noteManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Note deleted successfully.");
    }
    
    [HttpGet("getReceivedNotes/{userId}")]
    public  List<NotesReadDto> GetByReceiver(int userId)
    {
        return  _noteManager.GetByReceiver(userId);
    }
    
    [HttpGet("getSentNotes/{userId}")]
    public async Task<List<NotesReadDto>> GetBySender(int userId)
    {
        return await _noteManager.GetBySender(userId);
    }
    
    [HttpGet("getStarredNotes/{userId}")]
    public async Task<List<NotesReadDto>> GetStarred(int userId)
    {
        return await _noteManager.GetStarred(userId);
    }
}