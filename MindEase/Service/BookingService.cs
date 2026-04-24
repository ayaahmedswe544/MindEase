using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MindEase.DTOs.Booking;
using MindEase.DTOs.Journaling;
using MindEase.DTOs.Memory;
using MindEase.Hubs;
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
        private readonly IHubContext<ChatHub> _hubContext;

        public BookingService(IBookingRepo bookingRepo, UserManager<GeneralUser> userManager , IDoctorSessionSlotRepo doctorSessionSlotRepo,IHubContext<ChatHub>hubContext)
        {
            _bookingRepo = bookingRepo;
            _doctorSessionSlotRepo = doctorSessionSlotRepo;
            _userManager = userManager;
            _hubContext = hubContext;

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
            var docotr =await _userManager.FindByIdAsync(input.DoctorId);
            var DoctorName =docotr.FullName ;
            var dtoResponse = new BookingDto
            {
                Id=response.Data.Id,
                DoctorId = response.Data!.DoctorId,
                BookingStatus = response.Data.BookingStatus,
                ConfirmedAt = response.Data.ConfirmedAt,
                DoctorSessionSlotId = response.Data.DoctorSessionSlotId != 0 ? response.Data.DoctorSessionSlotId: null,
                UserId = response.Data.UserId,
                DoctorName=DoctorName
                

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
            {
                return new GeneralResponse<BookingDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };
            }
            var dtoResponse = new BookingDto
            {
                DoctorId = response.Data!.DoctorId,
                BookingStatus = response.Data.BookingStatus,
                ConfirmedAt = response.Data.ConfirmedAt,
                DoctorSessionSlotId = response.Data.DoctorSessionSlotId,
                UserId = response.Data.UserId,
                DoctorName=response.Data.Doctor.FullName

            };
            string message;
            switch (response.Data.BookingStatus)
            {
                case BookingStatus.Confirmed:
                    message = "Your booking was accepted ✅";
                    break;

                case BookingStatus.Rejected:
                    message = "Your booking was rejected ❌";
                    break;

                case BookingStatus.Cancelled:
                    message = "Your booking was cancelled ⚠️";
                    break;

                default:
                    message = "Booking updated";
                    break;
            }

            await _hubContext.Clients.User(response.Data.UserId)
     .SendAsync("BookingStatusChanged", new
     {
         Message=message,
         Status = response.Data.BookingStatus.ToString(),
         Doctorname=response.Data.Doctor.FullName ?? "Unknown"
     });
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
                Id=m.Id,
                DoctorId = m.DoctorId,
                BookingStatus = m.BookingStatus,
                ConfirmedAt = m.ConfirmedAt,
                DoctorSessionSlotId = m.DoctorSessionSlotId,
                UserId = m.UserId,
               DoctorName=m.Doctor.FullName,
               RequestedAt=m.RequestedAt
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