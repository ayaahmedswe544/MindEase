using MindEase.DTOs.Doctor;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IClientRepo
    {
        Task<GeneralResponse<User>> ClientProfileAsync(string DoctorId);
        Task<GeneralResponse<User>> UpdateProfileAsync(User client, IFormFile profilePicture);


    }
}
