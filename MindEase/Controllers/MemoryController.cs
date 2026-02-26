using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MindEase.DTOs.Memory;
using MindEase.IService;
using MindEase.Service;
using MindEase.Models.Response;

namespace MindEase.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryService _service;

        public MemoryController(IMemoryService service)
        {
            _service = service;
        }


        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost("create")]
        public async Task<ActionResult<GeneralResponse<MemoryResponseDto>>> Create([FromForm] CreateMemoryDto dto)
        {
            var userId = GetUserId();
            var response = await _service.CreateAsync(dto, userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<MemoryResponseDto>>> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }

        [HttpGet("user")]
        public async Task<ActionResult<GeneralResponse<List<MemoryResponseDto>>>> GetUserMemories()
        {
            var userId = GetUserId();
            var response = await _service.GetByUserIdAsync(userId);
            return StatusCode(response.Success ? 200 : 404, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }
        [HttpPost("update")]
        public async Task<ActionResult<GeneralResponse<MemoryResponseDto>>> Update([FromForm] UpdateMemoryDto dto)
        {
            var userId = GetUserId();
            var response = await _service.UpdateAsync(dto, userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }
    }
}