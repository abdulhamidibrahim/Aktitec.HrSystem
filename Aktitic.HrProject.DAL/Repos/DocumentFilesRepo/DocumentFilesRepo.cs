using Aktitic.HrProject.DAL.Context;
using Aktitic.HrProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.DAL.Repos;

public class DocumentFilesRepo(HrSystemDbContext context) : GenericRepo<DocumentFile>(context), IDocumentFilesRepo
{
    private readonly HrSystemDbContext _context = context;


    public async Task<List<DocumentFile>> GetDocumentFiles(int documentId)
    {
        if (_context.DocumentFiles != null)
            return await _context.DocumentFiles.Where(x => x.DocumentId == documentId).ToListAsync();
        
        return [];
    }
    
    public async Task<List<DocumentFile>> GetAllDocumentFilesAsync(int documentId, int page = 1, int pageSize = 10)
    {
        if (_context.DocumentFiles != null)
            return await _context.DocumentFiles
                .Where(x=>x.DocumentId == documentId && x.IsDeleted == false)
                .OrderBy(i => i.FileNumber)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        return [];    
    }

    // Add new item with sequential number
    public async Task<int> AddDocumentFileAsync(List<DocumentFile> documentFile)
    {
        // Find the first missing sequence
        for (var i= 0; i <documentFile.Count; i++)
        {
            var nextSequence = await GetNextSequenceAsync(documentFile[i].DocumentId);
            documentFile[i].FileNumber = nextSequence + i;
            _context.DocumentFiles?.Add(documentFile[i]);
        }

        return await _context.SaveChangesAsync();
    }

    // Update item and reorder remaining items
    // public async Task UpdateDocumentFileAsync(DocumentFile documentFile)
    // {
    //      _context.DocumentFiles.Update(documentFile);
    //     await _context.SaveChangesAsync();
    // }
    
    // Get next available sequence
    private async Task<int> GetNextSequenceAsync(int documentId)
    {
        var files = await _context.DocumentFiles.Where(x=>x.DocumentId == documentId).OrderBy(x => x.FileNumber).ToListAsync();

        var missingFileNumber = files
            .Select((x, i) => new { Item = x, Index = i + 1 })
            .FirstOrDefault(x => x.Item.FileNumber != x.Index);

        return missingFileNumber?.Index ?? files.Count + 1;
    }

    // Delete item and reorder remaining items
    public async Task<Task> DeleteDocumentAsync(int id)
    {
        var documentFile = await _context.DocumentFiles.FindAsync(id);
        if (documentFile != null)
        {
            var deletedSequence = documentFile.FileNumber;
            documentFile.IsDeleted = true;
            // await _context.SaveChangesAsync();
            // Reorder items
            var itemsToReorder = await _context.DocumentFiles
                                                        .Where(x => x.FileNumber > deletedSequence)
                                                        .ToListAsync();
            foreach (var reorderItem in itemsToReorder)
            {
                reorderItem.FileNumber -= 1;
            }
        }

        return Task.CompletedTask;
    }
}
