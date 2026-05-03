using MindEase.Models;

namespace MindEase.IRepo
{
    public interface IChatBotRepository
    {
        Task AddMessageAsync(ChatMessageBot message);
        Task<List<ChatMessageBot>> GetUserHistoryAsync(string userId, int limit = 20);
    }
}
