using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IJournalRepo
    {
        Task<GeneralResponse<Journal>> CreateAsync(Journal journal);
        Task<GeneralResponse<Journal>> UpdateJournalAsync(Journal journal);
        Task<GeneralResponse<Journal>> GetJournalByIdAsync(int id);
        Task<GeneralResponse<List<Journal>>> GetAllJournalsAsync(string userId);
        Task<GeneralResponse<bool>> DeleteAsync(int id);

    }
}
