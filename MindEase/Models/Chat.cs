using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindEase.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<ChatMessage> Messages { get; set; }
    }
}
