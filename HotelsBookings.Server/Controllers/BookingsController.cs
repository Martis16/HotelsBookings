using AutoMapper;
using HotelsBookings.Server.Calculations;
using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Dtos.Bookings;
using HotelsBookings.Server.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBookings.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        // GET: api/booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> GetBookings()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            var bookingDtos = _mapper.Map<IEnumerable<BookingsDto>>(bookings);
            return Ok(bookingDtos);
        }

        // POST: api/booking/{hotelId}
        [HttpPost("{hotelId}")]
        public async Task<ActionResult> CreateBooking([FromRoute] int hotelId, [FromBody] BookingsDto bookingDto)
        {
            // Validate the incoming DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the DTO to the entity
            var booking = _mapper.Map<Booking>(bookingDto);

            // Perform additional calculations before saving
            var strategy = new CalculateTotalCost();
            var calculator = new Calculator(strategy, bookingDto);

            booking.HotelsId = hotelId;
            booking.TotalCost = calculator.CalculateCost(bookingDto);

            // Handle validation errors
            if (booking.TotalCost == -1.0m) return BadRequest(new { message = "Booking end date must be in the future." });
            if (booking.TotalCost == -2.0m) return BadRequest(new { message = "StartDate must be before EndDate." });
            if (booking.TotalCost == -3.0m) return BadRequest(new { message = "PersonCount must be a positive number." });

            // Create the booking in the repository
            var createdBooking = await _bookingRepository.CreateBookingAsync(booking, hotelId);

            // Map the entity back to the DTO for the response
            var bookingResultDto = _mapper.Map<BookingsDto>(createdBooking);
            return CreatedAtAction(nameof(GetBookings), new { id = bookingResultDto.Id }, bookingResultDto);
        }
    }

}
