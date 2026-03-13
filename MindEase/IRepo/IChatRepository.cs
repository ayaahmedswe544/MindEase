using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IChatRepository
    {
        Task<GeneralResponse<Booking>> GetBookingAsync(int bookingId);

        Task<GeneralResponse<Chat>> GetChatByBookingIdAsync(int bookingId);

        Task<GeneralResponse<Chat>> CreateChatAsync(int bookingId);

        Task<GeneralResponse<ChatMessage>> AddMessageAsync(ChatMessage message);

        Task<GeneralResponse<List<ChatMessage>>> GetUnreadMessagesAsync(int chatId, string readerId);

        Task<GeneralResponse<bool>> SaveChangesAsync();
        public Task<GeneralResponse<List<ChatMessage>>> GetChatMessagesAsync(int bookingId);
    }
}
