using HotelsBookings.Server.Controllers;
using HotelsBookings.Server.Dtos.Bookings;

namespace HotelsBookings.Tests
{
    public class BookingControllerTests
    {
        [Theory]
        [InlineData("Standard", 2, true, "2024-07-20", "2024-07-22", 280)]
        [InlineData("Deluxe", 1, false, "2024-07-20", "2024-07-22", 320)]
        [InlineData("Suite", 3, true, "2024-07-20", "2024-07-21", 265)]
        [InlineData("Standard", 0, true, "2024-07-20", "2024-07-21", 120)]
        public void CalculateTotalCost_ShouldReturnCorrectCost(string roomType, int personCount, bool includeBreakfast, string startDate, string endDate, decimal expectedCost)
        {
            // Arrange
            var bookingDto = new BookingsDto
            {
                RoomType = roomType,
                PersonCount = personCount,
                IncludeBreakfast = includeBreakfast,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate)
            };

            var controller = new BookingController(null, null);

            // Act
            var roomRate = roomType switch
            {
                "Standard" => 100,
                "Deluxe" => 150,
                "Suite" => 200,
                _ => 100
            };

            var result = controller.CalculateTotalCost(bookingDto, roomRate);

            // Assert
            Assert.Equal(expectedCost, result);
        }
    }
}
