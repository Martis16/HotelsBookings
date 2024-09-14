using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookings.Server.Repository.Services
{
    public class BookingRepository(AppDBContext context) : IBookingRepository
    {
        private readonly AppDBContext _context = context;


        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                                 .Include(b => b.Hotel)
                                 .ToListAsync();
        }



        public async Task<Booking> CreateBookingAsync(Booking booking, int hotelId)
        {
            booking.HotelsId = hotelId;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

    }
}
