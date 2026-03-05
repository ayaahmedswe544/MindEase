using MindEase.Models;
using System.ComponentModel.DataAnnotations;

namespace MindEase.DTOs.Journaling
{
    public class UpdateJournalingDto
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
