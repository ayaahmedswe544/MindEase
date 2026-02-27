using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Doctor;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models.Response;
using System.Security.Claims;

namespace MindEase.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }
        private string GetDoctorId()
        {
            string DoctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return DoctorId;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<GeneralResponse<DoctorDto>>> Profile()
        {
            string DoctorId = GetDoctorId();
            var response = await _service.ProfileAsync(DoctorId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);


        }
    }
}


