using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System.Numerics;

namespace MindEase.Repo
{


        public class AuthRepository : IAuthRepository
        {
            private readonly UserManager<User> _userManager;
            private readonly UserManager<Doctor> _doctorManager;
            private readonly IJwtService _jwtService;

            public AuthRepository(UserManager<User> userManager,
                                  UserManager<Doctor> doctorManager,
                                  IJwtService jwtService)
            {
                _userManager = userManager;
                _doctorManager = doctorManager;
                _jwtService = jwtService;
            }
            public async Task<GeneralResponse<AuthResponse>> RegisterUserAsync(User user, string password)
            {
                var response = new GeneralResponse<AuthResponse>();
                try
                {
                var existingEmail = await _userManager.FindByEmailAsync(user.Email);
                if (existingEmail != null)
                {
                    response.Success = false;
                    response.Message = "Email is already registered.";
                    response.Errors = new Dictionary<string, string[]>
                    {
                        { "Email", new[] { "Email is already registered." } }
                    };
                    return response;
                }

                var result = await _userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        response.Success = false;
                        response.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                        response.Errors = result.Errors
                            .GroupBy(e => e.Code)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());
                    return response;
                    }

                    response.Success = true;
                   var token =  _jwtService.GenerateToken(user.Id.ToString(), user.Email, "User");
                    response.Data = new AuthResponse { Token = token };
                    response.Message = "User registered successfully";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                response.Errors = new Dictionary<string, string[]>
                    {
                        { "Exception", new[] { ex.Message } }
                    };
            }
                return response;
            }

        public async Task<GeneralResponse<AuthResponse>> RegisterDoctorAsync(Doctor doctor, string password)
        {
            var response = new GeneralResponse<AuthResponse>();

            try
            {
                var existingEmail = await _doctorManager.FindByEmailAsync(doctor.Email);
                if (existingEmail != null)
                {
                    response.Success = false;
                    response.Message = "Email is already registered.";
                    response.Errors = new Dictionary<string, string[]>
                    {
                        { "Email", new[] { "Email is already registered." } }
                    };
                    return response;
                }

                var existingLicense = await _doctorManager.Users
                    .FirstOrDefaultAsync(d => d.LicenseNumber == doctor.LicenseNumber);

                if (existingLicense != null)
                {
                    response.Success = false;
                    response.Message = "License number is already registered.";
                    response.Errors = new Dictionary<string, string[]>
                    {
                        { "LicenseNumber", new[] { "License number is already registered." } }
                    };
                    return response;
                }

                var result = await _doctorManager.CreateAsync(doctor, password);

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = string.Join("; ", result.Errors.Select(e => e.Description));
                    response.Errors = result.Errors
                        .GroupBy(e => e.Code)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());
                    return response;
                }

                var token = _jwtService.GenerateToken(doctor.Id.ToString(), doctor.Email, "Doctor");

                response.Success = true;
                response.Data = new AuthResponse { Token = token };
                response.Message = "Doctor registered successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors = new Dictionary<string, string[]>
                {
                    { "Exception", new[] { ex.Message } }
                };
                return response;
            }
        }

        public async Task<GeneralResponse<AuthResponse>> LoginUserAsync(string email, string password)
            {
                var response = new GeneralResponse<AuthResponse>();
                try
                {
                    var user = await _userManager.FindByNameAsync(email);
                    if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                    {
                        response.Success = false;
                        response.Message = "Invalid credentials";
                    response.Errors = new Dictionary<string, string[]>
                        {
                            { "Credentials", new[] { "Invalid email or password." } }
                        };
                    return response;
                    }

                    var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, "User");
                    response.Data = new AuthResponse { Token = token };
                    response.Message = "Login successful";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    response.Errors = new Dictionary<string, string[]>
                    {
                        { "Exception", new[] { ex.Message } }
                    };
                }
                return response;
            }
            public async Task<GeneralResponse<AuthResponse>> LoginDoctorAsync(string email, string password)
            {
                var response = new GeneralResponse<AuthResponse>();
                try
                {
                    var doctor = await _doctorManager.FindByNameAsync(email);
                if (doctor == null || !await _doctorManager.CheckPasswordAsync(doctor, password))
                    {
                        response.Success = false;
                        response.Message = "Invalid credentials";
                    response.Errors = new Dictionary<string, string[]>
                        {
                            { "Credentials", new[] { "Invalid email or password." } }
                        };
                    return response;
                    }

                    var token = _jwtService.GenerateToken(doctor.Id.ToString(), doctor.Email, "Doctor");
                    response.Data = new AuthResponse { Token = token };
                    response.Message = "Login successful";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    response.Errors = new Dictionary<string, string[]>
                    {
                        { "Exception", new[] { ex.Message } }
                    };
            }
                return response;
            }
        }
    }



