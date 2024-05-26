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
    public class HotelController : ControllerBase
    {
        private readonly AppDBContext _context;

        public HotelController(AppDBContext context)
        {
            _context = context;
            AddDummyData();
        }

        private void AddDummyData()
        {
            if (!_context.Hotels.Any())
            {
                var dummyHotels = new List<Hotels>
                {
                    new Hotels
                    {
                        HotelName = "Hotel Paradise",
                        Location = "Turkey, Belek",
                        Image = "./src/assets/Images/pexels-thorsten-technoman-109353-338504.jpg",
                        Bookings = new List<Bookings>()

                    },
                    new Hotels
                    {
                        HotelName = "Ocean View",
                        Location = "Philippines, El Nido",
                        Image = "./src/assets/Images/pexels-boonkong-boonpeng-442952-1134176.jpg",
                        Bookings = new List<Bookings>()
                    },
                    new Hotels
                    {
                        HotelName = "Mountain Retreat",
                        Location = "Turkey, Antalya",
                        Image = "./src/assets/Images/pexels-asman-chema-91897-594077.jpg",
                        Bookings = new List<Bookings>()
                    },
                    new Hotels
                    {
                        HotelName = "City Lights Hotel",
                        Location = "Greece, Heraklion",
                        Image = "./src/assets/Images/pexels-asadphoto-1268871.jpg",
                        Bookings = new List<Bookings>()
                    },
                    new Hotels
                    {
                        HotelName = "Desert Oasis",
                        Location = "Philippines, El Nido",
                        Image = "./src/assets/Images/pexels-michael-block-1691617-3225531.jpg",
                        Bookings = new List<Bookings>()
                    },
                    new Hotels
                    {
                        HotelName = "Lakeside Inn",
                        Location = "Maldives",
                        Image = "./src/assets/Images/pexels-ishan-139144-678725.jpg",
                        Bookings = new List<Bookings>()
                    },
                    new Hotels
                    {
                        HotelName = "Sunset Resort",
                        Location = "Maldives",
                        Image = "./src/assets/Images/pexels-asadphoto-1287460.jpg",
                        Bookings = new List<Bookings>()
                    },
                    new Hotels
                    {
                        HotelName = "Coastal Escape",
                        Location = "Egypt, Hurgada",
                        Image = "./src/assets/Images/pexels-pixabay-261429.jpg",
                        Bookings = new List<Bookings>()
                    }
                };


                _context.Hotels.AddRange(dummyHotels);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelsDto>>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(h => h.Bookings).ToListAsync();

            var hotelDTOs = hotels.Select(hotel => new HotelsDto
            {
                Id = hotel.Id,
                HotelName = hotel.HotelName,
                Location = hotel.Location,
                Image = hotel.Image,

                Bookings = hotel.Bookings.Select(booking => new BookingsDto
                {
                    Id = booking.Id,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    RoomType = booking.RoomType,
                    IncludeBreakfast = booking.IncludeBreakfast,
                    PersonCount = booking.PersonCount,
                    TotalCost = booking.TotalCost
                }).ToList()
            }).ToList();

            return hotelDTOs;
        }


        [HttpGet("{location}")]
        public async Task<ActionResult<IEnumerable<Hotels>>> GetHotelsByLocation(string location)
        {
            return await _context.Hotels.Where(h => h.Location.Contains(location)).ToListAsync();
        }
    }
}
