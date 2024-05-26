using HotelsBookings.Server.Dtos.Bookings;

namespace HotelsBookings.Server.Dtos.Hotels
{
    public class HotelsDto
    {

        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public List<BookingsDto> Bookings { get; set; }
    }
}
