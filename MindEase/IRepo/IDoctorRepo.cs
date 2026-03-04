using MindEase.DTOs.Doctor;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IDoctorRepo
    {
        Task<GeneralResponse<Doctor>> ProfileAsync(string DoctorId);

        Task<GeneralResponse<Doctor>> UpdateProfileAsync(Doctor doctor, IFormFile profilePicture);
        Task<GeneralResponse<List<User>>> GetDoctorUsersAsync(string ID);

    }
}
