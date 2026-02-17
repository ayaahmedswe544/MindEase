using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.Memory
{
    public class CreateMemoryDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public MoodState MoodState { get; set; }

        public IFormFile? Image { get; set; }
    }
}
