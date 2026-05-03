using MindEase.IRepo;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Repo
{
    public class LibraryRepo : ILibraryRepo
    {
        private readonly AppDbContext _context;
        public LibraryRepo(AppDbContext context) { 
            
            _context = context; 
        
        }
        public async Task<GeneralResponse<List<LibraryItem>>> GetLibraryItemsByMoodAsync(LibraryMood libraryMood)
        {
            if(!Enum.IsDefined(typeof(LibraryMood), libraryMood))
            {
               return new GeneralResponse<List<LibraryItem>>
                {
                    Data = null,
                    Success = false,
                    Message = "Invalid library mood."
                };
                
            }
            var libraryItems = _context.LibraryItems.Where(li => li.Mood == libraryMood).ToList();
            var response = new GeneralResponse<List<LibraryItem>>
            {
                Data = libraryItems,
                Success = true,
                Message = "Library items retrieved successfully."
            };
            return response;
        }
    }
}
