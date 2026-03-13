namespace MindEase.Repo
{
    using Microsoft.EntityFrameworkCore;
    using MindEase.IRepo;
    using MindEase.Models;
    using MindEase.Models.Response;

    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse<Booking>> GetBookingAsync(int bookingId)
        {
            var response = new GeneralResponse<Booking>();

            var booking = await _context.Bookings
                .Include(b => b.Chat)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
            {
                response.Success = false;
                response.Message = "Booking not found";
                return response;
            }

            response.Data = booking;
            return response;
        }

        public async Task<GeneralResponse<Chat>> GetChatByBookingIdAsync(int bookingId)
        {


            try
            {
                var chat = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.BookingId == bookingId);

                if (chat == null)
                {
                    return new GeneralResponse<Chat>
                    {
                        Success = false,
                        Message = "Chat not found"
                    };
                }
                return new GeneralResponse<Chat>
                {
                    Success = true,
                    Data = chat
                };
            }
            catch (Exception ex)
            {

                return new GeneralResponse<Chat>
                {
                    Success = false,
                    Message = $"Error retrieving chat: {ex.Message}"
                };
            }

        }

        public async Task<GeneralResponse<Chat>> CreateChatAsync(int bookingId)
        {


            if (bookingId < 0)
            {
                return new GeneralResponse<Chat>
                {
                    Success = false,
                    Message = "Invalid booking ID"
                };
            }
            var chat = new Chat
            {
                BookingId = bookingId,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            return new GeneralResponse<Chat>
            {
                Success = true,
                Data = chat,
                Message = "Chat created successfully"
            };
        }

        public async Task<GeneralResponse<ChatMessage>> AddMessageAsync(ChatMessage message)
        {
            if (message == null)
            {
                return new GeneralResponse<ChatMessage>
                {
                    Success = false,
                    Message = "Message cannot be null"
                };
            }
            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return new GeneralResponse<ChatMessage>
            {
                Success = true,
                Data = message,
                Message = "Message added successfully"
            };
        }

        public async Task<GeneralResponse<List<ChatMessage>>> GetUnreadMessagesAsync(int chatId, string readerId)
        {

            var messages = await _context.ChatMessages
                .Where(m => m.ChatId == chatId &&
                            m.SenderId != readerId &&
                            !m.IsRead)
                .ToListAsync();
            if (messages == null)
            {
                return new GeneralResponse<List<ChatMessage>>
                {
                    Success = false,
                    Message = "No unread messages found"
                };
            }
            return new GeneralResponse<List<ChatMessage>>
            {
                Success = true,
                Data = messages
            };

        }

        public async Task<GeneralResponse<bool>> SaveChangesAsync()
        {
            var response = new GeneralResponse<bool>();

            await _context.SaveChangesAsync();

            response.Data = true;
            return response;
        }

        public async Task<GeneralResponse<List<ChatMessage>>> GetChatMessagesAsync(int bookingId)
        {
            if (bookingId == 0)
            {
                return new GeneralResponse<List<ChatMessage>>
                {
                    Success = false,
                    Message = "Invalid booking id"
                };
            }
            else
            {
                var chat = await _context.Chats.FirstOrDefaultAsync(c => c.BookingId == bookingId);
                if (chat == null)
                {
                    return new GeneralResponse<List<ChatMessage>>
                    {
                        Success = false,
                        Message = "Chat not found"
                    };
                }
                var messages = await _context.ChatMessages
                    .Where(m => m.ChatId == chat.Id)
                    .ToListAsync();
                return new GeneralResponse<List<ChatMessage>>
                {
                    Success = true,
                    Data = messages
                };
            }
        }
    }
}
