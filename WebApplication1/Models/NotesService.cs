using Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore; // Required for ToListAsync
using Notes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface INoteService
{
    Task<IEnumerable<Note>> GetNotesByUserIdAsync(string userId);
}

public class NoteService : INoteService
{
    private readonly ApplicationDbContext _context;

    public NoteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetNotesByUserIdAsync(string userId)
    {
        if (!Guid.TryParse(userId, out Guid userGuid))
        {
            throw new ArgumentException("Invalid user ID format.");
        }

        return await _context.Notes
            .Where(note => note.UserId == userGuid)
            .ToListAsync(); // Extension method for IQueryable
    }
}
