using MindEase.Models;

namespace MindEase.DTOs.Client
{
    public class ClientDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string ProfilePicture { get; set; }
    }
}
