using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Doctor;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System.Security.Claims;

namespace MindEase.Controllers
{
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
        [Authorize(Roles = "Doctor")]

        [HttpGet("profile")]
        public async Task<ActionResult<GeneralResponse<DoctorDto>>> Profile()
        {
            string DoctorId = GetDoctorId();
            var response = await _service.ProfileAsync(DoctorId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);


        }
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<DoctorDto>>> UpdateProfile([FromForm] updateDoctorDto doctorDto)
        {
            string DoctorId = GetDoctorId();
            var response = await _service.UpdateProfileAsync(doctorDto, DoctorId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }
        [Authorize(Roles = "Doctor")]
        [HttpGet("patients")]
        public async Task<ActionResult<GeneralResponse<List<DoctorUsers>>>> GetDoctorUsers()
        {
            string DoctorId = GetDoctorId();
            var response = await _service.GetDoctorUsersAsync(DoctorId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }
        [Authorize(Roles = "Doctor,User")]
        [HttpGet("all")]
        public async Task<ActionResult<GeneralResponse<DoctorsPaginationDto>>> GetAllDoctors([FromQuery] int pageSize=10, [FromQuery] int pageNumber=1, [FromQuery] string? searchTerm=null)
        {
            var response = await _service.GetAllDoctorsAsync(pageSize, pageNumber, searchTerm);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }
    }
}


