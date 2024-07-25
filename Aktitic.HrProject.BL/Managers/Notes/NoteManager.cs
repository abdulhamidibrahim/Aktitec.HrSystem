
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class NoteManager:INoteManager
{
    private readonly IUnitOfWork _unitOfWork;

    public NoteManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<int> Add(NotesAddDto notesAddDto)
    {
        var notes = new Notes()
        {
            SenderId = notesAddDto.SenderId,
            ReceiverId = notesAddDto.ReceiverId,
            Content = notesAddDto.Content,
            Starred = notesAddDto.Starred,
            Date = DateTime.Now,
            CreatedAt = DateTime.Now,
        }; 
        _unitOfWork.Notes.Add(notes);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(NotesUpdateDto notesUpdateDto, int id)
    {
        var notes = _unitOfWork.Notes.GetById(id);

        if (notes == null) return Task.FromResult(0);
        if(notesUpdateDto.ReceiverId != null) notes.ReceiverId = notesUpdateDto.ReceiverId;
        if(notesUpdateDto.SenderId != null) notes.SenderId = notesUpdateDto.SenderId;
        if(notesUpdateDto.Content != null) notes.Content = notesUpdateDto.Content;
        if(notesUpdateDto.Starred != null) notes.Starred = notesUpdateDto.Starred;
        notes.Date = DateTime.Now;

        notes.UpdatedAt = DateTime.Now;
         _unitOfWork.Notes.Update(notes);
         return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var notes = _unitOfWork.Notes.GetById(id);
        if (notes==null) return Task.FromResult(0);
        notes.IsDeleted = true;
        notes.DeletedAt = DateTime.Now;
        _unitOfWork.Notes.Update(notes);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<NotesReadDto>? Get(int id)
    {
        var notes = _unitOfWork.Notes.GetById(id);
        if (notes == null) return null;
        return Task.FromResult(new NotesReadDto()
        {
            Id = notes.Id,
            SenderId = notes.SenderId,
            ReceiverId = notes.ReceiverId,
            Content = notes.Content,
            Starred = notes.Starred,
            Date = notes.Date
        });
    }

    public Task<List<NotesReadDto>> GetAll()
    {
        var notes = _unitOfWork.Notes.GetAll();
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

    public List<NotesReadDto> GetByReceiver(int receiverId)
    {
        var notes = _unitOfWork.Notes.GetByReceiver(receiverId);
        return (notes.Result.Select(note => new NotesReadDto()
        {
            Id = note.Id,
            SenderId = note.SenderId,
            ReceiverId = note.ReceiverId,
            Content = note.Content,
            Starred = note.Starred,
            Date = note.Date,
            FullName = note.Sender?.FullName,
            UserName = note.Sender?.UserName,
            Email = note.Sender?.Email,
            ImgUrl = note.Sender?.ImgUrl,
            ImgId = note.Sender?.ImgId,
            Gender = note.Sender?.Gender,
            Age = note.Sender?.Age,
            JobPosition = note.Sender?.JobPosition
            
        }).ToList());
    }

    public Task<List<NotesReadDto>> GetBySender(int senderId)
    {
        var notes = _unitOfWork.Notes.GetBySender(senderId);
        return Task.FromResult(notes.Result.Select(note => new NotesReadDto()
        {
            Id = note.Id,
            SenderId = note.SenderId,
            ReceiverId = note.ReceiverId,
            Content = note.Content,
            Starred = note.Starred,
            Date = note.Date,
            FullName = note.Receiver?.FullName,
            UserName = note.Receiver?.UserName,
            Email = note.Receiver?.Email,
            ImgUrl = note.Receiver?.ImgUrl,
            ImgId = note.Receiver?.ImgId,
            Gender = note.Receiver?.Gender,
            Age = note.Receiver?.Age,
            JobPosition = note.Receiver?.JobPosition
        }).ToList());
    }

    public Task<List<NotesReadDto>> GetStarred(int userId)
    {
        var notes = _unitOfWork.Notes.GetStarred(userId);
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
