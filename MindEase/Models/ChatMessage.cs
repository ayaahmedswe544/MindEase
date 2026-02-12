using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindEase.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public string SenderId { get; set; }
        public MessageSenderType MessageSenderType { get; set; }
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }
       
    }
}
