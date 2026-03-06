using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.MoodEntry;
using MindEase.IService;
using MindEase.Models.Response;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodEntryController : ControllerBase
    {
        private readonly IMoodEntryService _service;
        public MoodEntryController(IMoodEntryService service)
        {
            _service = service;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost("create")]
        public async Task<ActionResult<GeneralResponse<MoodEntryDto>>> CreateMoodEntry([FromBody] CreateMoodEntryDto moodEntryDto)
        {
            var userId = GetUserId();
            var response = await _service.CreateAsync(moodEntryDto, userId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }

        [HttpPost("delete")]
        public async Task<ActionResult<GeneralResponse<string>>> DeleteMoodEntry([FromBody] int id)
        {
            var userId = GetUserId();
            var response = await _service.DeleteAsync(id, userId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);

        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<GeneralResponse<MoodEntryDto>>> GetMoodEntry(int id)
        {
            var userId = GetUserId();
            var response = await _service.GetByIdAsync(id, userId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<GeneralResponse<List<MoodEntryDto>>>> GetAllMoodEntries()
        {
            var userId = GetUserId();
            var response = await _service.GetAllAsync(userId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }
        [HttpPost("update")]
        public async Task<ActionResult<GeneralResponse<MoodEntryDto>>> UpdateMoodEntry(updateMoodEntryDto dto) { 
        var userId = GetUserId();
        var response = await _service.UpdateAsync(dto, userId);
            return StatusCode(response.Success? StatusCodes.Status200OK: StatusCodes.Status400BadRequest,response);
        
        }
    }
}
