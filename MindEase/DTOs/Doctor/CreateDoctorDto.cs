using MindEase.Models;

namespace MindEase.DTOs.Doctor
{
    public class CreateDoctorDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Specialization { get; set; }
        public string Bio { get; set; }
        

    }
}
