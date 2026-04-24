using MindEase.DTOs.Doctor;
using MindEase.DTOs.DoctorSessionSlot;
using MindEase.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindEase.DTOs.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int? DoctorSessionSlotId { get; set; }
        public DoctorSessionSlotDto DoctorSessionSlot { get; set; }
        public string UserId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName {  get; set; }
        public DoctorDto Doctor { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }

    }
}
