using MindEase.DTOs.Auth;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Service
{

        public class AuthService : IAuthService
        {
            private readonly IAuthRepository _repo;

            public AuthService(IAuthRepository repo)
            {
                _repo = repo;
            }

            public async Task<GeneralResponse<AuthResponse>> RegisterUserAsync(RegisterUserDto dto)
            {
                var user = new User
                {
                    Email = dto.Email,
                    UserName = dto.Email,
                    FullName = dto.FullName,
                    Age = dto.Age,
                    Gender = dto.Gender
                };

                var result = await _repo.RegisterUserAsync(user, dto.Password);
                return result;
            }

            public async Task<GeneralResponse<AuthResponse>> RegisterDoctorAsync(RegisterDoctorDto dto)
            {
                var doctor = new Doctor
                {
                    Email = dto.Email,
                    UserName = dto.Email,
                    FullName = dto.FullName,
                    Age = dto.Age,
                    Gender = dto.Gender,
                    Specialization = dto.Specialization,
                    LicenseNumber = dto.LicenseNumber,
                    Bio = dto.Bio
                };

                var result = await _repo.RegisterDoctorAsync(doctor, dto.Password);
                return result;
            }

            public async Task<GeneralResponse<AuthResponse>> LoginUserAsync(LoginDto dto)
            {
                return await _repo.LoginUserAsync(dto.Email, dto.Password);
            }

            public async Task<GeneralResponse<AuthResponse>> LoginDoctorAsync(LoginDto dto)
            {
                return await _repo.LoginDoctorAsync(dto.Email, dto.Password);
            }
        }
    }

