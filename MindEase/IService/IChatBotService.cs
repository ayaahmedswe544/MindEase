using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IChatBotService
    {
        Task<GeneralResponse<PythonChatResponse>> ProcessUserMessageAsync(string userId, string messageText);
        Task<GeneralResponse<List<ChatMessageBot>>> GetHistoryAsync(string userId);
    }
}
