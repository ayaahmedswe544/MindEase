using System.ComponentModel.DataAnnotations;

namespace MindEase.Models
{
    public class DoctorSessionSlot
    {
        [Key]
        public int Id { get; set; }

        public int DoctorWeeklyScheduleId { get; set; }
        public DoctorWeeklySchedule DoctorWeeklySchedule { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; } 
        public bool IsBooked { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public Booking? Booking { get; set; }
    }
}
