using MindEase.DTOs.Doctor;

namespace MindEase.DTOs.DoctorSchedule
{
    public class DoctorScheduleDto
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public DoctorDto Doctor { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; }
    }
}
