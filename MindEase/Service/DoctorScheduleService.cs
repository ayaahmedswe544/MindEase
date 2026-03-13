using Azure;
using MindEase.DTOs.DoctorSchedule;
using MindEase.DTOs.Memory;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;

namespace MindEase.Service
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly IDoctorScheduleRepo _repo;
        public DoctorScheduleService(IDoctorScheduleRepo repo)
        {
            _repo = repo;
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

    }
}