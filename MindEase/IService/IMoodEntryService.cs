using Microsoft.AspNetCore.SignalR;
using MindEase.DTOs.MoodEntry;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IMoodEntryService
    {
            Task<GeneralResponse<MoodEntryDto>> CreateAsync(CreateMoodEntryDto moodEntrydto,string userId);
            Task<GeneralResponse<MoodEntryDto>> UpdateAsync(updateMoodEntryDto moodEntry,string userId);
            Task<GeneralResponse<MoodEntryDto>> GetByIdAsync(int id,string userId);
            Task<GeneralResponse<List<MoodEntryDto>>> GetAllAsync(string userId);
            Task<GeneralResponse<bool>> DeleteAsync(int id,string userId);


    }
}
