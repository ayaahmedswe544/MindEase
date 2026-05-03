using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using MindEase.Service;
using System.Security.Claims;

namespace MindEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;
        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] UserChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                var errorResponse = new GeneralResponse<PythonChatResponse>
                {
                    Success = false,
                    Message = "Message cannot be empty."
                };
                return StatusCode(errorResponse.Success ? 200 : 400, errorResponse);
            }
            string UserId = GetUserId();
            var response = await _chatBotService.ProcessUserMessageAsync(UserId, request.Message);
            return StatusCode(response.Success ? 200 : 400, response);
        }



        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            string UserId = GetUserId();
            var response = await _chatBotService.GetHistoryAsync(UserId);

            return StatusCode(response.Success ? 200 : 400, response);
        }
    }
}
