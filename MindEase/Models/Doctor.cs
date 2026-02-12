 using Microsoft.AspNetCore.Identity;
namespace MindEase.Models
   {
        public class Doctor:IdentityUser

    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string Bio { get; set; }

        public List<UserDoctor> UserDoctors { get; set; }
        public List<DoctorWeeklySchedule> WeeklySchedules { get; set; }
        public List<DoctorSessionSlot> SessionSlots { get; set; }
        public List<Booking> Bookings { get; set; }
    }   
}
