using ZAMETKI_FINAL.Model;

namespace ZAMETKI_FINAL.Abstraction
{
    public interface INoteInterface
    {
        public Note CreateNote(string title, string description, Priority NotePriority);
        public IEnumerable<Note> GetAllNotes();
        public Note UpdateNote(int noteId, string title, string description, Priority NotePriority, bool IsCompleted);
        public void DeleteNote(int NoteId);
        public Note GetNoteById(int NoteId);

        public void SaveNote();

    }
}
