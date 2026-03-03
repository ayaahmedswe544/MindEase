using MindEase.Models;

namespace MindEase.DTOs.Doctor
{
    public class updateDoctorDto
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Bio { get; set; }
        public IFormFile? ProfilePicture { get; set; }

    }
}
