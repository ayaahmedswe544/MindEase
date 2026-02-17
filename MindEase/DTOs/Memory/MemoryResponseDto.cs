using MindEase.Models;

namespace MindEase.DTOs.Memory
{
    public class MemoryResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public MoodState MoodState { get; set; }

        public DateTime Date { get; set; }

        public string? ImageUrl { get; set; }
    }
}
