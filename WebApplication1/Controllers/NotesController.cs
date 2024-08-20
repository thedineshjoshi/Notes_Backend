using Data.ApplicationDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Models;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NotesController : ControllerBase
    {
        public ApplicationDbContext db;
        private readonly INoteService _noteService;
        public NotesController(ApplicationDbContext db, INoteService noteService)
        {
            this.db = db;
            _noteService = noteService;
        }

        // GET: api/<NotesController>
        [Route("GetNotes")]
        [HttpGet]
        public async Task<IActionResult> GetNotesByUser()
        {
            var userId = User.FindFirst("UserId")?.Value; 
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var notes = await _noteService.GetNotesByUserIdAsync(userId);
            return Ok(notes);
        }

        // POST api/<NotesController>
        [Route("AddNote")]
        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteCreateViewModel newNote)
        {

            var notes = new Note
            {
                Id = Guid.NewGuid(),
                Title = newNote.Title,
                Label = newNote.Label,
                Content = newNote.Content,
                UserId = newNote.UserId
            };
            db.Add(notes);
            db.SaveChanges();
            return Ok(notes);
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteAsync(Guid id, [FromBody] NoteUpdateModel updateNote)
        {
            
            var existingNote = await db.Notes.FindAsync(id);
            if (existingNote == null)
            {
                return NotFound(); 
            }
            existingNote.Title = updateNote.Title;
            existingNote.Label = updateNote.Label;
            existingNote.Content = updateNote.Content;
            existingNote.UpdatedAt = DateTime.UtcNow; 
            await db.SaveChangesAsync();
            return Ok(existingNote); 
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(Guid id)
        {
            
            var note = await db.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound(); 
            }

            db.Notes.Remove(note);
            await db.SaveChangesAsync();

            return NoContent(); 
        }

    }
}
