using MindEase.Models;

namespace MindEase.DTOs.Journaling
{
    public class CreateJournalingDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
