using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.Models;

namespace MindEase.Repo
{
    public class ChatBotRepository:IChatBotRepository
    {
        private readonly AppDbContext _context;
        public ChatBotRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddMessageAsync(ChatMessageBot message)
        {
            _context.ChatMessageBot.Add(message);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ChatMessageBot>> GetUserHistoryAsync(string userId, int limit = 20)
        {
            return await _context.ChatMessageBot
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt) 
                .Take(limit)
                .OrderBy(m => m.CreatedAt) 
                .ToListAsync();
        }


    }
}
