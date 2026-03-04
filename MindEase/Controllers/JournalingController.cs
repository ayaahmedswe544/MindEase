using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        // POST: api/journal
        [HttpPost]
        public async Task<IActionResult> CreateJournal([FromBody] JournalRequest request)
        {
            var journal = await _journalService.CreateJournalAsync(request.Title, request.Content, request.UserId);
            return Ok(journal);
        }

        // GET: api/journal/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJournalById(int id)
        {
            var journal = await _journalService.GetJournalByIdAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            return Ok(journal);
        }

        // GET: api/journal
        [HttpGet("{userId}")]

        public async Task<IActionResult> GetAllJournals(string userId)
        {
            var journals = await _journalService.GetAllJournalsAsync(userId);
            return Ok(journals);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(int id)
        {
            var response = await _journalService.DeleteAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }

    }

    public class JournalRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}