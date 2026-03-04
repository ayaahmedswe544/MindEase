using Microsoft.EntityFrameworkCore;
using MindEase.Models;
using MindEase.IRepo;
using MindEase.Models.Response;

namespace MindEase.Repo
{
    public class JournalRepo : IJournalRepo
    {
        private readonly AppDbContext _context;

        public JournalRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Journal> AddJournalAsync(Journal journal)
        {
            _context.Journals.Add(journal);
            await _context.SaveChangesAsync();
            return journal;
        }

        public async Task<Journal> GetJournalByIdAsync(int id)
        {
            return await _context.Journals.FindAsync(id);
        }

        public async Task<IEnumerable<Journal>> GetAllJournalsAsync(string userId)
        {
            return await _context.Journals
                .Where(journal => journal.UserId == userId)
                .ToListAsync(); // تصفية السجلات بناءً على `UserId`
        }



        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var memory = await _context.Memories.FindAsync(id);
                if (memory == null)
                {
                    return new GeneralResponse<bool>
                    {
                        Success = false,
                        Message = "Memory not found."
                    };
                }

                _context.Memories.Remove(memory);
                await _context.SaveChangesAsync();

                return new GeneralResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Memory deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<bool>
                {
                    Success = false,
                    Message = "Failed to delete memory.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }

    }
}