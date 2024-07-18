namespace HotelsBookings.Server.DataModel
{
    public class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public ICollection<Booking> Bookings { get; set; }


    }
}
