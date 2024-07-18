using AutoMapper;
using HotelsBookings.Server.DataModel;
using HotelsBookings.Server.Dtos.Bookings;
using HotelsBookings.Server.Dtos.Hotels;

namespace HotelsBookings.Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingsDto>().ReverseMap();
            CreateMap<Hotel, HotelsDto>().ReverseMap();
        }
    }

}
