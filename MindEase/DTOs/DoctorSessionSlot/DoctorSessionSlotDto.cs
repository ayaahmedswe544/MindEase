using MindEase.DTOs.Doctor;
using MindEase.DTOs.DoctorSchedule;
using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.DoctorSessionSlot
{
    public class DoctorSessionSlotDto
    {
        public int Id { get; set; }
        public string DoctorWeeklyScheduleId { get; set; }
        public DoctorScheduleDto DoctorWeeklySchedule { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }
}
