using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IDoctorRepo
    {
        Task<GeneralResponse<Doctor>> ProfileAsync(string DoctorId);
       


    }
}
