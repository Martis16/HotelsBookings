
using HotelsBookings.Server.Dtos.Hotels;

namespace HotelsBookings.Server.Dtos.Bookings
{
    public class BookingsDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public bool IncludeBreakfast { get; set; }
        public int PersonCount { get; set; }
        public string RoomType { get; set; }
        public int HotelsId { get; set; }
        public HotelsDto Hotel { get; set; }
    }
}
