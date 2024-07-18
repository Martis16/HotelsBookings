using AutoMapper;
using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookings.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public BookingController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public decimal CalculateTotalCost(BookingsDto booking, int roomRate)
        {
            var breakfastCost = booking.IncludeBreakfast ? 15 : 0;
            var totalDays = (booking.EndDate - booking.StartDate).Days;
            if (totalDays == 0) totalDays = 1; // At least one day should be considered
            var persons = booking.PersonCount;
            return (roomRate + persons * breakfastCost) * totalDays + 20;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> GetBookings()
        {
            var bookings = await _context.Bookings.Include(b => b.Hotel).ToListAsync();
            var bookingDtos = _mapper.Map<List<BookingsDto>>(bookings);
            return bookingDtos;
        }

        [HttpPost("{HotelsId}")]
        public async Task<ActionResult> CreateBooking([FromRoute] int HotelsId, [FromBody] BookingsDto booking)
        {
            var hotel = await _context.Hotels.FindAsync(HotelsId);
            if (hotel == null)
            {
                return NotFound(new { message = "Hotel not found." });
            }

            if (booking.EndDate <= DateTime.Today)
            {
                return BadRequest(new { message = "Booking end date must be in the future." });
            }

            if (booking.StartDate >= booking.EndDate)
            {
                return BadRequest(new { message = "StartDate must be before EndDate." });
            }

            if (booking.PersonCount <= 0)
            {
                return BadRequest(new { message = "PersonCount must be a positive number." });
            }

            var roomRate = booking.RoomType switch
            {
                "Standard" => 100,
                "Deluxe" => 150,
                "Suite" => 200,
                _ => 100
            };

            booking.HotelsId = HotelsId;
            booking.TotalCost = CalculateTotalCost(booking, roomRate);

            var bookingEnt = _mapper.Map<Booking>(booking);

            try
            {
                _context.Bookings.Add(bookingEnt);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the booking.", details = ex.Message });
            }

            var bookingDTO = _mapper.Map<BookingsDto>(bookingEnt);
            return CreatedAtAction(nameof(GetBookings), new { id = bookingEnt.Id }, bookingDTO);
        }
    }
}
