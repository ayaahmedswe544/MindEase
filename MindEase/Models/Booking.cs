using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindEase.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("DoctorSessionSlot")]
        public int DoctorSessionSlotId { get; set; }
        public DoctorSessionSlot DoctorSessionSlot { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Chat Chat { get; set; }
        public BookingStatus BookingStatus { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
    }
}
