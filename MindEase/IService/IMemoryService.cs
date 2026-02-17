using MindEase.DTOs.Memory;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IMemoryService
    {
        Task<GeneralResponse<MemoryResponseDto>> CreateAsync(CreateMemoryDto dto, string userId);
        Task<GeneralResponse<MemoryResponseDto>> GetByIdAsync(int id);
        Task<GeneralResponse<bool>> DeleteAsync(int id);
        Task<GeneralResponse<List<MemoryResponseDto>>> GetByUserIdAsync(string userId);
    }
}
