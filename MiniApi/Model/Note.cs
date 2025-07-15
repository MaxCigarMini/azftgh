namespace MiniApi.Model
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; }
        public Priority NotePriority { get; set; } = Priority.Medium;
        public User User { get; set; }

        public enum Priority
        {
            Low = 1,
            Medium = 2,
            High = 3,
            Highest = 4
        }
    }

}
