using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookings.Server.Repository.Services
{
    public class HotelRepository(AppDBContext context) : IHotelRepository
    {
        private readonly AppDBContext _context = context;


        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                                 .Include(h => h.Bookings)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<Hotel>> GetHotelsByLocationAsync(string location)
        {
            return await _context.Hotels
                                 .Where(hotel => hotel.Location.Contains(location, System.StringComparison.CurrentCultureIgnoreCase))
                                 .ToListAsync();
        }
    }
}
