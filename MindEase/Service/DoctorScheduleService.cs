using Azure;
using MindEase.DTOs.DoctorSchedule;
using MindEase.DTOs.Memory;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MindEase.Service
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepo _repo;
        private readonly IDoctorSessionSlotRepo _doctorSessionSlotRepo;
        public DoctorScheduleService(IDoctorScheduleRepo repo, IDoctorSessionSlotRepo doctorSessionSlotRepo)
        {
            _repo = repo;
            _doctorSessionSlotRepo = doctorSessionSlotRepo;
        }
        public async Task<GeneralResponse<DoctorScheduleDto>> CreateDoctorScheduleAsync(CreateDoctorScheduleDto doctorScheduleSchdeduleDto, string doctorId)
        {
            if (doctorScheduleSchdeduleDto == null)
            {
                return new GeneralResponse<DoctorScheduleDto>
                {
                    Success = false,
                    Message = "Doctor Schedule data cannot be null.",
                    Data = null,
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Doctor", new[] { "Doctor Schedule data is required." } }
                    }
                };
            }
            var doctorSchedule = new DoctorWeeklySchedule
            {
                DoctorId = doctorId,
                DayOfWeek = doctorScheduleSchdeduleDto.DayOfWeek,
                StartTime = doctorScheduleSchdeduleDto.StartTime,
                EndTime = doctorScheduleSchdeduleDto.EndTime,
                IsActive = true
            };
            var response = await _repo.CreateAsync(doctorSchedule);
            await BuildSlotsForDay(response.Data, false);
            if (!response.Success)
                return new GeneralResponse<DoctorScheduleDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };



            var dtoResponse = new DoctorScheduleDto
            {
                Id = response.Data!.Id,
                DayOfWeek = response.Data.DayOfWeek,
                StartTime = response.Data.StartTime,
                EndTime = response.Data.EndTime,
                IsActive = response.Data.IsActive
            };



            return new GeneralResponse<DoctorScheduleDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };

        }



        public async Task<GeneralResponse<DoctorScheduleDto>> UpdateAsync(UpdateDoctorScheduleDto dto, string doctorId)
        {

            var doctorWeeklySchedule = new DoctorWeeklySchedule
            {
                Id = dto.Id,
                DoctorId = doctorId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsActive = dto.IsActive
            };


            var response = await _repo.UpdateAsync(doctorWeeklySchedule);
            await BuildSlotsForDay(response.Data, true);

            if (!response.Success)
                return new GeneralResponse<DoctorScheduleDto>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoResponse = new DoctorScheduleDto
            {
                Id = response.Data!.Id,
                DoctorId = response.Data.DoctorId,
                DayOfWeek = response.Data.DayOfWeek,
                StartTime = response.Data.StartTime,
                EndTime = response.Data.EndTime,
                IsActive = response.Data.IsActive
            };

            return new GeneralResponse<DoctorScheduleDto>
            {
                Success = true,
                Data = dtoResponse,
                Message = response.Message
            };
        }


        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<GeneralResponse<List<DoctorScheduleDto>>> GetByDoctorIdAsync(string DoctorId)
        {
            var response = await _repo.GetByDoctorIdAsync(DoctorId);

            if (!response.Success)
                return new GeneralResponse<List<DoctorScheduleDto>>
                {
                    Success = false,
                    Message = response.Message,
                    Errors = response.Errors
                };

            var dtoList = response.Data!.Select(m => new DoctorScheduleDto
            {
                Id = m.Id,
                DoctorId = m.DoctorId,
                DayOfWeek = m.DayOfWeek,
                StartTime = m.StartTime,
                EndTime = m.EndTime,
                IsActive = m.IsActive
            }).ToList();

            return new GeneralResponse<List<DoctorScheduleDto>>
            {
                Success = true,
                Data = dtoList,
                Message = response.Message
            };
        }

        private async Task<GeneralResponse<List<DoctorSessionSlot>>> BuildSlotsForDay(DoctorWeeklySchedule doctorWeeklySchedule, bool isUpdate)
        {
            try
            {
                int examinationPeriodMinutes = 60;
                var doctorSlots = new List<DoctorSessionSlot>();
                var slots = new List<TimeSpan>();

                if (examinationPeriodMinutes <= 0)
                {
                    return new GeneralResponse<List<DoctorSessionSlot>>
                    {
                        Success = false,
                        Message = "Failed to Create Day Slots For Doctor.",
                    };
                }

                var period = TimeSpan.FromMinutes(examinationPeriodMinutes);
                var dayStart = doctorWeeklySchedule.StartTime;  // بداية الدوام (TimeSpan)
                var dayEnd = doctorWeeklySchedule.EndTime;      // نهاية الدوام (TimeSpan)

                // إذا TimeTo أقل من TimeFrom (خطأ إعداد)
                if (dayEnd <= dayStart)
                {
                    return new GeneralResponse<List<DoctorSessionSlot>>
                    {
                        Success = false,
                        Message = "Failed to Create Day Slots For Doctor.",
                    };
                }

                var slotStart = dayStart;

                while (slotStart + period <= dayEnd)
                {
                    doctorSlots.Add(new DoctorSessionSlot
                    {
                        DoctorWeeklyScheduleId = doctorWeeklySchedule.Id,
                        StartTime = slotStart,
                        EndTime = slotStart + period,
                        IsBooked = false,
                        DoctorWeeklySchedule = null,
                        Booking = null
                    });
                    slotStart = slotStart + period;
                }

                var doctorSessionSlots = await _doctorSessionSlotRepo.ManageSlotsAsync(doctorSlots, isUpdate);

                return new GeneralResponse<List<DoctorSessionSlot>>
                {
                    Success = true,
                    Message = "Slots Created For Doctor Successfully.",
                };


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task<GeneralResponse<List<DoctorSessionSlot>>> TriggerDoctorSlotsStatus(string DoctorId)
        {
            try
            {
                int examinationPeriodMinutes = 60;
                var doctorSlots = new List<DoctorSessionSlot>();
                var slots = new List<TimeSpan>();

                if (examinationPeriodMinutes <= 0)
                {
                    return new GeneralResponse<List<DoctorSessionSlot>>
                    {
                        Success = false,
                        Message = "Failed to Create Day Slots For Doctor.",
                    };
                }

                List<DoctorWeeklySchedule> doctorWeeklySchedules = new List<DoctorWeeklySchedule>();
                var result = await _repo.GetByDoctorIdAsync(DoctorId);

                doctorWeeklySchedules = result.Data;
                var period = TimeSpan.FromMinutes(examinationPeriodMinutes);

                TimeSpan dayStart = new TimeSpan { };  // بداية الدوام (TimeSpan)
                TimeSpan dayEnd = new TimeSpan { };      // نهاية الدوام (TimeSpan)


                if (doctorWeeklySchedules.Count > 0)
                {

                    foreach (var item in doctorWeeklySchedules)
                    {
                        dayStart = item.StartTime;
                        dayEnd = item.EndTime;


                        // إذا TimeTo أقل من TimeFrom (خطأ إعداد)
                        if (dayEnd <= dayStart)
                        {
                            return new GeneralResponse<List<DoctorSessionSlot>>
                            {
                                Success = false,
                                Message = "Failed to Create Day Slots For Doctor.",
                            };
                        }
                        var slotStart = dayStart;

                        while (slotStart + period <= dayEnd)
                        {
                            doctorSlots.Add(new DoctorSessionSlot
                            {
                                DoctorWeeklyScheduleId = item.Id,
                                StartTime = slotStart,
                                EndTime = slotStart + period,
                                IsBooked = false,
                                DoctorWeeklySchedule = null,
                                Booking = null
                            });
                            slotStart = slotStart + period;
                        }

                        var doctorSessionSlots = await _doctorSessionSlotRepo.TriggerUpdateSlotsForDoctor(doctorSlots);

                        return new GeneralResponse<List<DoctorSessionSlot>>
                        {
                            Success = true,
                            Message = "Slots Created For Doctor Successfully.",
                        };

                    }
                }
                else
                {
                    return new GeneralResponse<List<DoctorSessionSlot>>
                    {
                        Success = false,
                        Message = "Failed to Create Day Slots For Doctor.",
                    };
                }

                return new GeneralResponse<List<DoctorSessionSlot>>
                {
                    Success = false,
                    Message = "Failed to Create Day Slots For Doctor.",
                };

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}