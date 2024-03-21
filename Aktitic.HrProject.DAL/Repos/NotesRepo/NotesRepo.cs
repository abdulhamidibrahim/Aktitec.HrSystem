using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos.AttendanceRepo;

public class NotesRepo :GenericRepo<Notes>,INotesRepo
{
    private readonly HrSystemDbContext _context;

    public NotesRepo (HrSystemDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<NoteSender>> GetByReceiver(int receiverId)
    {
        if (_context.Notes != null)
        {
            var notes = await _context.Notes
                .Where(x => x.ReceiverId == receiverId)
                .ToListAsync();

            var noteSenders = notes.Select(note => new NoteSender
            {
                Notes = note,
                Employee = _context.Employees?.FirstOrDefault(e => e.Id == note.SenderId)!
            }).ToList();

            return noteSenders;
        }

        // Handle the case when _context.Notes is null more gracefully
        return new List<NoteSender>();
    }


    public Task<List<Notes>> GetBySender(int senderId)
    {
        if (_context.Notes != null) return _context.Notes.Where(x => x.SenderId == senderId).ToListAsync();
        return Task.FromResult(new List<Notes>());
    }

    public Task<List<Notes>> GetStarred(int userId)
    {
        if (_context.Notes != null) return _context.Notes
            .Where(x => x.ReceiverId == userId && x.Starred == true)
            .ToListAsync();
        return Task.FromResult(new List<Notes>());
    }
}
