using System.ComponentModel.DataAnnotations;

namespace MindEase.Models
{
    public class MoodEntry
    {

        [Key]
        public int Id { get; set; }
        public MoodType State { get; set; }
        public DateOnly Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }



    }
}
