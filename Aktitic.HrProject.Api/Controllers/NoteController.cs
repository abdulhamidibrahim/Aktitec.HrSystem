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
        var user = _noteManager.Get(id);
        if (user == null) return Task.FromResult<NotesReadDto?>(null);
        return user;
    }
    
    [HttpPost("create")]
    public ActionResult Add([FromForm] NotesAddDto noteAddDto)
    {
        if (noteAddDto.SenderId != noteAddDto.ReceiverId)
        {
            try
            {
                var result = _noteManager.Add(noteAddDto);
                if (result.Result > 0) return Ok("Note added successfully.");
                return BadRequest("failed to add note. try entering valid data.");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.InnerException?.Message);
            }
            
        }

        return BadRequest("Sender and receiver cannot be the same person.");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromBody] NotesUpdateDto noteUpdateDto,int id)
    {
        _noteManager.Update(noteUpdateDto,id);
        return Ok("Note updated successfully.");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
        _noteManager.Delete(id);
        return Ok("Note deleted successfully.");
    }
    
    [HttpGet("getReceivedNotes/{userId}")]
    public async Task<List<NotesReadDto>> GetByReceiver(int userId)
    {
        return await _noteManager.GetByReceiver(userId);
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