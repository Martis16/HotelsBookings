using AutoMapper;
using HotelsBookings.Server.Dtos.Hotels;
using HotelsBookings.Server.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBookings.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelsDto>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();
            var hotelDTOs = _mapper.Map<List<HotelsDto>>(hotels);
            return Ok(hotelDTOs);
        }

        [HttpGet("{location}")]
        public async Task<ActionResult<IEnumerable<HotelsDto>>> GetHotelsByLocation(string location)
        {
            var hotels = await _hotelRepository.GetHotelsByLocationAsync(location);
            if (hotels == null)
            {
                return NotFound();
            }

            var hotelDTOs = _mapper.Map<List<HotelsDto>>(hotels);
            return Ok(hotelDTOs);
        }
    }
}
