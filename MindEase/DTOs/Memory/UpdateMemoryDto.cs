using MindEase.Models;

namespace MindEase.DTOs.Memory
{
    public class UpdateMemoryDto
    {
        public string? Title { get; set; }

        public MoodState? MoodState { get; set; }

        public IFormFile? Image { get; set; }
    }
}
