using Data.ApplicationDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.Models.ViewModel;
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

        [Route("SearchNotes")]
        [HttpGet]
        public async Task<IActionResult> SearchNotes([FromQuery] string label)
        {
            var notes = await db.Notes
                .Where(n => n.Label.Contains(label))
                .ToListAsync();

            return Ok(notes);
        }


        // POST api/<NotesController>
        [Route("AddNote")]
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteCreateViewModel newNote)
        {
            // Ensure the UserId exists
            var userExists = await db.Users.AnyAsync(u => u.Id == newNote.UserId);
            if (!userExists)
            {
                return BadRequest("User does not exist.");
            }

            var notes = new Note
            {
                Id = Guid.NewGuid(),
                Title = newNote.Title,
                Label = newNote.Label,
                Content = newNote.Content,
                UserId = newNote.UserId,
                BgColor = newNote.BgColor,
                FontColor = newNote.FontColor
            };

            if (!string.IsNullOrEmpty(newNote.Label))
            {
                var label = await db.Labels.FirstOrDefaultAsync(l => l.Name == newNote.Label);

                if (label == null)
                {
                    
                    label = new Label
                    {
                        Id = Guid.NewGuid(),
                        Name = newNote.Label
                    };
                    db.Labels.Add(label);
                    await db.SaveChangesAsync(); 
                }

                
                db.Notes.Add(notes);
                await db.SaveChangesAsync(); 

                var noteLabel = new NoteLabel
                {
                    NoteId = notes.Id,
                    LabelId = label.Id
                };
                db.NoteLabels.Add(noteLabel);
            }
            else
            {
                db.Notes.Add(notes);
                await db.SaveChangesAsync(); 
            }

            if (!string.IsNullOrEmpty(newNote.Label))
            {
                await db.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(CreateNote), new { id = notes.Id }, notes);
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
            existingNote.FontColor = updateNote.FontColor;
            existingNote.BgColor = updateNote.BgColor;
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
