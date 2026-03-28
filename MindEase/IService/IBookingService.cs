using MindEase.DTOs.Booking;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IBookingService
    {
        Task<GeneralResponse<BookingDto>> CreateAsync(CreateBookingDto input, string userId);

    }
}
