using MindEase.DTOs.Doctor;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IJournalService
    {
        Task<Journal> CreateJournalAsync(string title, string content, string userId);
        Task<Journal> GetJournalByIdAsync(int id);
        Task<IEnumerable<Journal>> GetAllJournalsAsync(string userId);
        Task<GeneralResponse<bool>> DeleteAsync(int id);

    }
}
