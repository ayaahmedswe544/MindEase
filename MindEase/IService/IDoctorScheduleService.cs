using MindEase.DTOs.DoctorSchedule;
using MindEase.Models.Response;

namespace MindEase.IService
{
    public interface IDoctorScheduleService
    {
        Task<GeneralResponse<DoctorScheduleDto>> CreateDoctorScheduleAsync(CreateDoctorScheduleDto doctorScheduleSchdeduleDto, string doctorId);
        Task<GeneralResponse<DoctorScheduleDto>> UpdateAsync(UpdateDoctorScheduleDto dto, string doctorId);
        Task<GeneralResponse<bool>> DeleteAsync(int id);
        Task<GeneralResponse<List<DoctorScheduleDto>>> GetByDoctorIdAsync(string DoctorId);
    }
}
