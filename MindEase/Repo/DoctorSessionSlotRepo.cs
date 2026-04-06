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
    public class DoctorSessionSlotRepo : IDoctorSessionSlotRepo
    {
        private readonly AppDbContext _context;
        private readonly UserManager<GeneralUser> _userManager;

        public DoctorSessionSlotRepo(AppDbContext context, UserManager<GeneralUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<GeneralResponse<List<DoctorSessionSlot>>> ManageSlotsAsync(List<DoctorSessionSlot> doctorSessionSlots, bool isUpdate)
        {
            try
            {
                if (isUpdate)
                {

                    var existingDoctorSessionSlots = await _context.DoctorSessionSlots.Where(x => x.DoctorWeeklyScheduleId == doctorSessionSlots[0].DoctorWeeklyScheduleId && x.IsBooked == false).ToListAsync();

                    _context.DoctorSessionSlots.RemoveRange(existingDoctorSessionSlots);
                    await _context.SaveChangesAsync();
                }

                foreach (var item in doctorSessionSlots)
                {

                    var existingDoctorWeeklySchedule = await _context.DoctorWeeklySchedules.FirstOrDefaultAsync(d => d.Id == item.DoctorWeeklyScheduleId);
                    if (existingDoctorWeeklySchedule == null)
                    {
                        return new GeneralResponse<List<DoctorSessionSlot>>
                        {
                            Success = false,
                            Message = "Doctor Weekly Schedule not found"
                        };
                    }


                    _context.DoctorSessionSlots.Add(item);
                }

                await _context.SaveChangesAsync();
                return new GeneralResponse<List<DoctorSessionSlot>>
                {
                    Success = true,
                    Message = "Slots Created For Doctor Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<DoctorSessionSlot>>
                {
                    Success = false,
                    Message = "Failed to Create Slots For Doctor.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };

            }


        }

        public async Task<GeneralResponse<List<DoctorSessionSlot>>> TriggerUpdateSlotsForDoctor(List<DoctorSessionSlot> doctorSessionSlots)
        {
            try
            {
                var currentDate = DateTime.Now;
                var itemDeleted = false;

                foreach (var item in doctorSessionSlots)
                {
                    var existingDoctorSessionSlotForDelete = await _context.DoctorSessionSlots.FirstOrDefaultAsync(x => x.DoctorWeeklyScheduleId == item.DoctorWeeklyScheduleId &&
                    x.IsBooked == true &&
                    x.CreationTime < currentDate.AddDays(-7) &&
                    (x.StartTime == item.StartTime || x.EndTime == item.EndTime));
                    if (existingDoctorSessionSlotForDelete != null)
                    {
                        _context.DoctorSessionSlots.RemoveRange(existingDoctorSessionSlotForDelete);
                        itemDeleted = true;
                    }


                    var existAvailableSlot = await _context.DoctorSessionSlots.FirstOrDefaultAsync(x => x.DoctorWeeklyScheduleId == item.DoctorWeeklyScheduleId && x.IsBooked == false &&
                    (x.StartTime == item.StartTime || x.EndTime == item.EndTime));

                    if (existAvailableSlot == null)
                    {

                        var existingDoctorWeeklySchedule = await _context.DoctorWeeklySchedules.FirstOrDefaultAsync(d => d.Id == item.DoctorWeeklyScheduleId);
                        if (existingDoctorWeeklySchedule == null)
                        {
                            return new GeneralResponse<List<DoctorSessionSlot>>
                            {
                                Success = false,
                                Message = "Doctor Weekly Schedule not found"
                            };
                        }


                        _context.DoctorSessionSlots.Add(item);
                    }
                }

                await _context.SaveChangesAsync();
                return new GeneralResponse<List<DoctorSessionSlot>>
                {
                    Success = true,
                    Message = "Slots Created For Slot Successfully.",
                };

            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<DoctorSessionSlot>>
                {
                    Success = false,
                    Message = "Failed to Create Slot For Doctor.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };

            }


        }


        public async Task<GeneralResponse<DoctorSessionSlot>> SetSlotAsBooked(int slotId)
        {
            try
            {
                DoctorSessionSlot existedSlot = await _context.DoctorSessionSlots.FindAsync(slotId);

                if (existedSlot == null)
                {
                    return new GeneralResponse<DoctorSessionSlot>
                    {
                        Success = false,
                        Message = "Slot not found."
                    };
                }
                existedSlot.IsBooked = true;
                await _context.SaveChangesAsync();

                return new GeneralResponse<DoctorSessionSlot>
                {
                    Success = true,
                    Data = existedSlot,
                    Message = "Slot has been booked successfully."
                };
            }
            catch (Exception ex)
            {

                return new GeneralResponse<DoctorSessionSlot>
                {
                    Success = false,
                    Message = "Failed to book Slot .",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }


        //public async Task<GeneralResponse<List<DoctorSessionSlot>>> GetSlotByDoctorIdAsync(string doctorId)
        //{
        //    try
        //    {
        //        var slots = await _context.DoctorSessionSlots.Where(x => x.DoctorWeeklySchedule.DoctorId == doctorId).ToListAsync();

        //        if (slots == null)
        //        {
        //            return new GeneralResponse<List<DoctorSessionSlot>>
        //            {
        //                Success = false,
        //                Message = "Journal not found."
        //            };
        //        }

        //        return new GeneralResponse<List<DoctorSessionSlot>>
        //        {
        //            Success = true,
        //            Data = slots
        //        };

        //    }
        //    catch (Exception ex)
        //    {

        //        return new GeneralResponse<List<DoctorSessionSlot>>
        //        {
        //            Success = false,
        //            Message = "Failed to retrieve slot.",
        //            Errors = new Dictionary<string, string[]>
        //            {
        //                { "Server", new[] { ex.Message } }
        //            }
        //        };
        //    }
        //}

    }
}