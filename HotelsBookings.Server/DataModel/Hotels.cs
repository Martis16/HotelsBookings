using System.Collections.Generic;


namespace HotelsBookings.Server.DataModel
{
    public class Hotels
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public string Image {  get; set; }
        public ICollection<Bookings> Bookings {  get; set; }


    }
}
