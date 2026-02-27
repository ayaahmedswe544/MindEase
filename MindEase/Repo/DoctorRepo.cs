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

        public DoctorRepo(AppDbContext context, IImageService imageService, UserManager<GeneralUser > userManager)
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
    } }
