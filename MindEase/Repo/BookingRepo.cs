using Microsoft.EntityFrameworkCore;
using MindEase.Models;
using MindEase.IRepo;
using MindEase.Models.Response;
using System;

namespace MindEase.Repo
{
    public class BookingRepo : IBookingRepo
    {
        private readonly AppDbContext _context;
        private readonly IDoctorSessionSlotRepo _doctorSessionSlotRepo;

        public BookingRepo(AppDbContext context, IDoctorSessionSlotRepo doctorSessionSlotRepo)
        {
            _context = context;
            _doctorSessionSlotRepo = doctorSessionSlotRepo;
        }

        public async Task<GeneralResponse<Booking>> CreateAsync(Booking booking)
        {
            try
            {
                DoctorSessionSlot slot = await _context.DoctorSessionSlots.FindAsync(booking.DoctorSessionSlotId);
                if (slot.IsBooked == true)
                {

                    return new GeneralResponse<Booking>
                    {
                        Success = false,
                        Message = "This Slot is already booked."
                    };

                }

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return new GeneralResponse<Booking>
                {
                    Success = true,
                    Data = booking,
                    Message = "Booking created successfully."
                };
            }
            catch (Exception ex)
            {

                return new GeneralResponse<Booking>
                {
                    Success = false,
                    Message = "Failed to create Booking.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }


        public async Task<GeneralResponse<Booking>> ChangeStatusAsync(int Id, BookingStatus status)
        {
            try
            {
                Booking existedBooking = await _context.Bookings.FindAsync(Id);

                if (existedBooking == null)
                {
                    return new GeneralResponse<Booking>
                    {
                        Success = false,
                        Message = "Booking not found."
                    };
                }
                if (status == BookingStatus.Confirmed)
                {
                    existedBooking.ConfirmedAt = DateTime.Now;
                    await _doctorSessionSlotRepo.SetSlotAsBooked(existedBooking.DoctorSessionSlotId);
                }
                existedBooking.BookingStatus = status;
                await _context.SaveChangesAsync();

                return new GeneralResponse<Booking>
                {
                    Success = true,
                    Data = existedBooking,
                    Message = "Booking Status Updated successfully."
                };
            }
            catch (Exception ex)
            {

                return new GeneralResponse<Booking>
                {
                    Success = false,
                    Message = "Failed to Update Booking Status .",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }



        public async Task<GeneralResponse<List<Booking>>> GetByUserIdAsync(string userId, bool isDoctor)
        {
            try
            {
                List<Booking> bookings = new List<Booking>();
                if (isDoctor)
                {
                    bookings = await _context.Bookings
                      .Where(m => m.DoctorId == userId)
                      .ToListAsync();
                }
                else
                {
                    bookings = await _context.Bookings
                        .Where(m => m.UserId == userId)
                        .ToListAsync();
                }
                return new GeneralResponse<List<Booking>>
                {
                    Success = true,
                    Data = bookings,
                    Message = "User bookings retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<Booking>>
                {
                    Success = false,
                    Message = "Failed to retrieve user bookings.",
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Server", new[] { ex.Message } }
                    }
                };
            }
        }

    }
}