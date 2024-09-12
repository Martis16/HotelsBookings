using HotelsBookings.Server.Dtos.Bookings;

namespace HotelsBookings.Server.Calculations
{
    public interface ICalcualtor
    {
        public decimal Calculate(BookingsDto booking, int roomRate);
    }
}
