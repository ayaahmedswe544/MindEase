using MindEase.DTOs.Library;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface ILibraryService
    {
        Task<GeneralResponse<List<LibraryItemDto>>> GetLibraryItemsByMoodAsync(LibraryMood libraryMood);
    }
}
