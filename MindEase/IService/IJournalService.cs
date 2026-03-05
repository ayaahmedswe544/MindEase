using MindEase.DTOs.Doctor;
using MindEase.DTOs.Journaling;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IJournalService
    {
        Task<GeneralResponse<JournalingDto>> CreateJournalAsync(CreateJournalingDto input, string userId);
        Task<GeneralResponse<JournalingDto>> UpdateAsync(UpdateJournalingDto input, string userId);
        Task<GeneralResponse<JournalingDto>> GetJournalByIdAsync(int id);
        Task<GeneralResponse<List<JournalingDto>>> GetAllJournalsAsync(string userId);
        Task<GeneralResponse<bool>> DeleteAsync(int id);

    }
}
