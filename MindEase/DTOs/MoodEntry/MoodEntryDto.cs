using MindEase.Models;

namespace MindEase.DTOs.MoodEntry
{
    public class MoodEntryDto
    {
        public int Id { get; set; }
        public MoodType MoodType { get; set; }
        public DateOnly Date { get; set; } 
    }
}
