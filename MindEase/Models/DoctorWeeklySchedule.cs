namespace MindEase.Models
{
    public class DoctorWeeklySchedule
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; }
    }
}
