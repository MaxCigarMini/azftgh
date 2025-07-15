using MiniApi.Abstraction;
using MiniApi.Model;
using System.Reflection;

namespace MiniApi.Services
{
    public class NoteService : INoteInterface
    {
        private List<Note> _notes = new List<Note>();
        private User user;
        public Note CreateNoteForUser(string title, string description, Note.Priority priority, User user)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException("title cannot be empty", nameof(title));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));

            var newNote = new Note
            {
                NoteId = _notes.Count > 0 ? _notes.Max(n => n.NoteId) + 1 : 1,
                Title = title,
                Description = description,
                NotePriority = priority,
                User = user,
            };
            _notes.Add(newNote);
            return newNote;
        }
        public IEnumerable<Note> GetAllNoteUser(int UserId)
        {
            if (UserId <= 0)
            {
                throw new ArgumentException("User ID must be positive", nameof(UserId));
            }

            return _notes.Where(note => note.User?.UserId == UserId);
        }
        public Note UpdateNote(int id, string title, string description) 
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));

            var note = _notes.FirstOrDefault(i => i.NoteId == id);
            note.Title = title;
            note.Description = description;
            note.CreatedDate = DateTime.UtcNow;

            return note;
        }
        public void DeleteNote(int NoteId)
        {
            if (NoteId <= 0)
                throw new ArgumentException("Invalid note ID", nameof(NoteId));

            var noteToDelete = _notes.FirstOrDefault(n =>  n.NoteId == NoteId);

            if (noteToDelete == null)
                throw new KeyNotFoundException($"Note with ID {NoteId} not found");

            _notes.Remove(noteToDelete);
        }
        public IEnumerable<Note> GetNoteSortByPriority()
        {
            if (!_notes.Any())
                return Enumerable.Empty<Note>();

            return _notes
                .OrderBy(note => note.NotePriority)
                .ThenByDescending(note => note.CreatedDate)
                .ToList();
        }
    }
}
