using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Dtos.Bookings;
using HotelsBookings.Server.Dtos.Hotels;

namespace HotelsBookings.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDBContext _context;

        public BookingController(AppDBContext context)
        {
            _context = context;

        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> GetBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Hotel) // Include hotel details
                .ToListAsync();

            var bookingDtos = bookings.Select(booking => new BookingsDto
            {
                Id = booking.Id,
                HotelsId = booking.HotelsId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                RoomType = booking.RoomType,
                IncludeBreakfast = booking.IncludeBreakfast,
                PersonCount = booking.PersonCount,
                TotalCost = booking.TotalCost,

                Hotel = new HotelsDto
                {
                    HotelName = booking.Hotel.HotelName,
                    Location = booking.Hotel.Location
                }
            }).ToList();

            return bookingDtos;
        }


        [HttpPost("{HotelsId}")]
        public async Task<ActionResult<BookingsDto>> CreateBooking([FromRoute] int HotelsId, Bookings booking)
        {
            // Calculate the total cost of the booking
            var roomRate = booking.RoomType switch
            {
                "Standard" => 100,
                "Deluxe" => 150,
                "Suite" => 200,
                _ => 100
            };

            booking.HotelsId = HotelsId;
            var breakfastCost = booking.IncludeBreakfast ? 15 : 0;
            var totalDays = (booking.EndDate - booking.StartDate).Days;
            var persons = booking.PersonCount;
            booking.TotalCost = (roomRate + persons * breakfastCost) * totalDays + 20;

            var bookingDTO = new BookingsDto
            {
                Id = booking.Id,
                HotelsId = booking.HotelsId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                RoomType = booking.RoomType,
                IncludeBreakfast = booking.IncludeBreakfast,
                PersonCount = booking.PersonCount,
                TotalCost = booking.TotalCost,
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, bookingDTO);
        }

    }
}
