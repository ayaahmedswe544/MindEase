using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface ILibraryRepo
    {
        Task<GeneralResponse<List<LibraryItem>>> GetLibraryItemsByMoodAsync(LibraryMood libraryMood);
    }
}
