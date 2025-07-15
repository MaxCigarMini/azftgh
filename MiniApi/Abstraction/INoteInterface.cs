using MiniApi.Model;

namespace MiniApi.Abstraction
{
    public interface INoteInterface
    {
        Note CreateNoteForUser(string title, string description, Note.Priority priority, User user);
        IEnumerable<Note> GetAllNoteUser(int UserId);
        Note UpdateNote(int id, string title, string description);
        void DeleteNote(int NoteId);
        IEnumerable<Note> GetNoteSortByPriority();
    }
}
