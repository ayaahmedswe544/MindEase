using MindEase.DTOs.ChatMessage;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IChatService
    {
        Task<GeneralResponse<Chat>> GetOrCreateChat(int bookingId);

        Task<GeneralResponse<ChatMessageDto>> SendMessage(
            int bookingId,
            string senderId,
            string content,
            MessageSenderType senderType);

        Task<GeneralResponse<bool>> MarkMessagesAsRead(int chatId, string readerId);
        Task<GeneralResponse<List<ChatMessageDto>>> GetChatMessages(int bookingId);
    }
}
