using System.Reflection;

namespace ZAMETKI_FINAL.Model
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime DateTimeModified { get; set; }
        public bool IsCompleted { get; set; }
        public Priority NotePriority { get; set; }
        public User Owner { get; set; }

    }
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Highest = 4
    }
}
