using System.ComponentModel.DataAnnotations;

namespace MindEase.Models
{
    public class Memory
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public MoodState MoodState { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Image { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
