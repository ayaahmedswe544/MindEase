using MindEase.Models;

namespace MindEase.DTOs.Client
{
    public class updateClientDto
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public IFormFile? ProfilePicture { get; set; }

    }
}
