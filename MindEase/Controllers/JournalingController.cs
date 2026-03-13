using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Journaling;
using MindEase.DTOs.Memory;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System.Security.Claims;

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
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<JournalingDto>>> CreateJournal([FromForm] CreateJournalingDto request)
        {
            var userId = GetUserId();
            var journalResponse = await _journalService.CreateJournalAsync(request, userId);
            return StatusCode(journalResponse.Success ? 200 : 400, journalResponse);

        }

        [HttpPost("update")]
        public async Task<ActionResult<GeneralResponse<JournalingDto>>> UpdateJournal([FromForm] UpdateJournalingDto dto)
        {
            var userId = GetUserId();
            var response = await _journalService.UpdateAsync(dto, userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<JournalingDto>>> GetJournalById(int id)
        {
            var journal = await _journalService.GetJournalByIdAsync(id);
            return StatusCode(journal.Success ? 200 : 404, journal);
        }

        [HttpGet("user")] 
        public async Task<ActionResult<GeneralResponse<List<JournalingDto>>>> GetAllUserJournals()
        {
            var userId = GetUserId();
            var journals = await _journalService.GetAllJournalsAsync(userId);
            return StatusCode(journals.Success ? 200 : 404, journals);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(int id)
        {
            var response = await _journalService.DeleteAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }

    }
 
}