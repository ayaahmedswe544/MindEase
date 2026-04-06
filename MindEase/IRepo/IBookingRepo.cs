using MindEase.DTOs.Doctor;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IBookingRepo
    {
        Task<GeneralResponse<Booking>> CreateAsync(Booking booking);
        Task<GeneralResponse<Booking>> ChangeStatusAsync(int Id, BookingStatus status);
        Task<GeneralResponse<List<Booking>>> GetByUserIdAsync(string userId, bool isDoctor);

    }
}
