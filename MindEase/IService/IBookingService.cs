using MindEase.DTOs.Booking;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IBookingService
    {
        Task<GeneralResponse<BookingDto>> CreateAsync(CreateBookingDto input, string userId);
        Task<GeneralResponse<BookingDto>> ChangeStatusAsync(int Id, BookingStatus status);
        Task<GeneralResponse<List<BookingDto>>> GetByUserIdAsync(string userId, bool isDoctor);

    }
}
