namespace MindEase.Models
{
    public class UserDoctor
    {
            public string UserId { get; set; }
            public User User { get; set; }

            public string DoctorId { get; set; }
            public Doctor Doctor { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        
    }
}
