using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Client;
using MindEase.DTOs.Doctor;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        private string GetClientId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("profile")]
        public async Task<ActionResult<GeneralResponse<ClientDto>>> GetClientProfile()
        {
            string ClientId = GetClientId();
            var response = await _clientService.GetProfileAsync(ClientId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);

        }
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<ClientDto>>> UpdateClientProfile([FromForm] updateClientDto clientDto)
        {
            string ClientId = GetClientId();
            var response = await _clientService.UpdateProfileAsync(clientDto, ClientId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }

    }

}