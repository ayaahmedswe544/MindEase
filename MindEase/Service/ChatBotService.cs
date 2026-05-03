using MindEase.DTOs.ChatBot;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using MindEase.Repo;

namespace MindEase.Service
{
    public class ChatBotService:IChatBotService
    {
        private readonly HttpClient _httpClient;
        private readonly IChatBotRepository _chatBotRepository;
        public ChatBotService(HttpClient httpClient, IChatBotRepository chatBotRepository)
        {
            _httpClient = httpClient;
            _chatBotRepository = chatBotRepository;
        }

        public async Task<GeneralResponse<PythonChatResponse>> ProcessUserMessageAsync(string userId, string messageText)
        {
            var response = new GeneralResponse<PythonChatResponse>();
            try
            {
                var userHistory = await _chatBotRepository.GetUserHistoryAsync(userId, 20);
                var recentHistory = userHistory.Select(m => new PythonMessageDto
                {
                    role = m.Sender == "Bot" ? "assistant" : "user",
                    content = m.Content
                }).ToList();
                await _chatBotRepository.AddMessageAsync(new ChatMessageBot
                {
                    UserId = userId,
                    Sender = "User",
                    Content = messageText
                });
                var pythonRequest = new PythonChatRequest
                {
                    message = messageText,
                    history = recentHistory
                };
                var httpResponse = await _httpClient.PostAsJsonAsync("/chat", pythonRequest);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = $"FastAPI returned an error. HTTP Status: {httpResponse.StatusCode}";
                    return response;
                }
                var pythonData = await httpResponse.Content.ReadFromJsonAsync<PythonChatResponse>();
                if (pythonData == null)
                {
                    response.Success = false;
                    response.Message = "Failed to deserialize response from FastAPI.";
                    return response;
                }
                if (!string.IsNullOrEmpty(pythonData.error))
                {
                    response.Success = false;
                    response.Message = $"Chatbot Engine Error: {pythonData.error}";
                    return response;
                }
                await _chatBotRepository.AddMessageAsync(new ChatMessageBot
                {
                    UserId = userId,
                    Sender = "Bot",
                    Content = pythonData.reply ?? "No reply generated."
                });
                response.Data = pythonData;
                response.Message = "Message processed successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An internal exception occurred: {ex.Message}";
            }
            return response;
        }


        public async Task<GeneralResponse<List<ChatMessageBot>>> GetHistoryAsync(string userId)
        {
            var response = new GeneralResponse<List<ChatMessageBot>>();

            try
            {
                var history = await _chatBotRepository.GetUserHistoryAsync(userId, 50);
                response.Data = history;
                response.Message = "History retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Could not retrieve history: {ex.Message}";
            }
            return response;
        }
    }
}
