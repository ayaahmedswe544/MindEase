using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.DoctorSchedule
{
    public class CreateDoctorScheduleDto
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
