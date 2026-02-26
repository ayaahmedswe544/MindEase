using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IMemoryRepository
    {
        Task<GeneralResponse<Memory>> CreateAsync(Memory memory, IFormFile? image);
        Task<GeneralResponse<Memory>> GetByIdAsync(int id);
        Task<GeneralResponse<bool>> DeleteAsync(int id);
        Task<GeneralResponse<List<Memory>>> GetByUserIdAsync(string userId);
        Task<GeneralResponse<Memory>> UpdateAsync(Memory memory, IFormFile image);
    }
}
