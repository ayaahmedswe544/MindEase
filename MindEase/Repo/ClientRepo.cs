using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Repo
{
    public class ClientRepo : IClientRepo
    {
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;
        private readonly UserManager<GeneralUser> _userManager;

        public ClientRepo(AppDbContext context, IImageService imageService, UserManager<GeneralUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _userManager = userManager;
        }

        public async Task<GeneralResponse<User>> ClientProfileAsync(string clientId)
        {
            try
            {
                var client = await _context.Users
    .OfType<User>()
    .FirstOrDefaultAsync(d => d.Id == clientId);
                if (client == null)
                {
                    return new GeneralResponse<User>
                    {
                        Success = false,
                        Message = "Client not found"
                    };
                }
                else {
                    return new GeneralResponse<User>
                    {
                        Success = true,
                        Data = client
                    };
                }
            }

            catch (Exception ex)
            {

                return new GeneralResponse<User>
                {
                    Success = false,
                    Message = "Failed to retrieve Profile of Client.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }

        public async Task<GeneralResponse<User>> UpdateProfileAsync(User client, IFormFile profilePicture)
        {
            try
            {
                var existingClient = _context.Users.OfType<User>().FirstOrDefault(d => d.Id == client.Id);
                if (existingClient == null)
                {
                    return new GeneralResponse<User>
                    {
                        Success = false,
                        Message = "Client not found"
                    };
                }
                bool ExistEmail = _context.Users.Any(u => u.Email == client.Email && u.Id != client.Id);
                if (ExistEmail)
                {
                    return new GeneralResponse<User>
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
                    existingClient.Email = client.Email;
                }
                if (profilePicture != null)
                {

                    var imageUploadResult = await _imageService.UploadImageAsync(profilePicture, "profile-pictures");
                    existingClient.Image = imageUploadResult;
                }
               

                if (!string.IsNullOrEmpty(client.FullName))
                {
                    existingClient.FullName = client.FullName;
                }
             

                await _context.SaveChangesAsync();
                return new GeneralResponse<User>
                {
                    Success = true,
                    Data = existingClient
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse<User>
                {
                    Success = false,
                    Message = "Failed to update Profile of Client.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };

            }


        }
         
    }
}