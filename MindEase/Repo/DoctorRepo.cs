using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Repo
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;
        private readonly UserManager<GeneralUser> _userManager;

        public DoctorRepo(AppDbContext context, IImageService imageService, UserManager<GeneralUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _userManager = userManager;
        }

        public async Task<GeneralResponse<Doctor>> ProfileAsync(string doctorId)
        {
            try
            {
                var doctor = await _context.Users
    .OfType<Doctor>()
    .FirstOrDefaultAsync(d => d.Id == doctorId);
                if (doctor == null)
                {
                    return new GeneralResponse<Doctor>
                    {
                        Success = false,
                        Message = "Doctor not found"
                    };
                }
                else {
                    return new GeneralResponse<Doctor>
                    {
                        Success = true,
                        Data = doctor
                    };
                }
            }

            catch (Exception ex)
            {

                return new GeneralResponse<Doctor>
                {
                    Success = false,
                    Message = "Failed to retrieve Profile of Doctor.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }

        public async Task<GeneralResponse<Doctor>> UpdateProfileAsync(Doctor doctor, IFormFile profilePicture)
        {
            try
            {
                var existingDoctor = _context.Users.OfType<Doctor>().FirstOrDefault(d => d.Id == doctor.Id);
                if (existingDoctor == null)
                {
                    return new GeneralResponse<Doctor>
                    {
                        Success = false,
                        Message = "Doctor not found"
                    };
                }
                bool ExistEmail = _context.Users.Any(u => u.Email == doctor.Email && u.Id != doctor.Id);
                if (ExistEmail)
                {
                    return new GeneralResponse<Doctor>
                    {
                        Success = false,
                        Message = "Email already exists.",
                        Errors = new Dictionary<string, string[]>
                            {
                                { "Email", new[] { "Email is already in use" } }
                            }
                    };
                }
                else
                {
                    existingDoctor.Email = doctor.Email;
                }
                if (profilePicture == null)
                {

                    return new GeneralResponse<Doctor>
                    {
                        Success = false,
                        Message = "Failed to upload profile picture.",
                        Errors = new Dictionary<string, string[]>
                            {
                                { "ImageUpload", new[] { "Error uploading image" } }
                            }
                    };


                }
                else {
                    var imageUploadResult = await _imageService.UploadImageAsync(profilePicture, "profile-pictures");
                    existingDoctor.Image = imageUploadResult;
                }

                if (!string.IsNullOrEmpty(doctor.FullName))
                {
                    existingDoctor.FullName = doctor.FullName;
                }
                if (!string.IsNullOrEmpty(doctor.Specialization))
                {
                    existingDoctor.Specialization = doctor.Specialization;
                }
                if (!string.IsNullOrEmpty(doctor.Bio))
                {
                    existingDoctor.Bio = doctor.Bio;
                }

                await _context.SaveChangesAsync();
                return new GeneralResponse<Doctor>
                {
                    Success = true,
                    Data = existingDoctor
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse<Doctor>
                {
                    Success = false,
                    Message = "Failed to update Profile of Doctor.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };

            }


        }

        public async Task<GeneralResponse<List<User>>> GetDoctorUsersAsync(string ID)
        {
            if (!_context.Users.OfType<Doctor>().Any(d => d.Id == ID))
            {
                return new GeneralResponse<List<User>>
                {
                    Success = false,
                    Message = "Doctor not found"
                };
            }

            var patients = _context.UserDoctors.Where(d=>d.DoctorId==ID).Select(d=>d.User).ToList();
            if(patients==null || patients.Count == 0)
            {
                return new GeneralResponse<List<User>>
                {
                    Success = false,
                    Message = "No patients found for this doctor"
                };
            }

            return new GeneralResponse<List<User>>
            {
                Success = true,
                Data = patients
            };
        }
    }
}