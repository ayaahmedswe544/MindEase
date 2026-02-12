using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MindEase.Models
{
    public class User:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string MoodState { get; set; } //lessa feha klam
        public List<UserDoctor> UserDoctors { get; set; }
        public List<Memory>? Memories { get; set; }
        public List<MoodEntry>? MoodEntries { get; set; }
        public List<Journal>? Journals { get; set; }

    }
}
