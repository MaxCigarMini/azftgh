namespace ZAMETKI_FINAL.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // byte[] желательно required

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
