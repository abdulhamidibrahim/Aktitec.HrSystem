namespace Aktitic.HrProject.BL;

public interface INoteManager
{
    public Task<int> Add(NotesAddDto notesAddDto);
    public Task<int> Update(NotesUpdateDto notesUpdateDto, int id);
    public Task<int> Delete(int id);
    public Task<NotesReadDto>? Get(int id);
    public Task<List<NotesReadDto>> GetAll();
    public List<NotesReadDto> GetByReceiver(int receiverId);
    public Task<List<NotesReadDto>> GetBySender(int senderId);
    public Task<List<NotesReadDto>> GetStarred(int userId);
}