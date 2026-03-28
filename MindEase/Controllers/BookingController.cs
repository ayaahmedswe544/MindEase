using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindEase.DTOs.Booking;
using MindEase.DTOs.Doctor;
using MindEase.DTOs.Memory;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System.Security.Claims;

namespace MindEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingController(IBookingService service)
        {
            _service = service;
        }
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
         

        [HttpPost("create")]
        public async Task<ActionResult<GeneralResponse<MemoryResponseDto>>> Create([FromForm] CreateBookingDto dto)
        {
            var userId = GetUserId();
            var response = await _service.CreateAsync(dto, userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

       
    }
}


