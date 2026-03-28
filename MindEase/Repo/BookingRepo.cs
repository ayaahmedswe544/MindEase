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

        public BookingRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse<Booking>> CreateAsync(Booking booking)
        {
            try
            {
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
         
    }
}