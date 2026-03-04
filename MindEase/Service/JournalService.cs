using Microsoft.AspNetCore.Identity;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Service
{
    public class JournalService : IJournalService
    {
        private readonly IJournalRepo _journalRepo;
        private readonly UserManager<GeneralUser> _userManager;

        public JournalService(IJournalRepo journalRepo, UserManager<GeneralUser> userManager)
        {
            _journalRepo = journalRepo;
            _userManager = userManager;

        }

        public async Task<Journal> CreateJournalAsync(string title, string content, string userId)
        {
            var journal = new Journal
            {
                Title = title,
                Content = content,
                UserId = userId
            };

            return await _journalRepo.AddJournalAsync(journal);
        }

        public async Task<Journal> GetJournalByIdAsync(int id)
        {
            return await _journalRepo.GetJournalByIdAsync(id);
        }

        public async Task<IEnumerable<Journal>> GetAllJournalsAsync(string userId)
        {
            return await _journalRepo.GetAllJournalsAsync(userId);
        }


        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            return await _journalRepo.DeleteAsync(id);
        }

    }
}