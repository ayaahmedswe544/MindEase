namespace MindEase.Service
{
    using MindEase.DTOs.ChatMessage;
    using MindEase.IRepo;
    using MindEase.IService;
    using MindEase.Models;
    using MindEase.Models.Response;

    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<GeneralResponse<Chat>> GetOrCreateChat(int bookingId)
        {
            var bookingResponse = await _chatRepository.GetBookingAsync(bookingId);

            if (!bookingResponse.Success)
                return new GeneralResponse<Chat>
                {
                    Success = false,
                    Message = bookingResponse.Message
                };

            var booking = bookingResponse.Data;

            if (booking.BookingStatus != BookingStatus.Confirmed)
            {
                return new GeneralResponse<Chat>
                {
                    Success = false,
                    Message = "Chat allowed only for confirmed bookings"
                };
            }

            if (booking.Chat != null)
            {
                return new GeneralResponse<Chat>
                {
                    Success = true,
                    Data = booking.Chat
                };
            }

            return await _chatRepository.CreateChatAsync(bookingId);
        }

        public async Task<GeneralResponse<ChatMessageDto>> SendMessage(
            int bookingId,
            string senderId,
            string content,
            MessageSenderType senderType)
        {
            var chatResponse = await GetOrCreateChat(bookingId);

            if (!chatResponse.Success)
                return new GeneralResponse<ChatMessageDto>
                {
                    Success = false,
                    Message = chatResponse.Message
                };

            var chat = chatResponse.Data;

            var message = new ChatMessage
            {
                ChatId = chat.Id,
                SenderId = senderId,
                Content = content,
                MessageSenderType = senderType,
                SentAt = DateTime.UtcNow
            };

            await _chatRepository.AddMessageAsync(message);
            await _chatRepository.SaveChangesAsync();

            var MessageDto = new ChatMessageDto
            {
                ChatId = chat.Id,
                Content = content,
                MessageSenderType = senderType,
                SentAt = DateTime.UtcNow

            };
            return new GeneralResponse<ChatMessageDto>
            {
                Success = true,
                Data = MessageDto,
                Message = "Message sent"
            };
        }

        public async Task<GeneralResponse<bool>> MarkMessagesAsRead(int chatId, string readerId)
        {
            var unreadResponse = await _chatRepository.GetUnreadMessagesAsync(chatId, readerId);

            if (!unreadResponse.Success)
                return new GeneralResponse<bool>
                {
                    Success = true,
                    Message = unreadResponse.Message
                };

            foreach (var msg in unreadResponse.Data)
            {
                msg.IsRead = true;
            }

            await _chatRepository.SaveChangesAsync();

            return new GeneralResponse<bool>
            {
                Success = true,
                Data = true
            };
        }
        public async Task<GeneralResponse<List<ChatMessageDto>>> GetChatMessages(int bookingId)
        {
            var messagesResponse = await _chatRepository.GetChatMessagesAsync(bookingId);
            if (!messagesResponse.Success)
                return new GeneralResponse<List<ChatMessageDto>>
                {
                    Success = false,
                    Message = messagesResponse.Message
                };
            var messageDtos = messagesResponse.Data.Select(m => new ChatMessageDto
            {
                Id=m.Id,
                ChatId = m.ChatId,
                Content = m.Content,
                MessageSenderType = m.MessageSenderType,
                SentAt = m.SentAt
            }).ToList();
            return new GeneralResponse<List<ChatMessageDto>>
            {
                Success = true,
                Data = messageDtos
            };
        }
    }
}