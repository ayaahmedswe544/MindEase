using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindEase.IRepo;
using MindEase.IService;
using MindEase.Models;
using MindEase.Models.Response;
using MindEase.Service;
using System;

namespace MindEase.Repo
{
    public class DoctorScheduleRepo : IDoctorScheduleRepo
    {
        private readonly AppDbContext _context;
        private readonly UserManager<GeneralUser> _userManager;

        public DoctorScheduleRepo(AppDbContext context, UserManager<GeneralUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<GeneralResponse<DoctorWeeklySchedule>> CreateAsync(DoctorWeeklySchedule doctorSchedule)
        {
            try
            {
                var existingDoctor = _context.Users.OfType<Doctor>().FirstOrDefault(d => d.Id == doctorSchedule.DoctorId);
                if (existingDoctor == null)
                {
                    return new GeneralResponse<DoctorWeeklySchedule>
                    {
                        Success = false,
                        Message = "Doctor not found"
                    };
                }
                bool ExisDayOfWeek = _context.DoctorWeeklySchedules.Any(u => u.DayOfWeek == doctorSchedule.DayOfWeek && u.Id == doctorSchedule.Id);
                if (ExisDayOfWeek)
                {
                    return new GeneralResponse<DoctorWeeklySchedule>
                    {
                        Success = false,
                        Message = "This Day is already exists.",
                        Errors = new Dictionary<string, string[]>
                            {
                                { "DayOfWeek", new[] { "This Day is already in use" } }
                            }
                    };
                }


                _context.DoctorWeeklySchedules.Add(doctorSchedule);
                await _context.SaveChangesAsync();
                return new GeneralResponse<DoctorWeeklySchedule>
                {
                    Success = true,
                    Data = doctorSchedule,
                    Message = "Doctor Schedule created successfully."
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse<DoctorWeeklySchedule>
                {
                    Success = false,
                    Message = "Failed to Create Day For Doctor.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };

            }


        }


        public async Task<GeneralResponse<DoctorWeeklySchedule>> UpdateAsync(DoctorWeeklySchedule doctorSchdule)
        {
            try
            {
                DoctorWeeklySchedule DoctorWeeklyScheduleFromDb = await _context.DoctorWeeklySchedules.FindAsync(doctorSchdule.Id);
                if (DoctorWeeklyScheduleFromDb == null)
                {
                    return new GeneralResponse<DoctorWeeklySchedule>
                    {
                        Success = false,
                        Message = "Doctor Weekly Schedule not found."
                    };
                }
                if (DoctorWeeklyScheduleFromDb.DoctorId != doctorSchdule.DoctorId)
                {
                    return new GeneralResponse<DoctorWeeklySchedule>
                    {
                        Success = false,
                        Message = "Unauthorized to update this doctorSchdule."
                    };
                }
                if (doctorSchdule.DayOfWeek != DoctorWeeklyScheduleFromDb.DayOfWeek)
                {
                    DoctorWeeklyScheduleFromDb.DayOfWeek = doctorSchdule.DayOfWeek;
                }

                if (doctorSchdule.StartTime != DoctorWeeklyScheduleFromDb.StartTime)
                {
                    DoctorWeeklyScheduleFromDb.StartTime = doctorSchdule.StartTime;
                }
                
                if (doctorSchdule.EndTime != DoctorWeeklyScheduleFromDb.EndTime)
                {
                    DoctorWeeklyScheduleFromDb.EndTime = doctorSchdule.EndTime;
                }
                
                if (doctorSchdule.IsActive != DoctorWeeklyScheduleFromDb.IsActive)
                {
                    DoctorWeeklyScheduleFromDb.IsActive = doctorSchdule.IsActive;
                }




                await _context.SaveChangesAsync();

                return new GeneralResponse<DoctorWeeklySchedule>
                {
                    Success = true,
                    Data = DoctorWeeklyScheduleFromDb,
                    Message = "Doctor Weekly Schedule Updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<DoctorWeeklySchedule>
                {
                    Success = false,
                    Message = "Failed to Update doctor Schdule.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }


        }


        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var doctorSchdule = await _context.DoctorWeeklySchedules.FindAsync(id);
                if (doctorSchdule == null)
                {
                    return new GeneralResponse<bool>
                    {
                        Success = false,
                        Message = "DoctorSchdule not found."
                    };
                }

                _context.DoctorWeeklySchedules.Remove(doctorSchdule);
                await _context.SaveChangesAsync();

                return new GeneralResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "DoctorSchdule deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<bool>
                {
                    Success = false,
                    Message = "Failed to delete doctorSchdule.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }


        public async Task<GeneralResponse<List<DoctorWeeklySchedule>>> GetByDoctorIdAsync(string doctorId)
        {
            try
            {
                var doctorShedules = await _context.DoctorWeeklySchedules
                    .Where(m => m.DoctorId == doctorId)
                    .ToListAsync();

                return new GeneralResponse<List<DoctorWeeklySchedule>>
                {
                    Success = true,
                    Data = doctorShedules,
                    Message = "doctor Shedules retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<DoctorWeeklySchedule>>
                {
                    Success = false,
                    Message = "Failed to retrieve user doctor Shedules.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }
    }
}