namespace HotelsBookings.Server.DataModel
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public bool IncludeBreakfast { get; set; }
        public int PersonCount { get; set; }
        public string RoomType { get; set; }
        public int HotelsId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
