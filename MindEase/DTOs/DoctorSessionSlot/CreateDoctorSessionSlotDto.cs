using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.DoctorSessionSlot
{
    public class CreateDoctorSessionSlotDto
    {
        [Required]
        public string DoctorWeeklyScheduleId { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsBooked { get; set; }
    }
}
