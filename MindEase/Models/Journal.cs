using System.ComponentModel.DataAnnotations;

namespace MindEase.Models
{
    public class Journal
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
