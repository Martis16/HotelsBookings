using HotelsBookings.Server.Dtos.Bookings;

namespace HotelsBookings.Server.Calculations
{
    public class CalculateTotalCost : ICalcualtor
    {
        public decimal Calculate(BookingsDto booking, int roomRate)
        {
            var breakfastCost = booking.IncludeBreakfast ? 15 : 0;
            var totalDays = (booking.EndDate - booking.StartDate).Days;
            if (totalDays == 0) totalDays = 1;
            var persons = booking.PersonCount;
            return (roomRate + persons * breakfastCost) * totalDays + 20;
        }
    }
}
