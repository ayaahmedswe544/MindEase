using Microsoft.AspNetCore.Identity;

namespace MindEase.Models
{
    public class GeneralUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
    }
}
