using MindEase.DTOs.Doctor;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IDoctorService
    {
        Task<GeneralResponse<DoctorDto>> ProfileAsync(string DoctorId);
        Task<GeneralResponse<DoctorDto>> UpdateProfileAsync(updateDoctorDto doctor, string ID);
    }
}
