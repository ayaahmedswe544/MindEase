using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IJournalRepo
    {
        Task<Journal> AddJournalAsync(Journal journal);
        Task<Journal> GetJournalByIdAsync(int id);
        Task<IEnumerable<Journal>> GetAllJournalsAsync(string userId);
        Task<GeneralResponse<bool>> DeleteAsync(int id);

    }
}
