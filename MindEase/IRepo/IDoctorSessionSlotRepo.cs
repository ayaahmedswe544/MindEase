using MindEase.DTOs.DoctorSchedule;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.IRepo
{
    public interface IDoctorSessionSlotRepo
    {
        Task<GeneralResponse<List<DoctorSessionSlot>>> ManageSlotsAsync(List<DoctorSessionSlot> doctorSessionSlots, bool isUpdate);
        Task<GeneralResponse<List<DoctorSessionSlot>>> TriggerUpdateSlotsForDoctor(List<DoctorSessionSlot> doctorSessionSlots);
       Task<GeneralResponse<List<DoctorSessionSlot>>> GetSlotsByDoctorIdAsync(string doctorId);
        Task<GeneralResponse<DoctorSessionSlot>> ToggleSlotBookedState(int? slotId);
        Task<GeneralResponse<DoctorSessionSlot>> DeleteSlotsByDoctorId(string doctorId);
    }
}
