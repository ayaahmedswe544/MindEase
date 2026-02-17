using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IAuthRepository
    {
        Task<GeneralResponse<AuthResponse>> RegisterUserAsync(User user, string password);
        Task<GeneralResponse<AuthResponse>> RegisterDoctorAsync(Doctor doctor, string password);
        Task<GeneralResponse<AuthResponse>> LoginUserAsync(string email, string password);
        Task<GeneralResponse<AuthResponse>> LoginDoctorAsync(string email, string password);
    }
}
