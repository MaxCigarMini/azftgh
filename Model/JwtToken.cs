namespace ZAMETKI_FINAL.Model
{
    public class JwtToken
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        // Поля для связки.
        public Guid UserId { get; set; }
        // default значение, которое выставит нам null, но не позволит обратится к полю. Удобно,чтобы потом это заполнил контекст.
        public virtual User User { get; set; } = null!;
    }
}
