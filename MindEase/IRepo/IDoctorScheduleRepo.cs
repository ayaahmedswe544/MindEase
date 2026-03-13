using MindEase.DTOs.DoctorSchedule;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IDoctorScheduleRepo
    {

        Task<GeneralResponse<DoctorWeeklySchedule>> CreateAsync(DoctorWeeklySchedule doctorSchedule);
        Task<GeneralResponse<DoctorWeeklySchedule>> UpdateAsync(DoctorWeeklySchedule doctorSchdule);
        Task<GeneralResponse<bool>> DeleteAsync(int id);
        Task<GeneralResponse<List<DoctorWeeklySchedule>>> GetByDoctorIdAsync(string doctorId);
    }
}
