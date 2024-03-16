
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class NoteManager:INoteManager
{
    private readonly INotesRepo _notesRepo;

    public NoteManager(INotesRepo notesRepo)
    {
        _notesRepo = notesRepo;
    }
    
    public Task<int> Add(NotesAddDto notesAddDto)
    {
        var notes = new Notes()
        {
            SenderId = notesAddDto.SenderId,
            ReceiverId = notesAddDto.ReceiverId,
            Content = notesAddDto.Content,
            Starred = notesAddDto.Starred,
            Date = DateTime.Now
        };
        return _notesRepo.Add(notes);
    }

    public Task<int> Update(NotesUpdateDto notesUpdateDto, int id)
    {
        var notes = _notesRepo.GetById(id);
        
        if (notes.Result == null) return Task.FromResult(0);
        if(notesUpdateDto.ReceiverId != null) notes.Result.ReceiverId = notesUpdateDto.ReceiverId;
        if(notesUpdateDto.SenderId != null) notes.Result.SenderId = notesUpdateDto.SenderId;
        if(notesUpdateDto.Content != null) notes.Result.Content = notesUpdateDto.Content;
        if(notesUpdateDto.Starred != null) notes.Result.Starred = notesUpdateDto.Starred;
        notes.Result.Date = DateTime.Now;


        return _notesRepo.Update(notes.Result);
    }

    public Task<int> Delete(int id)
    {
        var notes = _notesRepo.GetById(id);
        if (notes.Result != null) return _notesRepo.Delete(notes.Result);
        return Task.FromResult(0);
    }

    public Task<NotesReadDto>? Get(int id)
    {
        var notes = _notesRepo.GetById(id);
        if (notes.Result == null) return null;
        return Task.FromResult(new NotesReadDto()
        {
            Id = notes.Result.Id,
            SenderId = notes.Result.SenderId,
            ReceiverId = notes.Result.ReceiverId,
            Content = notes.Result.Content,
            Starred = notes.Result.Starred,
            Date = notes.Result.Date
        });
    }

    public Task<List<NotesReadDto>> GetAll()
    {
        var notes = _notesRepo.GetAll();
        return Task.FromResult(notes.Result.Select(note => new NotesReadDto()
        {
            Id = note.Id,
            SenderId = note.SenderId,
            ReceiverId = note.ReceiverId,
            Content = note.Content,
            Starred = note.Starred,
            Date = note.Date
        }).ToList());
    }

    public Task<List<NotesReadDto>> GetByReceiver(int receiverId)
    {
        var notes = _notesRepo.GetByReceiver(receiverId);
        return Task.FromResult(notes.Result.Select(note => new NotesReadDto()
        {
            Id = note.Notes.Id,
            SenderId = note.Notes.SenderId,
            ReceiverId = note.Notes.ReceiverId,
            Content = note.Notes.Content,
            Starred = note.Notes.Starred,
            Date = note.Notes.Date,
            FullName = note.Employee.FullName,
            UserName = note.Employee.UserName,
            Email = note.Employee.Email,
            ImgUrl = note.Employee.ImgUrl,
            ImgId = note.Employee.ImgId,
            Gender = note.Employee.Gender,
            Age = note.Employee.Age,
            JobPosition = note.Employee.JobPosition
            
        }).ToList());
    }

    public Task<List<NotesReadDto>> GetBySender(int senderId)
    {
        var notes = _notesRepo.GetBySender(senderId);
        return Task.FromResult(notes.Result.Select(note => new NotesReadDto()
        {
            Id = note.Id,
            SenderId = note.SenderId,
            ReceiverId = note.ReceiverId,
            Content = note.Content,
            Starred = note.Starred,
            Date = note.Date
        }).ToList());
    }

    public Task<List<NotesReadDto>> GetStarred(int userId)
    {
        var notes = _notesRepo.GetStarred(userId);
        return Task.FromResult(notes.Result.Select(note => new NotesReadDto()
        {
            Id = note.Id,
            SenderId = note.SenderId,
            ReceiverId = note.ReceiverId,
            Content = note.Content,
            Starred = note.Starred,
            Date = note.Date
        }).ToList());
    }
}
