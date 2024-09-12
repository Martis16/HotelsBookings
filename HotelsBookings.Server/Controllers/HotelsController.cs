using AutoMapper;
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
            _mapper = mapper;
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
