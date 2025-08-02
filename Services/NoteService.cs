using ZAMETKI_FINAL.Model;
using ZAMETKI_FINAL.Abstraction;

namespace ZAMETKI_FINAL.Services
{
    public class NoteService : INoteInterface
    {
        private readonly List<Note> _notes = new List<Note>();
        private static readonly User? _currentUser;

        public Note CreateNote(string title, string description, Priority NotePriority)
        {
            ValidateString(title, nameof(title));
            ValidateString(description, nameof(description));

            Note NewNote = new Note
            {
                NoteId = _notes.Count > 0 ? _notes.Max(n => n.NoteId) + 1 : 1,
                Title = title,
                Description = description,
                DateTimeCreate = DateTime.UtcNow,
                IsCompleted = false,
                NotePriority = NotePriority,
                Owner = _currentUser
            };
            _notes.Add(NewNote);

            SaveNote();

            return NewNote;
        }

        public IEnumerable<Note> GetAllNotes()
        {
            if (_currentUser == null)
                throw new ArgumentNullException(nameof(_currentUser), "User is not logged in");

            return _notes.Where(note => note.Owner == _currentUser);
        }

        public Note UpdateNote(int noteId, string title, string description, Priority NotePriority, bool IsCompleted)
        {
            ValidateString(title, nameof(title));
            ValidateString(description, nameof(description));

            var note = GetNoteById(noteId);
            note.Title = title;
            note.Description = description;
            note.NotePriority = NotePriority;
            note.IsCompleted = IsCompleted;

            SaveNote();

            return note;
        }
        public void DeleteNote(int NoteId)
        {
            var note = GetNoteById(NoteId);

            _notes.Remove(note);

            SaveNote();
        }
        public void SaveNote()
        {

        }




        public Note GetNoteById(int NoteId)
        {
            var NoteById = _notes.FirstOrDefault(n => n.NoteId == NoteId);
            if (NoteById == null)
                throw new KeyNotFoundException($"Note with id {NoteId} not found");

            return NoteById;
        }

        private void ValidateString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be empty", paramName);
        }
    }
}
