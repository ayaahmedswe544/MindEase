using MindEase.DTOs.Auth;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IAuthService
    {
        Task<GeneralResponse<AuthResponse>> RegisterUserAsync(RegisterUserDto dto);
        Task<GeneralResponse<AuthResponse>> RegisterDoctorAsync(RegisterDoctorDto dto);
        Task<GeneralResponse<AuthResponse>> LoginUserAsync(LoginDto dto);
        Task<GeneralResponse<AuthResponse>> LoginDoctorAsync(LoginDto dto);
    }
}
