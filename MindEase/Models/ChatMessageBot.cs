namespace MindEase.Models
{
    public class ChatMessageBot
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty; // "User" or "Bot"
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
