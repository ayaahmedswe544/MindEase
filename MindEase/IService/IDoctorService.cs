using MindEase.DTOs.Doctor;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IDoctorService
    {
        Task<GeneralResponse<DoctorDto>> ProfileAsync(string DoctorId);

    }
}
