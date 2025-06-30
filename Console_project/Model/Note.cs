using System.Xml.Linq;

namespace Console_project.Model
{
    public class Note
    {
        public int NoteId { get; init;  }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; }
        public int UserId { get; init; }
    }
}
