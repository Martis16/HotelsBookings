﻿using AutoMapper;
using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Dtos.Hotels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelsBookings.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public HotelController(AppDBContext context, IMapper mapper)
        {
            _context = context;
            AddDummyData();
            _mapper = mapper;
        }

        private void AddDummyData()
        {
            if (!_context.Hotels.Any())
            {
                var dummyHotels = new List<Hotel>
                {
                    new Hotel
                    {
                        HotelName = "Hotel Paradise",
                        Location = "Turkey, Belek",
                        Image = "./src/assets/Images/pexels-thorsten-technoman-109353-338504.jpg",
                        Bookings = new List<Booking>()

                    },
                    new Hotel
                    {
                        HotelName = "Ocean View",
                        Location = "Philippines, El Nido",
                        Image = "./src/assets/Images/pexels-boonkong-boonpeng-442952-1134176.jpg",
                        Bookings = new List<Booking>()
                    },
                    new Hotel
                    {
                        HotelName = "Mountain Retreat",
                        Location = "Turkey, Antalya",
                        Image = "./src/assets/Images/pexels-asman-chema-91897-594077.jpg",
                        Bookings = new List<Booking>()
                    },
                    new Hotel
                    {
                        HotelName = "City Lights Hotel",
                        Location = "Greece, Heraklion",
                        Image = "./src/assets/Images/pexels-asadphoto-1268871.jpg",
                        Bookings = new List<Booking>()
                    },
                    new Hotel
                    {
                        HotelName = "Desert Oasis",
                        Location = "Philippines, El Nido",
                        Image = "./src/assets/Images/pexels-michael-block-1691617-3225531.jpg",
                        Bookings = new List<Booking>()
                    },
                    new Hotel
                    {
                        HotelName = "Lakeside Inn",
                        Location = "Maldives",
                        Image = "./src/assets/Images/pexels-ishan-139144-678725.jpg",
                        Bookings = new List<Booking>()
                    },
                    new Hotel
                    {
                        HotelName = "Sunset Resort",
                        Location = "Maldives",
                        Image = "./src/assets/Images/pexels-asadphoto-1287460.jpg",
                        Bookings = new List<Booking>()
                    },
                    new Hotel
                    {
                        HotelName = "Coastal Escape",
                        Location = "Egypt, Hurgada",
                        Image = "./src/assets/Images/pexels-pixabay-261429.jpg",
                        Bookings = new List<Booking>()
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

            var hotelDTOs = _mapper.Map<List<HotelsDto>>(hotels);

            return Ok(hotelDTOs);
        }


        [HttpGet("{location}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotelsByLocation(string location)
        {
            return await _context.Hotels.Where(h => h.Location.ToLower().Contains(location.ToLower())).ToListAsync();
        }
    }
}
