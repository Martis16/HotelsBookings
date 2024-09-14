using HotelsBookings.Server.DataModel;

namespace HotelsBookings.Server.Repository.Interface
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<IEnumerable<Hotel>> GetHotelsByLocationAsync(string location);
    }
}
