using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IMoodEntryRepo
    {
        Task<GeneralResponse<MoodEntry>> CreateAsync(MoodEntry moodEntry);
        Task<GeneralResponse<MoodEntry>> UpdateAsync(MoodEntry moodEntry,string userId);
        Task<GeneralResponse<MoodEntry>> GetByIdAsync(int id,string userId);
        Task<GeneralResponse<List<MoodEntry>>> GetAllAsync(string userId);
        Task<GeneralResponse<bool>> DeleteAsync(int id, string userId);
    }
}
