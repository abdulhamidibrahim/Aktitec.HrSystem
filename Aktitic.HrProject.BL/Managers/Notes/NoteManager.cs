using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.BL;

public class NoteManager(IUnitOfWork unitOfWork) : INoteManager
{
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
        unitOfWork.Notes.Add(notes);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(NotesUpdateDto notesUpdateDto, int id)
    {
        var notes = unitOfWork.Notes.GetById(id);

        if (notes == null) return Task.FromResult(0);
        if(notesUpdateDto.ReceiverId != null) notes.ReceiverId = notesUpdateDto.ReceiverId;
        if(notesUpdateDto.SenderId != null) notes.SenderId = notesUpdateDto.SenderId;
        if(notesUpdateDto.Content != null) notes.Content = notesUpdateDto.Content;
        if(notesUpdateDto.Starred != null) notes.Starred = notesUpdateDto.Starred;
        notes.Date = DateTime.Now;

        notes.UpdatedAt = DateTime.Now;
         unitOfWork.Notes.Update(notes);
         return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var notes = unitOfWork.Notes.GetById(id);
        if (notes==null) return Task.FromResult(0);
        notes.IsDeleted = true;
        notes.DeletedAt = DateTime.Now;
        unitOfWork.Notes.Update(notes);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<NotesReadDto>? Get(int id)
    {
        var notes = unitOfWork.Notes.GetById(id);
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
        var notes = unitOfWork.Notes.GetAll();
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
        var notes = unitOfWork.Notes.GetByReceiver(receiverId);
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
        var notes = unitOfWork.Notes.GetBySender(senderId);
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
        var notes = unitOfWork.Notes.GetStarred(userId);
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
