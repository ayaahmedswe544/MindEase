using MindEase.DTOs.Doctor;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models.Response;

namespace MindEase.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepo _repo;
        public DoctorService(IDoctorRepo repo)
        {
            _repo = repo;
        }
        public async Task<GeneralResponse<DoctorDto>> ProfileAsync(string DoctorId)
        {
            if (string.IsNullOrWhiteSpace(DoctorId))
            {
                return new GeneralResponse<DoctorDto>
                {
                    Success = false,
                    Message = "Doctor ID cannot be null or empty.",
                    Data = null,
                    Errors = new Dictionary<string, string[]>
                    {
                        { "DoctorId", new[] { "Doctor ID is required." } }
                    }
                };
            }
            var response = await _repo.ProfileAsync(DoctorId);
            if (response.Success)
            {
                var doctorDto = new DoctorDto
                {
                    FullName = response.Data.FullName,
                    Email = response.Data.Email,
                    Gender = response.Data.Gender,
                    Age = response.Data.Age,
                    Specialization = response.Data.Specialization,
                    Bio = response.Data.Bio
                };

                return new GeneralResponse<DoctorDto>
                {
                    Success = true,
                    Message = "Doctor profile retrieved successfully.",
                    Data = doctorDto,
                    Errors = null

                };

            }
            return new GeneralResponse<DoctorDto> { 
            Success = false,
            Message = response.Message,
            Errors = response.Errors
            };
        }
    }
}