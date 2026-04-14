using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Auth;
using MindEase.IService;
using MindEase.Models.Response;
using System.Security.Claims;

namespace MindEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IDoctorScheduleService _doctorScheduleService;

        public AuthController(IAuthService authService, IDoctorScheduleService doctorScheduleService)
        {
            _authService = authService;
            _doctorScheduleService = doctorScheduleService;
        }
        private string GetId() { 
        string id= User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    return id;  
        }
        [HttpPost("register-user")]
        public async Task<ActionResult<GeneralResponse<AuthResponse>>> RegisterUser(RegisterUserDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpPost("register-doctor")]
        public async Task<ActionResult<GeneralResponse<AuthResponse>>> RegisterDoctor(RegisterDoctorDto dto)
        {
            var result = await _authService.RegisterDoctorAsync(dto);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpPost("login-user")]
        public async Task<ActionResult<GeneralResponse<AuthResponse>>> LoginUser(LoginDto dto)
        {
            var result = await _authService.LoginUserAsync(dto);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpPost("login-doctor")]
        public async Task<ActionResult<GeneralResponse<AuthResponse>>> LoginDoctor(LoginDto dto)
        {
            var result = await _authService.LoginDoctorAsync(dto);
            if (result.Success)
            {
                string doctorId =GetId();
                await _doctorScheduleService.TriggerDoctorSlotsStatus(doctorId);
            }
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
