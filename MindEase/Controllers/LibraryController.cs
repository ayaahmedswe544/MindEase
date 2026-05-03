using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Library;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }
        [HttpGet("Library")]

        public async Task<ActionResult<GeneralResponse<List<LibraryItemDto>>>> GetLibraryItemsByMoodAsync(LibraryMood libraryMood)
        {

            var response = await _libraryService.GetLibraryItemsByMoodAsync(libraryMood);
            return StatusCode(response.Success ? 200 : 400, response);
        }
    }
}
