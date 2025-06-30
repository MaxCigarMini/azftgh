using Console_project.Interface;
using Console_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Console_project.Repository
{
    public class NoteService : INote
    {
        private const string NoteFileJson = "notes.json";
        private List<Note> _notes = new List<Note>();
        private int _lastNoteId;
        private static User _currentuser;

        public IEnumerable<Note> GetAllNotes(int UserId)
        {
            if (UserId <= 0)
                throw new ArgumentNullException(nameof(UserId), "Invalid user ID");

            var userNotes = _notes.Where(n => n.UserId == UserId).ToList();

            return userNotes;
        }

        public Note GetNoteById(int id)
        {
            var note = _notes.FirstOrDefault(n => n.NoteId == id);
            if (note == null)
                throw new KeyNotFoundException($"Note with id {id} not found");

            return note;
        }

        public void ChangeNote(int id, string title, string description)
        {
            ValidateString(title, nameof(title));

            var note = GetNoteById(id);
            note.Title = title;
            note.Description = description;
        }

        public void RemoveNote(int id)
        {
            var note = GetNoteById(id);
            _notes.Remove(note);
            SaveNote();
        }

        public Note AddNote(int NoteId, string title, string description)
        {
            ValidateString(title, nameof(title));
            ValidateString(description, nameof(description));


            var newNote = new Note
            {
                NoteId = ++_lastNoteId,
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedDate = DateTime.UtcNow,
            };

            _notes.Add(newNote);
            SaveNote();
            return newNote;
        }

        public void CompleteNote(int id)
        {
            var note = GetNoteById(id);
            note.IsCompleted = true;
        }
        private void ValidateString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be empty", paramName);
        }
        public void SaveNote()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(NoteFileJson, JsonSerializer.Serialize(_notes, options));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }
        public void LoadNote()
        {
            try
            {
                if (File.Exists(NoteFileJson))
                {
                    var json = File.ReadAllText(NoteFileJson);
                    _notes = JsonSerializer.Deserialize<List<Note>>(json) ?? new List<Note>();
                    _lastNoteId = _notes.Count > 0 ? _notes.Max(n => n.NoteId) : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                _notes = new List<Note>();
                _lastNoteId = 0;
            }
        }
    }
}

