namespace MindEase.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using MindEase.DTOs.ChatMessage;
    using MindEase.IService;
    using MindEase.Models;
    using MindEase.Models.Response;
    using System.Security.Claims;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"Connected user: {userId}");
            await base.OnConnectedAsync();
        }
        private string GetUserId()
        {
            return Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        private string GetUserName()
        {
            return Context.User.FindFirst(ClaimTypes.Name)?.Value;
        }
        private string GetUserRole()
        {
            return Context.User.FindFirst(ClaimTypes.Role)?.Value;
        }

        private string GetRoom(int chatId)
        {
            return $"chat_{chatId}";
        }
        private MessageSenderType GetSenderType()
        {
            var role = GetUserRole();
            return role == "Doctor" ? MessageSenderType.Doctor : MessageSenderType.User;
        }

        public async Task JoinChat(int bookingId)
        {
            var response = await _chatService.GetOrCreateChat(bookingId);

            if (!response.Success)
              throw new HubException(response.Message);

            var chatId = response.Data.Id;

            await Groups.AddToGroupAsync(Context.ConnectionId, GetRoom(chatId));
           
        }

        public async Task SendMessage(int bookingId, string content)
        {
            var senderId = GetUserId();
            var senderType = GetSenderType();

            var response = await _chatService.SendMessage(
                bookingId,
                senderId,
                content,
                senderType);

            if (!response.Success)
                throw new HubException(response.Message);

            var chatId = response.Data.ChatId;
            var senderName = GetUserName();

            await Clients.Group(GetRoom(chatId))
                .SendAsync("ReceiveMessage", response.Data);

        }

        public async Task MarkAsRead(int bookingId)
        {
            var response = await _chatService.GetOrCreateChat(bookingId);

            if (!response.Success)
                throw new HubException(response.Message);

            var chatId = response.Data.Id;
            var userId = GetUserId();

            await _chatService.MarkMessagesAsRead(chatId, userId);
            await Clients.Group(GetRoom(chatId))
                .SendAsync("ChatSeen");
        }
        public async Task LoadMessages(int bookingid)
        {
            var response = await _chatService.GetChatMessages(bookingid);
            if (!response.Success)
                throw new HubException(response.Message);

            await Clients.Caller.SendAsync("LoadMessages", response.Data);
         
        }
    }
}