using MindEase.DTOs.Doctor;
using MindEase.DTOs.DoctorSessionSlot;
using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.Booking
{
    public class CreateBookingDto
    {
        [Required]
        public int DoctorSessionSlotId { get; set; }
        [Required]
        public string DoctorId { get; set; }
    }
}
