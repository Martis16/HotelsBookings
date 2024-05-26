using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace HotelsBookings.Server.DataModel
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Bookings> Bookings { get; set; }




    }

}
