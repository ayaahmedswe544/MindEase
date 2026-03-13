using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.DoctorSchedule;
using MindEase.DTOs.Memory;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System.Security.Claims;

namespace MindEase.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _service;
        public DoctorScheduleController(IDoctorScheduleService service)
        {
            _service = service;
        }
        private string GetDoctorId()
        {
            string DoctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return DoctorId;
        }
         
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<DoctorScheduleDto>>> CreateSchedule([FromForm] CreateDoctorScheduleDto doctorSchdeduleDto)
        {
            string DoctorId = GetDoctorId();
            var response = await _service.CreateDoctorScheduleAsync(doctorSchdeduleDto, DoctorId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }

        [HttpPost("update")]
        public async Task<ActionResult<GeneralResponse<DoctorScheduleDto>>> UpdateSchedule([FromForm] UpdateDoctorScheduleDto doctorSchdeduleDto)
        {
            string DoctorId = GetDoctorId();
            var response = await _service.UpdateAsync(doctorSchdeduleDto, DoctorId);
            return StatusCode(response.Success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }

        [HttpGet("doctorSchedules")]
        public async Task<ActionResult<GeneralResponse<List<DoctorScheduleDto>>>> GetAllDoctorSchedules(string DoctorId)
        {
            var response = await _service.GetByDoctorIdAsync(DoctorId);
            return StatusCode(response.Success ? 200 : 404, response);
        }

    }
}


