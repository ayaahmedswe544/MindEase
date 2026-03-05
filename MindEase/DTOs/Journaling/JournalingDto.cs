using MindEase.Models;

namespace MindEase.DTOs.Journaling
{
    public class JournalingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
