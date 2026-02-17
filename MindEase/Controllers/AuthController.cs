using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Auth;
using MindEase.IService;
using MindEase.Models.Response;

namespace MindEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
