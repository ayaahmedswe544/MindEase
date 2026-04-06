using Azure;
using Microsoft.AspNetCore.Identity;
using MindEase.DTOs.Booking;
using MindEase.DTOs.Journaling;
using MindEase.DTOs.Memory;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using MindEase.Repo;

namespace MindEase.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly IDoctorSessionSlotRepo _doctorSessionSlotRepo;
        private readonly UserManager<GeneralUser> _userManager;

        public BookingService(IBookingRepo bookingRepo, UserManager<GeneralUser> userManager , IDoctorSessionSlotRepo doctorSessionSlotRepo)
        {
            _bookingRepo = bookingRepo;
            _doctorSessionSlotRepo = doctorSessionSlotRepo;
            _userManager = userManager;
        }

        public async Task<GeneralResponse<BookingDto>> CreateAsync(CreateBookingDto input, string userId)
        {


            var booking = new Booking
            {
                DoctorSessionSlotId = input.DoctorSessionSlotId,
                UserId = userId,
                RequestedAt = DateTime.Now,
                BookingStatus = BookingStatus.Pending,
                DoctorId = input.DoctorId,
            };
            var response = await _bookingRepo.CreateAsync(booking);

            if (!response.Success)
                return new GeneralResponse<BookingDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new BookingDto
            {
                DoctorId = response.Data!.DoctorId,
                BookingStatus = response.Data.BookingStatus,
                ConfirmedAt = response.Data.ConfirmedAt,
                DoctorSessionSlotId = response.Data.DoctorSessionSlotId,
                UserId = response.Data.UserId

            };


            return new GeneralResponse<BookingDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
        }


        public async Task<GeneralResponse<BookingDto>> ChangeStatusAsync(int Id , BookingStatus status)
        { 

            var response = await _bookingRepo.ChangeStatusAsync( Id, status);

            if (!response.Success)
                return new GeneralResponse<BookingDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new BookingDto
            {
                DoctorId = response.Data!.DoctorId,
                BookingStatus = response.Data.BookingStatus,
                ConfirmedAt = response.Data.ConfirmedAt,
                DoctorSessionSlotId = response.Data.DoctorSessionSlotId,
                UserId = response.Data.UserId

            };

            return new GeneralResponse<BookingDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
        }


        public async Task<GeneralResponse<List<BookingDto>>> GetByUserIdAsync(string userId , bool isDoctor)
        {
            var response = await _bookingRepo.GetByUserIdAsync(userId , isDoctor);

            if (!response.Success)
                return new GeneralResponse<List<BookingDto>>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoList = response.Data!.Select(m => new BookingDto
            {
                DoctorId = m.DoctorId,
                BookingStatus = m.BookingStatus,
                ConfirmedAt = m.ConfirmedAt,
                DoctorSessionSlotId = m.DoctorSessionSlotId,
                UserId = m.UserId
            }).ToList();

            return new GeneralResponse<List<BookingDto>>
            {
                Success = true,
                Data = dtoList,
                Message = response.Message
            };
        }

    }
}