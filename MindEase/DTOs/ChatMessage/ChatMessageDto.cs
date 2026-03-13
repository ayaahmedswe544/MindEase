using MindEase.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindEase.DTOs.ChatMessage
{
    public class ChatMessageDto
    {
        public int Id { get; set; }

        public int ChatId { get; set; }
        public MessageSenderType MessageSenderType { get; set; }
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }

    }
}
