using MindEase.DTOs.ChatBot;

namespace MindEase.Models
{
    public class PythonChatRequest
    {
        public string message { get; set; } = string.Empty;
        public List<PythonMessageDto> history { get; set; } = new();
    }
}
