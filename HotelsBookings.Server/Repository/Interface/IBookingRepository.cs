using HotelsBookings.Server.DataModel;

namespace HotelsBookings.Server.Repository.Interface
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> CreateBookingAsync(Booking booking, int hotelId);

    }
}
