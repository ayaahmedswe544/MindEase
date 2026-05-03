using MindEase.DTOs.Library;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
namespace MindEase.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepo _libraryRepo;
        public LibraryService(ILibraryRepo libraryRepo) {

            _libraryRepo = libraryRepo;
        
        }
        public async Task<GeneralResponse<List<LibraryItemDto>>> GetLibraryItemsByMoodAsync(LibraryMood libraryMood)
        {
            
            if (!Enum.IsDefined(typeof(LibraryMood), libraryMood))
            {
                return new GeneralResponse<List<LibraryItemDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "Invalid library mood."
                };

            }
            try
            {
                var response = await _libraryRepo.GetLibraryItemsByMoodAsync(libraryMood);

                if (!response.Success)
                { return new GeneralResponse<List<LibraryItemDto>>
                    {
                        Data = null,
                        Success = false,
                        Message = response.Message
                    };
                }
                else
                {
                    var libraryItemDtos = response.Data.Select(li => new LibraryItemDto
                    {
                        Id = li.Id,
                        Title = li.Title,
                        Mood = li.Mood,
                        ContentUrl = li.ContentUrl,
                        ImageUrl = li.ImageUrl,
                        Type = li.Type
                    }).ToList();

                    return new GeneralResponse<List<LibraryItemDto>>
                    {
                        Data = libraryItemDtos,
                        Success = true,
                        Message = "Library items retrieved successfully."
                    };

                }
            }
            catch(Exception ex) {
            
                return new GeneralResponse<List<LibraryItemDto>>
                {
                    Data = null,
                    Success = false,
                    Message = "An error occurred while retrieving library items.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }

        }
    }
}
