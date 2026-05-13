using MindEase.Models;

namespace MindEase.DTOs.Doctor
{
    public class DoctorDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Specialization { get; set; }
        public int? SessionTime { get; set; }
        public int? Price { get; set; }
        public string? Bio { get; set; }
        public string ProfilePicture { get; set; }
    }
}
