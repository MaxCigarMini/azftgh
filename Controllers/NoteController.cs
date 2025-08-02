using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ZAMETKI_FINAL.Abstraction;
using ZAMETKI_FINAL.Dto_Vm;
using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class NoteController : ControllerBase
    {
        private readonly INoteInterface _noteService;

        public NoteController(INoteInterface noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("by_Owner")]
        public ActionResult<IEnumerable<Note>> GetAllNotes()
        {
            try
            {
                var notes = _noteService.GetAllNotes();
                return Ok(notes);
            }
            catch (ArgumentNullException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{NoteId}")]
        public IActionResult GetNoteById(int NoteId)
        {
            try
            {
                var note = _noteService.GetNoteById(NoteId);
                return Ok(note);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteDto noteDTO)
        {
            try
            {
                var createdNote = _noteService.CreateNote(noteDTO.title, noteDTO.description, noteDTO.NotePriority);
                return CreatedAtAction(
                    nameof(GetNoteById),
                    new { id = createdNote.NoteId },
                    createdNote
                    );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{NoteId}")]
        public IActionResult UpdateNote(int NoteId, [FromBody] NoteUpdateDto noteUpdateDTO)
        {
            try
            {
                var updateNote = _noteService.UpdateNote(NoteId, noteUpdateDTO.title, noteUpdateDTO.description, noteUpdateDTO.NotePriority, noteUpdateDTO.isCompleted);
                return Ok(updateNote);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{NoteId}")]
        public IActionResult DeleteNote(int NoteId)
        {
            try
            {
                _noteService.DeleteNote(NoteId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}
