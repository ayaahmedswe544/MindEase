using System.ComponentModel.DataAnnotations;

namespace MindEase.Models
{
    public class DoctorSessionSlot
    {
        [Key]
        public int Id { get; set; }

        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public SlotStatus SlotStatus { get; set; }
        public Booking Booking { get; set; }
    }
}
