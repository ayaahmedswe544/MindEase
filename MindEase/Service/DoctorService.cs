using MindEase.DTOs.Doctor;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
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
                    Id = response.Data.Id,
                    FullName = response.Data.FullName,
                    Email = response.Data.Email,
                    Gender = response.Data.Gender,
                    Age = response.Data.Age,
                    Specialization = response.Data.Specialization,
                    Bio = response.Data.Bio,
                    ProfilePicture = response.Data.Image
                };

                return new GeneralResponse<DoctorDto>
                {
                    Success = true,
                    Message = "Doctor profile retrieved successfully.",
                    Data = doctorDto,
                    Errors = null

                };

            }
            return new GeneralResponse<DoctorDto>
            {
                Success = false,
                Message = response.Message,
                Errors = response.Errors
            };
        }

        public async Task<GeneralResponse<DoctorDto>> UpdateProfileAsync(updateDoctorDto doctorto, string ID)
        {
            if (doctorto == null)
            {
                return new GeneralResponse<DoctorDto>
                {
                    Success = false,
                    Message = "Doctor data cannot be null.",
                    Data = null,
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Doctor", new[] { "Doctor data is required." } }
                    }
                };
            }
            Doctor doctor = new Doctor();
            doctor.Id = ID;
            doctor.FullName = doctorto.FullName;
            doctor.Email = doctorto.Email;
            doctor.Specialization = doctorto.Specialization;
            doctor.Bio = doctorto.Bio;
            var resoponse = await _repo.UpdateProfileAsync(doctor, doctorto.ProfilePicture);
            if (resoponse.Success)
            {
                var doctorDto = new DoctorDto
                {
                    Id = resoponse.Data.Id,
                    FullName = resoponse.Data.FullName,
                    Email = resoponse.Data.Email,
                    Specialization = resoponse.Data.Specialization,
                    Bio = resoponse.Data.Bio,
                    ProfilePicture = resoponse.Data.Image,
                    Gender = resoponse.Data.Gender,
                    Age = resoponse.Data.Age

                };

                return new GeneralResponse<DoctorDto>
                {
                    Success = true,
                    Message = "Doctor profile updated successfully.",
                    Data = doctorDto,
                    Errors = null
                };
            }
            else
            {
                return new GeneralResponse<DoctorDto>
                {
                    Success = false,
                    Message = resoponse.Message,
                    Data = null,
                    Errors = resoponse.Errors
                };
            }

        }

        public async Task<GeneralResponse<List<DoctorUsers>>> GetDoctorUsersAsync(string ID)
        {
            if (ID == null)
            {

                return new GeneralResponse<List<DoctorUsers>>
                {
                    Success = false,
                    Message = "Doctor ID cannot be null.",
                    Data = null,
                    Errors = new Dictionary<string, string[]>
                        {
                            { "DoctorId", new[] { "Doctor ID is required." } }
                        }
                };

            }
            var response = await _repo.GetDoctorUsersAsync(ID);
            if (response.Success)
            {
                var doctorUsersList = response.Data.Select(du => new DoctorUsers
                {
                    ID = du.Id,
                    FullName = du.FullName,
                    Age = du.Age,
                    ImageUrl = du.Image
                }).ToList();
                return new GeneralResponse<List<DoctorUsers>>
                {
                    Success = true,
                    Message = "Doctor users retrieved successfully.",
                    Data = doctorUsersList,
                    Errors = null
                };
            }
            else
            {
                return new GeneralResponse<List<DoctorUsers>>
                {
                    Success = false,
                    Message = response.Message,
                    Data = null,
                    Errors = response.Errors
                };
            }
        }
    }
}