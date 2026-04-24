using MindEase.DTOs.Doctor;
using MindEase.DTOs.DoctorSchedule;
using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.DoctorSessionSlot
{
    public class DoctorSessionSlotDto
    {
        public int Id { get; set; }
        public int DoctorWeeklyScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }
        public bool IsBooked { get; set; }
    }
}
