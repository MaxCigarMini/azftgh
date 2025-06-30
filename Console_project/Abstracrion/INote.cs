using Console_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_project.Interface;


namespace Console_project.Interface
{
    public interface INote
    {
        IEnumerable<Note> GetAllNotes(int UserId);
        Note GetNoteById(int NoteId);
        Note AddNote(int NoteId, string title, string description);
        void CompleteNote(int id);
        void ChangeNote(int id, string title, string description);
        void RemoveNote(int id);
    }
}
