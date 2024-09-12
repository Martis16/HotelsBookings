using AutoMapper;
using HotelsBookings.Server.Calculations;
using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Dtos.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookings.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController(AppDBContext context, IMapper mapper) : ControllerBase
    {
        private readonly AppDBContext _context = context;
        private readonly IMapper _mapper = mapper;


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


            var strategy = new CalculateTotalCost();
            var Calculator = new Calculator(strategy, booking);

            booking.HotelsId = HotelsId;
            booking.TotalCost = Calculator.CalculateCost(booking);

            if (booking.TotalCost == -1.0m) return BadRequest(new { message = "Booking end date must be in the future." });
            if (booking.TotalCost == -2.0m) return BadRequest(new { message = "StartDate must be before EndDate." });
            if (booking.TotalCost == -3.0m) return BadRequest(new { message = "PersonCount must be a positive number." });

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
