using HotelsBookings.Server.Dtos.Bookings;

namespace HotelsBookings.Server.Calculations
{
    public class Calculator(ICalcualtor strategy, BookingsDto booking)
    {
        private readonly ICalcualtor _startegy = strategy;

        private readonly int roomRate = booking.RoomType switch
        {
            "Standard" => 100,
            "Deluxe" => 150,
            "Suite" => 200,
            _ => 100
        };


        public decimal CalculateCost(BookingsDto booking)
        {
            if (booking.EndDate <= DateTime.Today)
            {
                return -1.0m;
            }

            if (booking.StartDate >= booking.EndDate)
            {
                return -2.0m;
            }

            if (booking.PersonCount <= 0)
            {
                return -3.0m;
            }
            decimal price = 0;
            price = _startegy.Calculate(booking, roomRate);

            return price;
        }
    }
}
