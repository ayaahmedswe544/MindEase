using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.Auth
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required]
        [Range(25, 100)]
        public int Age { get; set; }
    }
}
